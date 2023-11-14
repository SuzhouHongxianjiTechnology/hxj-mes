using HslCommunication;
using HslCommunication.Profinet.Siemens;
using System.Net.NetworkInformation;
using Furion.Logging.Extensions;
using System.Net;
using TouchSocket.Core;
using AlbertCollection.Application.BgWorkers;
using AlbertCollection.Core.Const;
using AlbertCollection.Core.Entity.Device;
using AlbertCollection.Core.Enums;
using AlbertCollection.Application.Services.GatewayConfiguration.Dto;
using AlbertCollection.Application.Cache;
using AlbertCollection.Application.Services.Driver.Dto;
using static System.Collections.Specialized.BitVector32;
using TcpClient = TouchSocket.Sockets.TcpClient;
using TouchSocket.Sockets;
// ReSharper disable AsyncApostle.ConfigureAwaitHighlighting

namespace AlbertCollection.Application.Services.GatewayConfiguration;

/// <summary>
/// 西门子通讯
/// </summary>
public class S7Communication : BaseCommunication
{
    public DeviceCollection _device = new();

    private readonly ICacheRedisService _cacheService;

    private TcpClient _tcpClient = new TcpClient();

    /// <summary>
    /// 构造函数，初始化西门子读写
    /// </summary>
    /// <param name="device"></param>
    public S7Communication(DeviceCollection device, ICacheRedisService cacheService)
    {
        _device = device;
        _cacheService = cacheService;
    }

