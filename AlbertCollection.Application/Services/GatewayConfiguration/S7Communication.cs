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

namespace AlbertCollection.Application.Services.GatewayConfiguration;

/// <summary>
/// 西门子通讯
/// </summary>
public class S7Communication : BaseCommunication
{
    public DeviceCollection _device = new();


    /// <summary>
    /// 构造函数，初始化西门子读写
    /// </summary>
    /// <param name="device"></param>
    public S7Communication(DeviceCollection device)
    {
        _device = device;
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

    private void SendHeartBeat(CancellationToken cancellationToken)
    {
        _device.IsHeartbeatOpen = true;
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
                        }
                        else
                        {
                            if (result.ErrorCode < 0)
                            {
                                _device.IsHeartbeatOpen = false;
                                BackMessage.AddMessage(("HeartBeatOpen" + result.Message + DateTime.Now), LogLevel.Error);
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
                                                (deviceSeq.SeqName+ "【plc-mes】 标签上升沿开始").LogInformation();
                                                // 发生了上升沿，需要完成两步
                                                // 1. 读取 Rfid
                                                ReadData(deviceSeq.RfidLabel,out var rfid);
                                                // 发生了上升沿，两步交互逻辑
                                                // 读成功写两步.1.上升沿写 false，响应地址写 true
                                                await plc.WriteAsync(deviceSeq.RfidRisingEdge, false);
                                                (deviceSeq.SeqName + "【plc-mes】 标签上升沿 结束").LogInformation();

                                                if (string.IsNullOrEmpty(rfid))
                                                {
                                                    (deviceSeq.SeqName+"rfid 未读取到").LogError();
                                                }
                                                else
                                                {

                                                    var rfidModel = DbContext.Db.Queryable<Albert_RFID>().First(x=>x.RFID.ToString() == rfid);
                                                    (deviceSeq.SeqName + "【Rfid 值】" + rfid).LogInformation();
                                                    
                                                    deviceSeq.ReadDataDic.AddOrUpdate("RFID", rfid);
                                                    
                                                    // 不为 0,表示被占用，直接给出错误和 NG
                                                    if (rfidModel?.RFIDIsUse != 0)
                                                    {
                                                        "托盘被占用，请重新选择".LogError();
                                                        // 直接 NG [允许工作1允许，2NG，3下线]
                                                        WriteData(deviceSeq.StationAllow, "2", out var allowResult);
                                                        (deviceSeq.SeqName + "【托盘被占用】" + allowResult).LogInformation();
                                                    }
                                                   
                                                    if (string.IsNullOrEmpty(rfidModel?.ProductCode))
                                                    {
                                                        // ToDo:PLC 异常的时候产品码也可能为空
                                                        // 2.1 查询产品码为空，则说明是第一站,直接放行 [允许工作1允许，2NG，3下线]
                                                        WriteData(deviceSeq.StationAllow, "1", out var allowResult);
                                                        (deviceSeq.SeqName + "【是否运行工作】-OK").LogInformation();
                                                    }
                                                    else
                                                    {
                                                        // 2.2 从数据库中查询产品码是否存在，如果存在再去线体查找是否可以做
                                                        // 这边需要两个线体查询逻辑
                                                        var dataFirst = DbContext.Db.Queryable<Albert_DataFirst>()
                                                            .First(x => x.ProductCode == rfidModel.ProductCode);
                                                        if (dataFirst != null)
                                                        {
                                                            // 如果托盘绑定了产品码，这边直接放入，后面要用到
                                                            deviceSeq.ReadDataDic.AddOrUpdate("ProductCode", dataFirst.ProductCode);

                                                            if (dataFirst.OpFinalResult == "OK")
                                                            {
                                                                // OK 件 [允许工作1允许，2NG，3下线]
                                                                WriteData(deviceSeq.StationAllow, "1", out var allowResult);
                                                                (deviceSeq.SeqName+"Rfid 结果" + allowResult).LogInformation();
                                                            }
                                                            else
                                                            {
                                                                // NG 件 [允许工作1允许，2NG，3下线]
                                                                WriteData(deviceSeq.StationAllow, "2", out var allowResult);
                                                                (deviceSeq.SeqName + "Rfid 结果" + allowResult).LogInformation();
                                                            }
                                                        }
                                                    }

                                                }

                                                // 读成功写两步.上升沿写 false，2.响应地址写 true
                                                await plc.WriteAsync(deviceSeq.RfidResponseEdge, true);
                                                (deviceSeq.SeqName + "MES-PLC 标签结束 true").LogInformation();
                                            }

                                            // 更新历史的值
                                            oldValue = read.Content ? 1 : 0;
                                        }
                                    }
                                }
                                deviceSeq.IsOpen = true;
                                await Task.Delay(20);
                            }
                        }
                        catch (Exception ex)
                        {
                            (deviceSeq.SeqName + ex.Message).LogError();
                            deviceSeq.IsOpen = false;
                            break;
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
                                            (deviceSeq.SeqName + "plc-mes 保存数据上升沿开始").LogInformation();
                                            // 读成功写两步.上升沿写 false，响应地址写 true
                                            await plc.WriteAsync(deviceSeq.RisingEdge, false);
                                            (deviceSeq.SeqName+"plc-mes 清除了保存数据上升沿").LogInformation();
                                            // 这边分为 批量读-插入数据库 批量读-更新数据库 写入PLC
                                            switch (deviceSeq.SqlOperate)
                                            {
                                                // 这种是第一站不需要插入数据到 DataFirst 中的情况
                                                case SqlOperateEnum.None:
                                                    // 1. 从 Plc 中读取 ReadData 对应的地址填充到 ReadDataDic 中
                                                    ReadDataFromPlc(deviceSeq, plc);
                                                    // 读取列表，绑定到 Rfid 上，等到 Op20 的时候再切换绑定
                                                    await SqlOperateNone(deviceSeq);
                                                    // 1. 读取完毕，打印日志
                                                    (deviceSeq.SeqName + "执行完毕").LogInformation();
                                                    break;
                                                // 数据插入，读取数据后直接插入 DataFirst 中，首站会用到
                                                case SqlOperateEnum.Insert:
                                                    // 1. 从 Plc 中读取 ReadData 对应的地址填充到 ReadDataDic 中
                                                    ReadDataFromPlc(deviceSeq, plc);
                                                    // 2. 将 ReadData 中的数据插入数据库（内部含有 Aop 拦截器）
                                                    await SqlOperateInsert(deviceSeq);
                                                    // 3. 读取完毕，打印日志
                                                    (deviceSeq.SeqName + "插入数据执行完毕").LogInformation();
                                                    break;
                                                case SqlOperateEnum.Update:
                                                    // 1. 从 Plc 中读取 ReadData 对应的地址填充到 ReadDataDic 中
                                                    ReadDataFromPlc(deviceSeq, plc);
                                                    // 2. 更新数据
                                                    await SqlOperateUpdate(deviceSeq);
                                                    // 3. 读取完毕，打印日志
                                                    (deviceSeq.SeqName + "更新数据执行完毕").LogInformation();
                                                    break;
                                                default:
                                                    (deviceSeq.SeqName + "未走任何分支，请检查配置文件").LogError();
                                                    break;
                                            }
                                            // 1. Op20 要更新 Rfid 表 label 和 product 的绑定关系
                                            await SqlOperateFinalAop(deviceSeq);

                                            (deviceSeq.SeqName + "保存数据结束").LogInformation();

                                            // 读成功写两步.上升沿写 false，响应地址写 true
                                            await plc.WriteAsync(deviceSeq.ResponseEdge, true);
                                            (deviceSeq.SeqName + "mes-plc 保存数据结束置位").LogInformation();
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
                            deviceSeq.IsOpen = false;
                            break;
                        }
                }, deviceSeq.StopStationTokenSource.Token);
            }
        }
        else
        {
            BackMessage.AddMessage(DeviceConst.DEV_Station_FIND_NG, LogLevel.Error);
        }
    }

    /// <summary>
    /// 这个主要用于第一站数据，节拍和加工时间绑定到 rfid 表上，方便 Op20 站移动
    /// </summary>
    /// <param name="deviceSeq"></param>
    /// <returns></returns>
    private async Task SqlOperateNone(DeviceSeq deviceSeq)
    {
        await SqlOperateNoneAop(deviceSeq);
    }

    /// <summary>
    /// None 的 Aop 拦截
    /// </summary>
    /// <param name="deviceSeq"></param>
    /// <returns></returns>
    public virtual async Task SqlOperateNoneAop(DeviceSeq deviceSeq)
    {

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
    /// 将 ReadDataDic 中的表批量插入到数据库中
    /// </summary>
    /// <param name="deviceSeq"></param>
    private async Task SqlOperateInsert(DeviceSeq deviceSeq)
    {
        // 这个必写
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
            await DbContext.Db.Insertable(deviceSeq.ReadDataDic)
                .AS("Albert_DataFirst").ExecuteCommandAsync();
        }
        catch (Exception ex)
        {
            ex.Message.LogError();
        }
    }

    private async Task SqlOperateUpdate(DeviceSeq deviceSeq)
    {
        // 这边会进行 Aop 拦截，用复写方法可以实现重载
        // 在拦截里进行判断是否有错误，如果没有直接写 OK 
        // 这些必写
        deviceSeq.ReadDataDic.AddOrUpdate("OpFinalStation", deviceSeq.SeqName);
        deviceSeq.ReadDataDic.AddOrUpdate("OpFinalDate", DateTime.Now);
        deviceSeq.ReadDataDic.AddOrUpdate(deviceSeq.SeqName + "Time", DateTime.Now);
 
        await SqlOperateUpdateAop(deviceSeq);

        // 数据更新,以 ProductCode
        var line = await DbContext.Db.Updateable(deviceSeq.ReadDataDic)
            .AS("Albert_DataFirst").WhereColumns("ProductCode").ExecuteCommandAsync();

        if (line <= 0)
        {
            (deviceSeq.SeqName+"未更新任何数据").LogError();
        }
        ////字典集合
        //var dtList = new List<Dictionary<string, object>>
        //{
        //    deviceSeq.ReadDataDic,
        //    deviceSeq.UpdateDataDic
        //};
        //await DbContext.Db.Updateable(dtList).AS(_device.SqlStore + "." + deviceSeq.SqlTable)
        //    .WhereColumns(deviceSeq.UpdateDataDic.Keys.ToArray())
        //    .ExecuteCommandAsync();
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