    private void PrintClinetLog(string msg)
    {
        try
        {
            msg.LogInformation();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    #region 设备相关

    /// <summary>
    /// 初始化设备
    /// </summary>
    public override BaseCommunication Init(bool singleCollect)
    {
        // 1. 先测试能否 ping 通
        if (!CmdPing(_device.IpAddress, out var strErr))
        {
            strErr.LogError();
            return this;
        }

        if (_device == null)
        {
            "初始化设备为空".LogError();
            return this;
        }

        // 2. 初始化：建立连接、开启心跳
        InitDevice(singleCollect);
        // 3. 记录日志
        BackMessage.AddMessage(_device.Name + DeviceConst.DEV_INIT_OK, LogLevel.Information);
        return this;
    }

    /// <summary>
    /// 关闭设备
    /// </summary>
    /// <returns></returns>
    public override async Task<bool> StopDeviceAsync()
    {
        if (!_device.IsOpen) return false;
        var operateResult = await _device.SimTcpNet.ConnectCloseAsync();
        if (operateResult.IsSuccess)
        {
            // 关闭设备心跳进程
            _device.IsOpen = false;
            _device.StopHeartbeatOpenTokenSource?.Cancel();
            _device.StopHeartbeatOpenTokenSource?.SafeDispose();
            // 遍历关闭工站 IsOpen = true 的设备
            _device.StationSeqs.ForEach(s => { StopStation(s.SeqName); });
        }

        BackMessage.AddMessage(_device.Name + DeviceConst.DEV_DISCONNECT, LogLevel.Error);
        return operateResult.IsSuccess;
    }

    /// <summary>
    /// 获取设备
    /// </summary>
    /// <returns></returns>
    public override DeviceCollection GetDevice()
    {
        return _device;
    }

    /// <summary>
    /// 更新设备
    /// </summary>
    /// <param name="device"></param>
    /// <returns></returns>
    public override BaseCommunication UpdateDevice(DeviceCollection device)
    {
        _device.Name = device.Name;
        _device.IpAddress = device.IpAddress;
        _device.Port = device.Port;
        _device.SimPlcType = device.SimPlcType;
        _device.StopHeartbeatOpenTokenSource = new CancellationTokenSource();
        return this;
    }

    private void InitDevice(bool singleCollect)
    {
        // 1. 初始化 PLC
        CreateSiemens();
        // 2. 连接 PLC
        ConnectSiemensPlc();
        // 3. 只有当设备成功建立通讯后才开启心跳、开启所有工站采集
        if (_device.IsOpen)
        {
            SendHeartBeat(_device.StopHeartbeatOpenTokenSource.Token);
            if (!singleCollect) {
                _device.StationSeqs.ForEach(s => { StartStation(s.SeqName); });
            } 
        }
    }

    /// <summary>
    /// 获取在制产品
    /// </summary>
    /// <returns></returns>
    private async Task<Albert_PdmProduct> GetPdmProductAsync()
    {
        // 1.获取在制产品型号
        var pdmProduct = _cacheService.Get<Albert_PdmProduct>(CacheConst.PdmProduct);

        if (pdmProduct == null)
        {
            // 2.如果缓存中没有，则直接拿第一件产品作为默认的型号
            pdmProduct = await DbContext.Db.Queryable<Albert_PdmProduct>()
                .FirstAsync(x => x.ProductPkInt == 1);
            _cacheService.AddObject(CacheConst.PdmProduct, pdmProduct);
        }

        return pdmProduct;
    }

    /// <summary>
    /// 根据在制产品型号获取使用的工艺
    /// </summary>
    /// <returns></returns>
    private async Task<Albert_Craft> GetCraftAsync()
    {
        var craft = _cacheService.Get<Albert_Craft>(CacheConst.Craft);

        if (craft == null)
        {
            // 1.获取在制产品型号
            var pdmProduct = await GetPdmProductAsync();

            // 2.根据产品型号获取工艺，即为在制工艺
            craft = await DbContext.Db
                .Queryable<Albert_Craft>()
                .FirstAsync(x => x.CraftPkInt == pdmProduct.CraftPkInt);
            _cacheService.AddObject(CacheConst.Craft, craft);
        }

        return craft;
    }

    /// <summary>
    /// 获取在制工艺下的所有工站
    /// </summary>
    /// <returns></returns>
    private async Task<List<Albert_PdmCraftDevice>> GetPdmCraftStationListApi()
    {
        // 在制工艺-对应所有设备
        var craft = await GetCraftAsync();

        if (craft == null)
        {
            return null;
        }
        else
        {
            var craftStationList = _cacheService.Get<List<Albert_PdmCraftDevice>>(CacheConst.CraftStationList);

            if (craftStationList == null)
            {
                craftStationList = await DbContext.Db.Queryable<Albert_PdmCraftDevice>()
                    .Where(x => x.CraftPkInt == craft.CraftPkInt && x.DeviceDBIsUse == "Y")
                    .OrderBy(x => x.CraftSort)
                    .ToListAsync();
                _cacheService.AddObject(CacheConst.CraftStationList, craftStationList);
            }

            return craftStationList;
        }
    }

    /// <summary>
    /// 获取所有 Plc
    /// </summary>
    /// <returns></returns>
    private async Task<List<Albert_DeviceConfigure>> GetPdmDeviceListApiAsync()
    {
        // 1. 从缓存中获取所有 PLC
        var deviceList = _cacheService.Get<List<Albert_DeviceConfigure>>(CacheConst.DeviceList);

        if (deviceList == null)
        {
            deviceList = DbContext.Db
                .Queryable<Albert_DeviceConfigure>()
                .Where(x => x.DeviceIsUse == "Y")
                .OrderBy(x => x.DeviceSort)
                .ToList();
            _cacheService.AddObject(CacheConst.DeviceList, deviceList);
        }

        return deviceList;
    }

    private void SendHeartBeat(CancellationToken cancellationToken)
    {
        _device.IsHeartbeatOpen = true;
        bool firstStart = true;

        // 开启心跳
        Task.Run(async () =>
        {
            while (true)
                try
                {
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        var result = await _device.SimTcpNet.ReadInt16Async(_device.SetHeartAddress);

                        if (result.IsSuccess)
                        {
                            // 一直读 plc 数据，用于给 plc 判断上位机是否连接
                            ("HeartBeatOpen" +result.Content+ DateTime.Now).LogDebug();

                            // 以二线体切型为准
                            if (_device.Id == 2)
                            {
                                ReadData(_device.ProductType, out var productType);

                                if ((!string.IsNullOrEmpty(productType)) && (productType != "0"))
                                {
                                    // 1. 从缓存取出当前工艺采取的型号
                                    var pdmProduct = await GetPdmProductAsync();

                                    // 2.1 如果读取的产品型号和缓存中的在制工艺中的产品不一致，则进行切换型号
                                    // 更新数据库，更新缓存
                                    if (pdmProduct.ProductPkInt != productType.ToInt(1))
                                    {
                                        // 2.2 切换在制产品
                                        // ReSharper disable once AsyncApostle.ConfigureAwaitHighlighting
                                        pdmProduct = await DbContext.Db.Queryable<Albert_PdmProduct>()
                                            .FirstAsync(x => x.ProductPkInt == productType.ToInt(1));
                                        _cacheService.AddObject(CacheConst.PdmProductType, productType);
                                        _cacheService.AddObject(CacheConst.PdmProduct, pdmProduct);
                                        CacheConst.PdmProductUpdate.LogWarning();

                                        // 3.切换在制工艺(切换产品的话在制工艺一定会发生切换
                                        var craft = await DbContext.Db
                                            .Queryable<Albert_Craft>()
                                            .FirstAsync(x => x.CraftPkInt == pdmProduct.CraftPkInt);
                                        _cacheService.AddObject(CacheConst.Craft, craft);
                                        CacheConst.PdmProductUpdateCraft.LogWarning();

                                        // 4. 切换在制工艺对应的工站列表
                                        var craftStationList = await DbContext.Db.Queryable<Albert_PdmCraftDevice>()
                                            .Where(x => x.CraftPkInt == craft.CraftPkInt && x.DeviceDBIsUse == "Y")
                                            .OrderBy(x => x.CraftSort)
                                            .ToListAsync();

                                        craftStationList.ForEach(x => x.DeviceDBStatus = "Y");
                                        await DbContext.Db.Updateable(craftStationList)
                                            .WhereColumns(it => new { it.PdmCraftDevicePkInt })
                                            .ExecuteCommandAsync();
                                        _cacheService.AddObject(CacheConst.CraftStationList, craftStationList);
                                        CacheConst.PdmProductUpdateCraftStationList.LogWarning();
                                    }
                                }

                                if (firstStart)
                                {
                                    var craftStationList = await GetPdmCraftStationListApi();
                                    // 设置工站状态为运行状态
                                    craftStationList.ForEach(x => x.DeviceDBStatus = "Y");

                                    await DbContext.Db.Updateable(craftStationList)
                                        .WhereColumns(it => new { it.PdmCraftDevicePkInt })
                                        .ExecuteCommandAsync();
                                    _cacheService.AddObject(CacheConst.CraftStationList, craftStationList);
                                    firstStart = false;
                                    CacheConst.FirstUpdateStationListStatusY.LogWarning();
                                }
                            }
                        }
                        else
                        {
                            if (result.ErrorCode < 0)
                            {
                                _device.IsHeartbeatOpen = false;
                                BackMessage.AddMessage(("HeartBeatOpen" + result.Message + DateTime.Now), LogLevel.Error);

                                // 获取当前 plc
                                var pdmDeviceList = await GetPdmDeviceListApiAsync();
                                var device = pdmDeviceList
                                    .Where(x => x.DevicePkInt == _device.Id);

                                if (device == null)
                                {

                                }

                                // 设置工站状态为异常状态
                                var craftStationList = await GetPdmCraftStationListApi();
                                craftStationList.ForEach(x => x.DeviceDBStatus = "N");
                                await DbContext.Db.Updateable(craftStationList)
                                    .WhereColumns(it => new { it.PdmCraftDevicePkInt })
                                    .ExecuteCommandAsync();
                                _cacheService.AddObject(CacheConst.CraftStationList, craftStationList);
                                firstStart = true;
                                CacheConst.FirstUpdateStationListStatusN.LogWarning();
                            }   
                        }

                        await Task.Delay(500, cancellationToken);
                    }
                    else
                    {
                        _device.IsHeartbeatOpen = false;
                        BackMessage.AddMessage(_device.Name+DeviceConst.DEV_HEARTBEAT_STOP, LogLevel.Information);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString().LogError();
                    _device.IsHeartbeatOpen = false;
                }
        }, cancellationToken);
    }

    private void CreateSiemens()
    {
        try
        {
            _device.SimTcpNet = new SiemensS7Net(SiemensPLCS.S1500);
            _device.IsOpen = ConnectSiemensPlc();
        }
        catch (Exception ex)
        {
            ("型号：" + _device.SimPlcType.ToString() + " IP：" + _device.IpAddress + " PLC连接异常：" + ex.Message).LogError();
        }
    }

    private bool CmdPing(string strHost, out string strErr)
    {
        try
        {
            lock (strHost)
            {
                if (strHost == "." || strHost == "localhost") strHost = "127.0.0.1";
                if (!IPAddress.TryParse(strHost, out var ip))
                {
                    strErr = "连接失败";
                    return false;
                }

                // Windows L2TP VPN和非Windows VPN使用ping VPN服务端的方式获取是否可以连通     
                var pingSender = new Ping();
                var options = new PingOptions();

                // Use the default Ttl value which is 128,     
                // but change the fragmentation behavior.     
                options.DontFragment = true;

                // Create a buffer of 32 bytes of data to be transmitted.     
                var data = "test";
                var buffer = Encoding.ASCII.GetBytes(data);
                var timeout = 50;
                var reply = pingSender.Send(strHost, timeout, buffer, options);
                strErr = "";
                return reply?.Status == IPStatus.Success;
            }
        }
        catch (Exception ex)
        {
            strErr = ex.Message;
            return false;
        }
    }

    private bool ConnectSiemensPlc()
    {
        if (!IPAddress.TryParse(_device.IpAddress, out var address))
        {
            ("型号：" + _device.SimPlcType.ToString() + " Ip地址格式输入不正确！IP：" + _device.IpAddress).LogError();
            return false;
        }

        if (!int.TryParse(_device.Port, out var port)) port = 5000;
        _device.SimTcpNet.IpAddress = _device.IpAddress;
        _device.SimTcpNet.Port = port;
        try
        {
            var connect = _device.SimTcpNet.ConnectServer();
            if (!connect.IsSuccess)
                ("型号：" + _device.SimPlcType.ToString() + " PLC连接失败！IP：" + _device.IpAddress + " Port:" + port)
                    .LogError();
            return connect.IsSuccess;
        }
        catch (Exception ex)
        {
            ("型号：" + _device.SimPlcType.ToString() + " IP：" + _device.IpAddress + " PLC连接异常：" + ex.Message).LogError();
        }

        return false;
    }

    #endregion

    #region 工站相关

    /// <summary>
    /// 单站开启采集
    /// </summary>
    /// <param name="stationName"></param>
    public override void StartStation(string stationName)
    {
        var deviceSeq = _device.StationSeqs.Find(x => x.SeqName == stationName);
        if (deviceSeq != null && _device.SimTcpNet != null)
        {
            if (deviceSeq.IsOpen)
            {
                BackMessage.AddMessage(DeviceConst.DEV_Station_Start, LogLevel.Information);
            }
            else
            {
                deviceSeq.IsOpen = true;
                deviceSeq.StopStationTokenSource = new CancellationTokenSource();

                // 下面是两个异步分支
                // 一个是用于读 Rfid 并告知是否能做
                Task.Run(async () =>
                {
                    // 用于判断是否是第一次
                    var oldValue = -1;
                    var plc = _device.SimTcpNet;
                    while (true)
                    {
                        try
                        {
                            // 异步任务取消分支
                            if (deviceSeq.StopStationTokenSource.IsCancellationRequested)
                            {
                                deviceSeq.IsOpen = false;
                                $"{deviceSeq.SeqName}--停止采集".LogWarning();
                                break;
                            }
                            else
                            {
                                // 读取工站上升沿地址  从 0-->1 上升沿读取
                                var read = await plc.ReadBoolAsync(deviceSeq.RfidRisingEdge);

                                if (read.IsSuccess)
                                {
                                    if (oldValue == -1)
                                    {
                                        (deviceSeq.SeqName + "第一次开机未知状态").LogInformation();
                                        // 第一次开机未知的情况，先初始化一次数据
                                        // 更新历史的值
                                        oldValue = read.Content ? 1 : 0;
                                    }
                                    else
                                    {
                                        if (read.Content && oldValue == 0)
                                        {
                                            $"{deviceSeq.SeqName}--{CacheConst.RfidUp}".LogInformation();
                                            _cacheService.LPush(CacheConst.PlcMes, $"{deviceSeq.SeqName}--{CacheConst.RfidUp}");
                                            // 发生了上升沿，需要完成两步
                                            // 1. 读取 Rfid
                                            ReadData(deviceSeq.RfidLabel, out var rfid);
                                            // 发生了上升沿，两步交互逻辑
                                            // 读成功写两步.1.上升沿写 false，响应地址写 true
                                            await plc.WriteAsync(deviceSeq.RfidRisingEdge, false);
                                            $"{deviceSeq.SeqName}--{CacheConst.RfidDown}".LogInformation();
                                            _cacheService.LPush(CacheConst.PlcMes, $"{deviceSeq.SeqName}--{CacheConst.RfidDown}");

                                            if (string.IsNullOrEmpty(rfid))
                                            {
                                                $"{deviceSeq.SeqName}--{CacheConst.RfidReadError}".LogError();
                                            }
                                            else
                                            {
                                                var rfidModel = DbContext.Db.Queryable<Albert_RFID>()
                                                    .First(x => x.RFID.ToString() == rfid);

                                                $"{deviceSeq.SeqName}【Rfid 值】读取完毕{rfid}".LogInformation();
                                                _cacheService.LPush(CacheConst.PlcMes, $"{deviceSeq.SeqName}--【Rfid 值】读取完毕{rfid}");
                                                // 这边往字典里面添加了 RFID
                                                deviceSeq.ReadDataDic.AddOrUpdate("RFID", rfid);

                                                RfidAop(rfidModel, deviceSeq);
                                            }

                                            // 读成功写两步.上升沿写 false，2.响应地址写 true
                                            await plc.WriteAsync(deviceSeq.RfidResponseEdge, true);
                                            $"{deviceSeq.SeqName}--{CacheConst.RfidResponseUp}".LogInformation();
                                            _cacheService.LPush(CacheConst.PlcMes, $"{deviceSeq.SeqName}--{CacheConst.RfidResponseUp}");
                                        }

                                        // 更新历史的值
                                        oldValue = read.Content ? 1 : 0;
                                    }
                                }

                                deviceSeq.IsOpen = true;
                                await Task.Delay(20);
                            }
                        }
                        catch (Exception ex)
                        {
                            (deviceSeq.SeqName + ex.Message).LogError();
                            //deviceSeq.IsOpen = false;
                            //break;
                        }
                    }
                }, deviceSeq.StopStationTokenSource.Token);

                // 第二个分支用于读取数据
                Task.Run(async () =>
                {
                    // 用于判断是否是第一次
                    var oldValue = -1;
                    var plc = _device.SimTcpNet;
                    while (true)
                        try
                        {
                            // 异步任务取消分支
                            if (deviceSeq.StopStationTokenSource.IsCancellationRequested)
                            {
                                deviceSeq.IsOpen = false;
                                $"{deviceSeq.SeqName}--工站--停止采集".LogInformation();
                                break;
                            }
                            else
                            {
                                // 读取工站上升沿地址  从 0-->1 上升沿读取
                                var read = await plc.ReadBoolAsync(deviceSeq.RisingEdge);

                                if (read.IsSuccess)
                                {
                                    if (oldValue == -1)
                                    {
                                        (deviceSeq.SeqName + "第一次开机未知状态").LogInformation();
                                        // 第一次开机未知的情况，先初始化一次数据
                                        // 更新历史的值
                                        oldValue = read.Content ? 1 : 0; 
                                    }
                                    else
                                    {
                                        if (read.Content && oldValue == 0)
                                        {
                                            // 发生了上升沿，进入到业务处理模块
                                            $"{deviceSeq.SeqName}--{CacheConst.SaveDataUp}".LogInformation();
                                            _cacheService.LPush(CacheConst.PlcMes, $"{deviceSeq.SeqName}--{CacheConst.SaveDataUp}");

                                            // 读成功写两步.上升沿写 false，响应地址写 true
                                            await plc.WriteAsync(deviceSeq.RisingEdge, false);
                                            (deviceSeq.SeqName+"plc-mes 清除了保存数据上升沿").LogInformation();
                                            _cacheService.LPush(CacheConst.PlcMes, $"{deviceSeq.SeqName}--{CacheConst.SaveDataDown}");
                                            // 这边分为 批量读-插入数据库 批量读-更新数据库 写入PLC

                                            switch (deviceSeq.SqlOperate)
                                            {
                                                // 读取，Op10 站
                                                case SqlOperateEnum.None:
                                                    // 读取列表，绑定到 Rfid 上，等到 Op20 的时候再切换绑定
                                                    await SqlOperateNone(deviceSeq,plc);
                                                    break;
                                                // 读取插入
                                                case SqlOperateEnum.Insert:
                                                    await SqlOperateInsert(deviceSeq,plc);
                                                    break;
                                                // 读取更新
                                                case SqlOperateEnum.Update:
                                                    await SqlOperateUpdate(deviceSeq, plc);
                                                    break;
                                                case SqlOperateEnum.Ignore:
                                                    break;
                                                default:
                                                    (deviceSeq.SeqName + "【未走任何分支，请检查配置文件】").LogError();
                                                    break;
                                            }

                                            (deviceSeq.SeqName + "【mes-plc 保存数据结束】完成").LogInformation();
                                            // 读成功写两步.上升沿写 false，响应地址写 true
                                            await plc.WriteAsync(deviceSeq.ResponseEdge, true);
                                            $"{deviceSeq.SeqName}--{CacheConst.SaveDataResponseUp}".LogInformation();
                                            _cacheService.LPush(CacheConst.PlcMes, $"{deviceSeq.SeqName}--{CacheConst.SaveDataResponseUp}");
                                        }
                                        // 更新历史的值
                                        oldValue = read.Content ? 1 : 0; 
                                    }
                                }
                            }

                            deviceSeq.IsOpen = true;
                            await Task.Delay(20);
                        }
                        catch (Exception ex)
                        {
                            (deviceSeq.SeqName+ex.Message).LogError();
                            // deviceSeq.IsOpen = false;
                            // break;
                        }
                }, deviceSeq.StopStationTokenSource.Token);

                // 这边是采集压机曲线
                if (deviceSeq.SeqName == "Op120")
                {
                    // Rfid 上升延 这边有一个特殊情况 Op120 工站要根据 1200.4 bool false结束 true开启
                    // Op120Torque float,DB50.1210.0
                    Task.Run(async () =>
                    {
                        // 用于判断是否是第一次
                        var plc = _device.SimTcpNet;
                        var torqueList = new List<string>();

                        while (true)
                        {
                            try
                            {
                                // 异步任务取消分支
                                if (deviceSeq.StopStationTokenSource.IsCancellationRequested)
                                {
                                    deviceSeq.IsOpen = false;
                                    $"{deviceSeq.SeqName}--停止采集".LogWarning();
                                    break;
                                }
                                else
                                {
                                    torqueList.Clear();
                                    // 读取压机上升沿信号，如果是 1，则直接循环采集 50 个点
                                    var torqueUp = await plc.ReadBoolAsync("DB50.1200.4");

                                    if (torqueUp.Content)
                                    {
                                        // ToDo;和 plc 商量，读取完后我来写 0
                                        // 同时读取 rfid ，rfid 表上面有产品码，根据产品码将读取到的压机曲线数据插入到数据库
                                        ReadData(deviceSeq.RfidLabel, out var rfid);
                                        for (int i = 0; i < 50; i++)
                                        {
                                            ReadData("float,DB50.1210.0", out var torque);
                                            // 用来防止 plc 读取的位数过长
                                            string subTorque = torque.Length >= 4 ? torque.Substring(0, 4) : torque;
                                            torqueList.Add(subTorque);
                                            await Task.Delay(20);
                                        }

                                        // 将 torqueList 中的数据插入到数据库中
                                        if (torqueList.Count > 0)
                                        {
                                            string op120TorqueList = string.Join(",", torqueList);

                                            var rfidModel = DbContext.Db.Queryable<Albert_RFID>()
                                                .First(x => x.RFID.ToString() == rfid);

                                            var tempDic = new Dictionary<string, object>();
                                            tempDic.AddOrUpdate("ProductCode", rfidModel.ProductCode);
                                            tempDic.AddOrUpdate("Op120TorqueList", op120TorqueList);

                                            var line = await DbContext.Db.Updateable(tempDic)
                                                .AS("Albert_DataFirst").WhereColumns("ProductCode").ExecuteCommandAsync();

                                            if (line > 0)
                                            {
                                                (deviceSeq.SeqName + "压机曲线更新成功").LogInformation();
                                                await Task.Delay(1000);
                                            }
                                            else
                                            {
                                                (deviceSeq.SeqName + "压机曲线更新失败").LogError();
                                            }
                                        }
                                        else
                                        {
                                            (deviceSeq.SeqName + "压机曲线未获取到任何数据").LogError();
                                        }
                                    }

                                    await Task.Delay(50);
                                }
                            }
                            catch (Exception ex)
                            {
                                (deviceSeq.SeqName + ex.Message).LogError();
                                //deviceSeq.IsOpen = false;
                                //break;
                            }
                        }
                    }, deviceSeq.StopStationTokenSource.Token);
                }
            }
        }
        else
        {
            BackMessage.AddMessage(DeviceConst.DEV_Station_FIND_NG, LogLevel.Error);
        }
    }


    private void RfidAop(Albert_RFID? rfidModel,DeviceSeq deviceSeq)
    {

        if (deviceSeq.SeqName == "Op10"||deviceSeq.SeqName =="Op150")
        {
            // 不为 0,表示被占用，直接给出错误和 NG
            if (rfidModel?.RFIDIsUse != 0)
            {
                // 直接 NG [允许工作1允许，2NG]
                WriteData(deviceSeq.StationAllow, "2", out _);
                $"{deviceSeq.SeqName}--{CacheConst.RfidIsUse}--{rfidModel?.RFID} 直接给 plc {deviceSeq.StationAllow} 发 2".LogError();
                _cacheService.LPush(CacheConst.PlcMes, $"{deviceSeq.SeqName}--{CacheConst.RfidIsUse}--{rfidModel?.RFID} 直接给 plc {deviceSeq.StationAllow}发 2");
            }
            else
            {
                // 如果没被占用直接发 1
                WriteData(deviceSeq.StationAllow, "1", out _);
                $"{deviceSeq.SeqName}【产品码为空直接放行】-OK".LogInformation();
            }
        }
        else
        {
            // 这些可能会被重写
            deviceSeq.ReadDataDic.AddOrUpdate("OpFinalResult", "OK");
            deviceSeq.ReadDataDic.AddOrUpdate(deviceSeq.SeqName + "Result", "OK");

            // 2.2 【一线体】Op10、Op20 不会走该分支，到 Op30 之后会用到 Rfid 表中产品码
            // 根据托盘的产品码去数据库 Albert_DataFirst 表中查询产品码是否存在，如果存在再去线体查找是否可以做
            if (deviceSeq.DeviceId == 1)
            {
                // 140 清洗工站直接放行
                if (deviceSeq.SeqName == "Op20"||deviceSeq.SeqName == "Op140")
                {
                    // 直接发 1
                    WriteData(deviceSeq.StationAllow, "1", out _);
                    $"{deviceSeq.SeqName}【产品码为空直接放行】-OK".LogInformation();
                }
                else
                {
                    // 一线体(与数据库交互一次) Op20 是不需要和数据库交互的，直接 
                    var dataFirst = DbContext.Db.Queryable<Albert_DataFirst>()
                        .First(x => x.ProductCode == rfidModel.ProductCode);

                    if (dataFirst != null)
                    {
                        // 如果托盘绑定了产品码，这边直接放入，后面要用到
                        deviceSeq.ReadDataDic.AddOrUpdate("ProductCode", rfidModel.ProductCode);

                        // 如果上一站结果是 NG，下面所有站都会是 NG
                        if (dataFirst.OpFinalResult == "NG")
                        {
                            // 这些可能会被重写
                            deviceSeq.ReadDataDic.AddOrUpdate("OpFinalResult", "NG");
                            deviceSeq.ReadDataDic.AddOrUpdate(deviceSeq.SeqName + "Result", "NG");
                        }

                        #region 屏蔽工站逻辑(根据 Redis 缓存获取工站列表)
                        // 从缓存中获取数据
                        var station = _cacheService
                            .Get<List<Albert_PdmCraftDevice>>(CacheConst.CraftStationList)
                            ?.Where(x => x.DeviceDBName == deviceSeq.SeqName)
                            .First();

                        // 没有缓存从数据库获取
                        if (station == null)
                        {
                            var stationList = DbContext.Db
                                .Queryable<Albert_PdmCraftDevice>()
                                .Where(x => x.DeviceDBName ==
                                    deviceSeq.SeqName);

                            // 写入缓存中
                            _cacheService.AddObject(
                                CacheConst.CraftStationList, stationList);

                            station = stationList.First();
                        }

                        // 工站屏蔽了,直接发 2，让其流出
                        if (station?.DeviceDBIsUse == "N")
                        {
                            // NG 件 [允许工作1允许，2NG]
                            WriteData(deviceSeq.StationAllow, "2", out _);
                            $"{deviceSeq.SeqName}【工站屏蔽了,直接 NG 流出】".LogWarning();
                        }
                        else
                        {
                            // 没有屏蔽工站，则按照正常逻辑进行
                            var finalResult = (dataFirst.OpFinalResult == "OK") ? "1" : "2";
                            WriteData(deviceSeq.StationAllow, finalResult, out _);
                            $"{deviceSeq.SeqName}【rfid 标签上升沿反馈】{finalResult}".LogInformation();
                        }
                        #endregion
                    }
                    else
                    {
                        // 未根据产品码查询到相关数据，直接 NG
                        WriteData(deviceSeq.StationAllow, "2", out _);
                        $"{deviceSeq.SeqName}未根据产品码查询到相关数据，直接 NG".LogError();
                    }
                }
            }
            else
            {
                // 二线体 Op260 Op270 需要解放
                if (deviceSeq.SeqName == "Op160"|| deviceSeq.SeqName == "Op170"||
                    deviceSeq.SeqName == "Op180_1" || deviceSeq.SeqName == "Op180_2" ||
                    deviceSeq.SeqName == "Op180_3" || deviceSeq.SeqName == "Op350")
                {
                    // 直接发 1
                    WriteData(deviceSeq.StationAllow, "1", out _);
                    $"{deviceSeq.SeqName}【直接放行,发 1 OK】".LogInformation();
                }
                else
                {
                    // 280 需要我这边给 plc 发送一个壳体码
                    if (deviceSeq.SeqName == "Op280" && deviceSeq.WriteData.Count > 0)
                    {
                        if (string.IsNullOrEmpty(rfidModel?.ShellCode))
                        {
                            var mockShellCode = _cacheService.Get("MockShellCode");
                            WriteData(deviceSeq.WriteData[0].TypeAndDb, mockShellCode, out _);
                            $"【280 站】发送模拟课题码{mockShellCode}".LogInformation();
                        }
                        else
                        {
                            WriteData(deviceSeq.WriteData[0].TypeAndDb, rfidModel?.ShellCode, out _);
                        }
                    }

                    if (deviceSeq.SeqName == "Op330")
                    {
                        PrintZpl();
                    }

                    // Op190 后续每站都需要验证前一站是否 OK  
                    var dataSecond = DbContext.Db.Queryable<Albert_DataSecond>()
                        .First(x => x.ShellCode == rfidModel.ShellCode);

                    if (dataSecond != null)
                    {
                        // 如果托盘绑定了壳体码，这边直接放入，后面要用到
                        deviceSeq.ReadDataDic.AddOrUpdate("ShellCode", rfidModel?.ShellCode);

                        if (dataSecond.OpFinalResult == "NG")
                        {
                            // 这些可能会被重写
                            deviceSeq.ReadDataDic.AddOrUpdate("OpFinalResult", "NG");
                            deviceSeq.ReadDataDic.AddOrUpdate(deviceSeq.SeqName + "Result", "NG");
                        }
                        else
                        {
                            // 这些可能会被重写
                            deviceSeq.ReadDataDic.AddOrUpdate("OpFinalResult", "OK");
                            deviceSeq.ReadDataDic.AddOrUpdate(deviceSeq.SeqName + "Result", "OK");
                        }

                        #region 屏蔽工站逻辑(根据 Redis 缓存获取工站列表)
                        // 从缓存中获取数据
                        var station = _cacheService
                            .Get<List<Albert_PdmCraftDevice>>(CacheConst.CraftStationList)
                            ?.Where(x => x.DeviceDBName == deviceSeq.SeqName)
                            .First();

                        // 没有缓存从数据库获取
                        if (station == null)
                        {
                            var stationList = DbContext.Db
                                .Queryable<Albert_PdmCraftDevice>()
                                .Where(x => x.DeviceDBName ==
                                    deviceSeq.SeqName);

                            // 写入缓存中
                            _cacheService.AddObject(
                                CacheConst.CraftStationList, stationList);

                            station = stationList.First();
                        }

                        // 工站屏蔽了,直接发 2，让其流出
                        if (station?.DeviceDBIsUse == "N")
                        {
                            // NG 件 [允许工作1允许，2NG]
                            WriteData(deviceSeq.StationAllow, "2", out _);
                            $"{deviceSeq.SeqName}【工站屏蔽了,直接 NG 流出】".LogWarning();
                        }
                        else
                        {
                            // 没有屏蔽工站，则按照正常逻辑进行
                            var finalResult = (dataSecond.OpFinalResult == "OK") ? "1" : "2";
                            WriteData(deviceSeq.StationAllow, finalResult, out _);
                            $"{deviceSeq.SeqName}【rfid 标签上升沿反馈】{finalResult}".LogInformation();
                        }
                        #endregion
                    }
                    // ToDo:Debug 测试时放开
                    // WriteData(deviceSeq.StationAllow, "1", out _);
                    else
                    {
                        // 未根据产品码查询到相关数据，直接 NG
                        WriteData(deviceSeq.StationAllow, "2", out _);
                        $"{deviceSeq.SeqName}未根据产品码查询到相关数据，直接 NG".LogError();
                    }
                }

               
            }
        }
    }

    private void PrintZpl()
    {
        var zpl = _cacheService.Get("Zpl");

        _tcpClient.Connected = (client, e) => { };//成功连接到服务器
        _tcpClient.Disconnecting = (client, e) => { };//即将从服务器断开连接。此处仅主动断开才有效。
        _tcpClient.Disconnected = (client, e) => { };//从服务器断开连接，当连接不成功时不会触发。
        _tcpClient.Received = (client, byteBlock, requestInfo) =>
        {
            //从服务器收到信息。但是一般byteBlock和requestInfo会根据适配器呈现不同的值。
            string mes = Encoding.UTF8.GetString(byteBlock.Buffer, 0, byteBlock.Len);
            _tcpClient.Logger.Info($"客户端接收到信息：{mes}");
        };

        //载入配置
        _tcpClient.Setup(new TouchSocketConfig()
            .SetRemoteIPHost("192.168.1.115:9100")
            .ConfigureContainer(a =>
            {
                // 两种日志方式
                a.SetSingletonLogger(new LoggerGroup(new FileLogger(), new EasyLogger(PrintClinetLog)));
            }));

        Result result = _tcpClient.TryConnect();//或者可以调用TryConnect
        if (result.IsSuccess())
        {
            _tcpClient.Logger.Info("客户端成功连接");
        }
        else
        {
            _tcpClient.Logger.Info("客户端成功失败");
        }

        // 清除贴标
        //_tcpClient.Send("~JA");
        _tcpClient.Send(zpl);
        "贴标完成".LogInformation();
    }

    /// <summary>
    /// 这个主要用于 Op10 数据，节拍和加工时间绑定到 rfid 表上，方便 Op20 站移动
    /// </summary>
    /// <param name="deviceSeq"></param>
    /// <returns></returns>
    private async Task SqlOperateNone(DeviceSeq deviceSeq, SiemensS7Net plc)
    {
        // 1. 从 Plc 中读取 ReadData 对应的地址填充到 ReadDataDic 中
        ReadDataFromPlc(deviceSeq, plc);

        // 用于第一站数据，节拍和加工时间绑定到 rfid 表上，方便 Op20 站搬运数据
        if (deviceSeq.SeqName == "Op10")
        {
            deviceSeq.ReadDataDic.AddOrUpdate("RFIDIsUse", 1);
            deviceSeq.ReadDataDic.AddOrUpdate("Op10Time", DateTime.Now);
            deviceSeq.ReadDataDic.AddOrUpdate("Op10Result", "OK");
        }

        if (deviceSeq.SeqName == "Op150")
        {
            deviceSeq.ReadDataDic.AddOrUpdate("RFIDIsUse", 1);
            deviceSeq.ReadDataDic.AddOrUpdate("Op150Time", DateTime.Now);
            deviceSeq.ReadDataDic.AddOrUpdate("Op150Result", "OK");
            deviceSeq.ReadDataDic.AddOrUpdate("Bearing", "N"); // 轴承
            deviceSeq.ReadDataDic.AddOrUpdate("Case", "Y"); // 壳体
            deviceSeq.ReadDataDic.AddOrUpdate("SteelBall", "Y"); // 钢球
            deviceSeq.ReadDataDic.AddOrUpdate("PlugCap", "Y"); // 堵帽
            deviceSeq.ReadDataDic.AddOrUpdate("Spring", "Y"); // 弹簧
        }

        if (deviceSeq.SeqName == "Op160")
        {
            deviceSeq.ReadDataDic.AddOrUpdate("Bearing", "Y"); // 轴承
            deviceSeq.ReadDataDic.AddOrUpdate("Op160Time", DateTime.Now);
            deviceSeq.ReadDataDic.AddOrUpdate("Op160Result", "OK");
        }

        if (deviceSeq.SeqName == "Op170")
        {
            deviceSeq.ReadDataDic.AddOrUpdate("Op170Time", DateTime.Now);
            deviceSeq.ReadDataDic.AddOrUpdate("Op170Result", "OK");
        }

        if (deviceSeq.SeqName == "Op180_1")
        {
            deviceSeq.ReadDataDic.AddOrUpdate("Op180_1Time", DateTime.Now);
            deviceSeq.ReadDataDic.AddOrUpdate("Op180_1Result", "OK");
        }

        if (deviceSeq.SeqName == "Op180_2")
        {
            deviceSeq.ReadDataDic.AddOrUpdate("Op180_2Time", DateTime.Now);
            deviceSeq.ReadDataDic.AddOrUpdate("Op180_2Result", "OK");
        }

        var line = await DbContext.Db.Updateable(deviceSeq.ReadDataDic)
            .AS("Albert_RFID").WhereColumns("RFID").ExecuteCommandAsync();

        if (line > 0)
        {
            // 3. 读取完毕，打印日志
            (deviceSeq.SeqName + "【RFID 表更新】完成").LogInformation();
        }
        else
        {
            (deviceSeq.SeqName + "【RFID 表更新】失败").LogError();
        }


    }

    /// <summary>
    /// 将 ReadDataDic 中的表批量插入到数据库中
    /// </summary>
    /// <param name="deviceSeq"></param>
    private async Task SqlOperateInsert(DeviceSeq deviceSeq,SiemensS7Net plc)
    {
        ReadDataFromPlc(deviceSeq, plc);

        // OpFinalStation OpFinalDate 两个字段必写
        deviceSeq.ReadDataDic.AddOrUpdate("OpFinalStation", deviceSeq.SeqName);
        deviceSeq.ReadDataDic.AddOrUpdate("OpFinalDate", DateTime.Now);
        // 当前站加工时间
        deviceSeq.ReadDataDic.AddOrUpdate(deviceSeq.SeqName+"Time", DateTime.Now);
        // 这些可能被覆写，当前站结果+最终站结果
        deviceSeq.ReadDataDic.AddOrUpdate(deviceSeq.SeqName + "Result","OK");
        deviceSeq.ReadDataDic.AddOrUpdate("OpFinalResult", "OK");
        // 这边会进行 Aop 拦截，用复写方法可以实现重载
        // 在拦截里进行判断是否有错误，如果错误，则需要修改 OpFinalResult
        await SqlOperateInsertAop(deviceSeq);
        
        try
        {
            int line = 0;

            if (deviceSeq.DeviceId == 1)
            {
                line = await DbContext.Db.Insertable(deviceSeq.ReadDataDic)
                    .AS("Albert_DataFirst").ExecuteCommandAsync();
            }
            else
            {
                line = await DbContext.Db.Insertable(deviceSeq.ReadDataDic)
                    .AS("Albert_DataSecond").ExecuteCommandAsync();
            }
           

            if (line > 0)
            {
                //读取完毕，打印日志
                (deviceSeq.SeqName + "【插入数据】完成").LogInformation();
            }
            else
            {
                (deviceSeq.SeqName + "【插入数据】失败").LogInformation();
            }
            
        }
        catch (Exception ex)
        {
            $"【插入数据】失败{ex.Message}".LogError();
        }
    }

    private async Task SqlOperateUpdate(DeviceSeq deviceSeq, SiemensS7Net plc)
    {
        try
        {
            int line = 0;

            // 托盘上肯定绑定了产品码
            if (deviceSeq.DeviceId == 1)
            {
                if (deviceSeq.ReadDataDic.TryGetValue("ProductCode", out var productCode))
                {
                    var dataFirst = DbContext.Db.Queryable<Albert_DataFirst>()
                        .First(x => x.ProductCode == productCode.ToString());

                    if (dataFirst != null)
                    {
                        // 如果最终结果是 NG，直接不读取数据
                        if (dataFirst.OpFinalResult == "NG")
                        {
                            (deviceSeq.SeqName + "【上一站结果是NG】不更新最终站、不读取数据，直接发 1").LogInformation();
                        }
                        // 如果最终结果是 OK，更新最终站+读取数据
                        else
                        {
                            deviceSeq.ReadDataDic.AddOrUpdate("OpFinalStation", deviceSeq.SeqName);
                            ReadDataFromPlc(deviceSeq, plc);
                        }
                    }
                    else
                    {
                        deviceSeq.ReadDataDic.AddOrUpdate("OpFinalStation", deviceSeq.SeqName);
                        ReadDataFromPlc(deviceSeq, plc);
                    }
                }
                else
                {
                    deviceSeq.ReadDataDic.AddOrUpdate("OpFinalStation", deviceSeq.SeqName);
                    ReadDataFromPlc(deviceSeq, plc);
                }

                deviceSeq.ReadDataDic.AddOrUpdate("OpFinalDate", DateTime.Now);
                deviceSeq.ReadDataDic.AddOrUpdate(deviceSeq.SeqName + "Time", DateTime.Now);

                await SqlOperateUpdateAop(deviceSeq);

                // 数据更新,以 ProductCode
                line = await DbContext.Db.Updateable(deviceSeq.ReadDataDic)
                    .AS("Albert_DataFirst").WhereColumns("ProductCode").ExecuteCommandAsync();
            }

            if (deviceSeq.DeviceId == 2)
            {
                if (deviceSeq.ReadDataDic.TryGetValue("ShellCode", out var shellCode))
                {
                    var dataSecond = DbContext.Db.Queryable<Albert_DataSecond>()
                        .First(x => x.ShellCode == shellCode.ToString());

                    if (dataSecond != null)
                    {
                        // 如果最终结果是 NG，直接不读取数据
                        if (dataSecond.OpFinalResult == "NG")
                        {
                            (deviceSeq.SeqName + "【上一站结果是 NG】不更新最终站、不读取数据，直接发 1").LogInformation();
                        }
                        // 如果最终结果是 OK，更新最终站+读取数据
                        else
                        {
                            deviceSeq.ReadDataDic.AddOrUpdate("OpFinalStation", deviceSeq.SeqName);
                            ReadDataFromPlc(deviceSeq, plc);
                        }
                    }
                    else
                    {
                        deviceSeq.ReadDataDic.AddOrUpdate("OpFinalStation", deviceSeq.SeqName);
                        ReadDataFromPlc(deviceSeq, plc);
                    }
                }
                else
                {
                    deviceSeq.ReadDataDic.AddOrUpdate("OpFinalStation", deviceSeq.SeqName);
                    ReadDataFromPlc(deviceSeq, plc);
                }

                deviceSeq.ReadDataDic.AddOrUpdate("OpFinalDate", DateTime.Now);
                deviceSeq.ReadDataDic.AddOrUpdate(deviceSeq.SeqName + "Time", DateTime.Now);

                await SqlOperateUpdateAop(deviceSeq);

                // 数据更新,以 ShellCode
                line = await DbContext.Db.Updateable(deviceSeq.ReadDataDic)
                    .AS("Albert_DataSecond").WhereColumns("ShellCode").ExecuteCommandAsync();
            }

            if (line > 0)
            {
                (deviceSeq.SeqName + "【更新数据】完成").LogInformation();
            }
            else
            {
                (deviceSeq.SeqName + "【更新数据】失败").LogError();
            }
        }
        catch (Exception ex)
        {
            $"【出现异常】{ex.Message}".LogError();
        }
       
    }

    /// <summary>
    /// 最终的拦截
    /// </summary>
    /// <param name="deviceSeq"></param>
    /// <returns></returns>
    public virtual async Task SqlOperateFinalAop(DeviceSeq deviceSeq)
    {

    }


    /// <summary>
    /// 虚方法来实现插入到不同表的操作
    /// </summary>
    /// <param name="deviceSeq"></param>
    public virtual async Task SqlOperateInsertAop(DeviceSeq deviceSeq)
    {
    }

    /// <summary>
    /// 虚方法来实现更新到不同表的操作
    /// </summary>
    /// <param name="deviceSeq"></param>
    public virtual async Task SqlOperateUpdateAop(DeviceSeq deviceSeq)
    {
    }

    /// <summary>
    /// 从 Plc 中读取 ReadData 对应的地址填充到 ReadDataDic 中
    /// </summary>
    /// <param name="deviceSeq"></param>
    /// <param name="plc"></param>
    private void ReadDataFromPlc(DeviceSeq deviceSeq, SiemensS7Net plc)
    {
        // 读逻辑
        var readTypeArr = deviceSeq.ReadData
            .Select(x => x.TypeAndDb.Split(',')[1])
            .ToArray();
        var readDataLength = deviceSeq.ReadData.Select(x =>
        {
            var parts = x.TypeAndDb.Split(',');
            switch (parts[0])
            {
                case DeviceConst.DEV_TYPE_BOOL:
                case DeviceConst.DEV_TYPE_BYTE:
                    return (ushort)1;
                case DeviceConst.DEV_TYPE_SHORT:
                case DeviceConst.DEV_TYPE_USHORT:
                    return (ushort)2;
                case DeviceConst.DEV_TYPE_INT:
                case DeviceConst.DEV_TYPE_UINT:
                case DeviceConst.DEV_TYPE_FLOAT:
                    return (ushort)4;
                case DeviceConst.DEV_TYPE_LONG:
                case DeviceConst.DEV_TYPE_ULONG:
                case DeviceConst.DEV_TYPE_DOUBLE:
                    return (ushort)8;
                case DeviceConst.DEV_TYPE_STRING:
                    return ushort.Parse(parts[2]);
                default:
                    return (ushort)0; // 返回默认值或抛出异常，具体根据需求决定
            }
        }).ToArray();

        var readResult = plc.Read(readTypeArr, readDataLength);

        if (readResult.IsSuccess)
        {
            var i = 0;
            deviceSeq.ReadData.ForEach(x =>
            {
                var str = "";
                switch (x.TypeAndDb.Split(',')[0])
                {
                    case DeviceConst.DEV_TYPE_BOOL:
                        str = plc.ByteTransform.TransBool(
                                readResult.Content,
                                i == 0
                                    ? 0
                                    : readDataLength.Take(i)
                                        .Aggregate(
                                            (q, w) => (ushort)(q + w)))
                            .ToString();
                        break;
                    case DeviceConst.DEV_TYPE_BYTE:
                        str = plc.ByteTransform.TransByte(
                                readResult.Content,
                                i == 0
                                    ? 0
                                    : readDataLength.Take(i)
                                        .Aggregate(
                                            (q, w) => (ushort)(q + w)))
                            .ToString();
                        break;
                    case DeviceConst.DEV_TYPE_SHORT:
                        str = plc.ByteTransform.TransInt16(
                                readResult.Content,
                                i == 0
                                    ? 0
                                    : readDataLength.Take(i)
                                        .Aggregate(
                                            (q, w) => (ushort)(q + w)))
                            .ToString();
                        break;
                    case DeviceConst.DEV_TYPE_USHORT:
                        str = plc.ByteTransform.TransUInt16(
                                readResult.Content,
                                i == 0
                                    ? 0
                                    : readDataLength.Take(i)
                                        .Aggregate(
                                            (q, w) => (ushort)(q + w)))
                            .ToString();
                        break;
                    case DeviceConst.DEV_TYPE_INT:
                        str = plc.ByteTransform.TransInt32(
                                readResult.Content,
                                i == 0
                                    ? 0
                                    : readDataLength.Take(i)
                                        .Aggregate(
                                            (q, w) => (ushort)(q + w)))
                            .ToString();
                        break;
                    case DeviceConst.DEV_TYPE_UINT:
                        str = plc.ByteTransform.TransUInt32(
                                readResult.Content,
                                i == 0
                                    ? 0
                                    : readDataLength.Take(i)
                                        .Aggregate(
                                            (q, w) => (ushort)(q + w)))
                            .ToString();
                        break;
                    case DeviceConst.DEV_TYPE_LONG:
                        str = plc.ByteTransform.TransInt64(
                                readResult.Content,
                                i == 0
                                    ? 0
                                    : readDataLength.Take(i)
                                        .Aggregate(
                                            (q, w) => (ushort)(q + w)))
                            .ToString();
                        break;
                    case DeviceConst.DEV_TYPE_ULONG:
                        str = plc.ByteTransform.TransUInt64(
                                readResult.Content,
                                i == 0
                                    ? 0
                                    : readDataLength.Take(i)
                                        .Aggregate(
                                            (q, w) => (ushort)(q + w)))
                            .ToString();
                        break;
                    case DeviceConst.DEV_TYPE_FLOAT:
                        str = plc.ByteTransform.TransSingle(
                                readResult.Content,
                                i == 0
                                    ? 0
                                    : readDataLength.Take(i)
                                        .Aggregate(
                                            (q, w) => (ushort)(q + w)))
                            .ToString();
                        break;
                    case DeviceConst.DEV_TYPE_DOUBLE:
                        str = plc.ByteTransform.TransDouble(
                                readResult.Content,
                                i == 0
                                    ? 0
                                    : readDataLength.Take(i)
                                        .Aggregate(
                                            (q, w) => (ushort)(q + w)))
                            .ToString();
                        break;
                    case DeviceConst.DEV_TYPE_STRING:
                        str = Encoding.ASCII.GetString(readResult.Content,
                            i == 0
                                ? 0
                                : readDataLength.Take(i)
                                    .Aggregate((q, w) => (ushort)(q + w)),
                            int.Parse(x.TypeAndDb.Split(',')[2]));
                        break;
                    default:
                        break;
                }

                i++;

                // 添加读取数据到 ReadDataDic 中
                deviceSeq.ReadDataDic.AddOrUpdate(x.SqlColumnName, str);
            });
        }
    }

    private void UpdateDataFromPlc(DeviceSeq deviceSeq)
    {
        var updateTypeArr = deviceSeq.UpdateData;
        updateTypeArr?.ForEach(x =>
        {
            // 1.从 plc 读取数据
            ReadData(x.TypeAndDb, out var result);
            // 2. 添加到 where 条件更新表中
            deviceSeq.UpdateDataDic.AddOrUpdate(x.SqlColumnName, result);
        });
    }

    private async Task TransDataToDic(DeviceSeq deviceSeq)
    {
        var writeTypeArr = deviceSeq.WriteData;
        writeTypeArr?.ForEach(x =>
        {
            deviceSeq.WriteDataDic.AddOrUpdate(x.SqlColumnName, "");
        });
        // Aop 拦截，追加或改变一些值
        await WriteDataDicAop(deviceSeq);
    }

    /// <summary>
    /// 写数据到 Plc 中
    /// </summary>
    /// <param name="deviceSeq"></param>
    private void WriteDataToPlc(DeviceSeq deviceSeq)
    {
        deviceSeq.WriteDataDic.ForEach(x => {
            var plcAddress = deviceSeq.WriteData.FirstOrDefault(y => y.SqlColumnName == x.Key);
            if (plcAddress != null)
            {
                WriteData(plcAddress.TypeAndDb, x.Value.ToString(), out _);
            }
        });
    }

    /// <summary>
    /// 虚方法来实现更新到不同表的操作
    /// </summary>
    /// <param name="deviceSeq"></param>
    public virtual async Task WriteDataDicAop(DeviceSeq deviceSeq)
    {
    }

    /// <summary>
    /// 停止工站
    /// </summary>
    /// <param name="stationName"></param>
    /// <returns>true 代表停止成功</returns>
    public override bool StopStation(string stationName)
    {
        var station = _device.StationSeqs.FirstOrDefault(x => x.SeqName == stationName);
        if (station == null)
        {
            BackMessage.AddMessage(DeviceConst.DEV_Station_FIND_NG, LogLevel.Error);
            return false;
        }
        else
        {
            if (station.IsOpen)
            {
                station.IsOpen = false;
                station.StopStationTokenSource?.Cancel();
                station.StopStationTokenSource?.Dispose();
                return true;
            }

            return true;
        }
    }

    #endregion

    #region 数据读取

    /// <summary>
    /// 统一的读取结果的数据解析，显示
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="result"></param>
    /// <param name="address"></param>
    /// <returns></returns>
    private string ReadResultRender<T>(OperateResult<T> result, string address)
    {
        if (result.IsSuccess)
        {
            return result.Content.ToString();
        }
        else
        {
            (address + " 读取失败-原因：" + result.ToMessageShowString()).LogError();
            return "";
        }
    }

    /// <summary>
    /// 读取数据
    /// </summary>
    /// <param name="strAddress"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public override BaseCommunication ReadData(string strAddress, out string result)
    {
        if (!_device.IsOpen)
        {
            BackMessage.AddMessage(DeviceConst.DEV_STATUS_DISCONNECT, LogLevel.Error);
            result = "";
            return this;
        }

        var str = strAddress.Split(',');

        if (str.Length >= 3)
        {
            // 字符串读取
            ReadDataCommon(str[0], str[1], str[2], out result);
        }
        else
        {
            // 其他类型读取
            ReadDataCommon(str[0], str[1], "0", out result);
        }
       
        return this;
    }

    /// <summary>
    /// 读取数据（通用版）
    /// </summary>
    /// <param name="strType"></param>
    /// <param name="address"></param>
    /// <param name="strReadUshort">读取字符串的长度</param>
    /// <param name="result"></param>
    /// <returns></returns>
    public override BaseCommunication ReadDataCommon(string strType, string address, string strReadUshort,
        out string result)
    {
        if (!_device.IsOpen)
        {
            BackMessage.AddMessage(DeviceConst.DEV_STATUS_DISCONNECT, LogLevel.Error);
            ;
            result = "";
            return this;
        }

        var strReturn = string.Empty;
        switch (strType)
        {
            case DeviceConst.DEV_TYPE_BOOL:
                strReturn = ReadResultRender(_device.SimTcpNet.ReadBool(address), address);
                break;
            case DeviceConst.DEV_TYPE_BYTE:
                strReturn = ReadResultRender(_device.SimTcpNet.ReadByte(address), address);
                break;
            case DeviceConst.DEV_TYPE_SHORT:
                strReturn = ReadResultRender(_device.SimTcpNet.ReadInt16(address), address);
                break;
            case DeviceConst.DEV_TYPE_USHORT:
                strReturn = ReadResultRender(_device.SimTcpNet.ReadUInt16(address), address);
                break;
            case DeviceConst.DEV_TYPE_INT:
                strReturn = ReadResultRender(_device.SimTcpNet.ReadInt32(address), address);
                break;
            case DeviceConst.DEV_TYPE_UINT:
                strReturn = ReadResultRender(_device.SimTcpNet.ReadUInt32(address), address);
                break;
            case DeviceConst.DEV_TYPE_LONG:
                strReturn = ReadResultRender(_device.SimTcpNet.ReadInt64(address), address);
                break;
            case DeviceConst.DEV_TYPE_ULONG:
                strReturn = ReadResultRender(_device.SimTcpNet.ReadUInt64(address), address);
                break;
            case DeviceConst.DEV_TYPE_FLOAT:
                strReturn = ReadResultRender(_device.SimTcpNet.ReadFloat(address), address);
                break;
            case DeviceConst.DEV_TYPE_DOUBLE:
                strReturn = ReadResultRender(_device.SimTcpNet.ReadDouble(address), address);
                break;
            case DeviceConst.DEV_TYPE_STRING:
                strReturn = ReadResultRender(_device.SimTcpNet.ReadString(address, ushort.Parse(strReadUshort)),
                    address);
                break;
            default:
                break;
        }

        result = strReturn.Replace("\0", "").Trim();
        return this;
    }

    #endregion

    #region 数据写入

    /// <summary>
    /// 统一的数据写入的结果显示
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    private bool WriteResultRender(OperateResult result)
    {
        if (!result.IsSuccess) result.ToMessageShowString().LogError();
        return result.IsSuccess;
    }

    /// <summary>
    /// 统一的数据写入的结果显示
    /// </summary>
    /// <param name="strAddress"></param>
    /// <param name="setValue"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public override BaseCommunication WriteData(string strAddress, string setValue, out bool result)
    {
        if (!_device.IsOpen)
        {
            BackMessage.AddMessage(DeviceConst.DEV_STATUS_DISCONNECT, LogLevel.Error);
            result = false;
            return this;
        }

        var str = strAddress.Split(',');
        WriteDataCommon(str[0], str[1], setValue, out result);
        return this;
    }

    /// <summary>
    /// 写入通用版
    /// </summary>
    /// <param name="strType"></param>
    /// <param name="address"></param>
    /// <param name="setValue"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public override BaseCommunication WriteDataCommon(string strType, string address, string setValue, out bool result)
    {
        if (_device == null || !_device.IsOpen)
        {
            BackMessage.AddMessage(DeviceConst.DEV_STATUS_DISCONNECT, LogLevel.Error);
            result = false;
            return this;
        }

        var b_Return = false;
        try
        {
            switch (strType)
            {
                case DeviceConst.DEV_TYPE_BOOL:
                    b_Return = WriteResultRender(_device.SimTcpNet.Write(address, bool.Parse(setValue)));
                    break;
                case DeviceConst.DEV_TYPE_BYTE:
                    b_Return = WriteResultRender(_device.SimTcpNet.Write(address, byte.Parse(setValue)));
                    break;
                case DeviceConst.DEV_TYPE_SHORT:
                    b_Return = WriteResultRender(_device.SimTcpNet.Write(address, short.Parse(setValue)));
                    break;
                case DeviceConst.DEV_TYPE_USHORT:
                    b_Return = WriteResultRender(_device.SimTcpNet.Write(address, ushort.Parse(setValue)));
                    break;
                case DeviceConst.DEV_TYPE_INT:
                    b_Return = WriteResultRender(_device.SimTcpNet.Write(address, int.Parse(setValue)));
                    break;
                case DeviceConst.DEV_TYPE_UINT:
                    b_Return = WriteResultRender(_device.SimTcpNet.Write(address, uint.Parse(setValue)));
                    break;
                case DeviceConst.DEV_TYPE_LONG:
                    b_Return = WriteResultRender(_device.SimTcpNet.Write(address, long.Parse(setValue)));
                    break;
                case DeviceConst.DEV_TYPE_ULONG:
                    b_Return = WriteResultRender(_device.SimTcpNet.Write(address, ulong.Parse(setValue)));
                    break;
                case DeviceConst.DEV_TYPE_FLOAT:
                    b_Return = WriteResultRender(_device.SimTcpNet.Write(address, float.Parse(setValue)));
                    break;
                case DeviceConst.DEV_TYPE_DOUBLE:
                    b_Return = WriteResultRender(_device.SimTcpNet.Write(address, double.Parse(setValue)));
                    break;
                case DeviceConst.DEV_TYPE_STRING:
                    b_Return = WriteResultRender(_device.SimTcpNet.Write(address, setValue));
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            ("PLC写入异常：" + ex.Message).LogError();
            result = false;
            return this;
        }

        result = b_Return;
        return this;
    }

    #endregion

    #region 批量读取 返回byte[]

    /// <summary>
    /// 批量读取
    /// </summary>
    /// <param name="address">地址</param>
    /// <param name="readLength">读取长度</param>
    /// <param name="result">结果</param>
    /// <returns></returns>
    public override BaseCommunication ReadByteArray(string address, string readLength, out byte[] result)
    {
        if (!_device.IsOpen)
        {
            BackMessage.AddMessage(DeviceConst.DEV_STATUS_DISCONNECT, LogLevel.Error);
            result = null;
            return this;
        }

        var bt_Read = new byte[int.Parse(readLength)];
        try
        {
            var read = _device.SimTcpNet.Read(address, ushort.Parse(readLength));
            if (read.IsSuccess)
                bt_Read = read.Content;
            else
                ("批量读取失败：" + read.ToMessageShowString()).LogError();
        }
        catch (Exception ex)
        {
            ("批量读取失败：" + ex.Message).LogError();
            result = null;
            return this;
        }

        result = bt_Read;
        return this;
    }

    #endregion
}