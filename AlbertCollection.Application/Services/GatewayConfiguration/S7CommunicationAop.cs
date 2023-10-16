﻿using AlbertCollection.Application.Cache;
using AlbertCollection.Application.Services.GatewayConfiguration.Dto;
using AlbertCollection.Core.Entity.Device;
using Furion.Logging.Extensions;
using HslCommunication.Enthernet;
using TouchSocket.Core;

namespace AlbertCollection.Application.Services.GatewayConfiguration
{
    /// <summary>
    /// 暴露给外界用，所有拦截操作都写在这里
    /// </summary>
    public class S7CommunicationAop : S7Communication
    {
        private readonly ICacheRedisService _cacheService;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="device"></param>
        /// <param name="cacheService"></param>
        public S7CommunicationAop(DeviceCollection device, ICacheRedisService cacheService) : base(device,cacheService)
        {
            _device = device;
            _cacheService = cacheService;
        }

        // 这种用来切换租户 ID
        // var test = DbContext.Db.GetConnectionScope("019").Queryable<object>().AS("app_transaction").ToList();

        /// <summary>
        /// 插入拦截器
        /// </summary>
        /// <param name="deviceSeq"></param>
        /// <returns></returns>
        public override async Task SqlOperateInsertAop(DeviceSeq deviceSeq)
        {
            if (deviceSeq.SeqName == "Op20")
            {
                if (deviceSeq.ReadDataDic.TryGetValue("RFID", out var rfid)
                    && deviceSeq.ReadDataDic.TryGetValue("ProductCode", out var productCode))
                {
                    var rfidModel = DbContext.Db.Queryable<Albert_RFID>().First(x => x.RFID.ToString() == rfid.ToString());

                    if (rfidModel != null)
                    {
                        // 1.更新 RFID 表
                        // 绑定 Rfid 和 产品码
                        rfidModel.ProductCode = productCode.ToString();
                        // 更新绑定关系
                        await DbContext.Db.Updateable(rfidModel)
                            .Where(it => it.RFID == rfid.ToInt(0))
                            .ExecuteCommandAsync();

                        // 2.更新产品表
                        // 从 _rfidModel 中获取 Op10Beat，Op10Result,Op10Time
                        deviceSeq.ReadDataDic.AddOrUpdate("Op10Beat", rfidModel.OP10Beat);
                        deviceSeq.ReadDataDic.AddOrUpdate("Op10Result", rfidModel.Op10Result);
                        deviceSeq.ReadDataDic.AddOrUpdate("Op10Time", rfidModel.Op10Time);
                    }
                    else
                    {
                        (deviceSeq.SeqName + "【数据库未查询到】RFID - " + rfid.ToString()).LogError();
                        _cacheService.LPush("MES-PLC 交互", (deviceSeq.SeqName + "【数据库未查询到】RFID - " + rfid.ToString()));
                    }
                }
                else
                {
                    (deviceSeq.SeqName+"【数据字典未查询到】RFID 或产品码").LogError();
                    _cacheService.LPush("MES-PLC 交互", (deviceSeq.SeqName + "【数据字典未查询到】RFID 或产品码"));
                }
            }
        }

        /// <summary>
        /// 更新拦截器
        /// </summary>
        /// <param name="deviceSeq"></param>
        /// <returns></returns>
        public override async Task SqlOperateUpdateAop(DeviceSeq deviceSeq)
        {
            // Op60 站压力、位移判断
            if (deviceSeq.SeqName == "Op60")
            {
                if (deviceSeq.ReadDataDic.TryGetValue("ProductCode", out var productCode))
                {
                    // 如果影响产品节拍改为异步
                    var srcFolder = App.GetConfig<string>("Op60PressFolder");
                    var filePath = RemoveFile(srcFolder, productCode.ToString());
                    deviceSeq.ReadDataDic.AddOrUpdate("Op60PressureFile",filePath);

                    if (deviceSeq.ReadDataDic.TryGetValue("Op60Pressure", out var pressure))
                    {
                        if ((pressure.ToString() == "2"))
                        {
                            deviceSeq.ReadDataDic.AddOrUpdate("OpFinalResult", "NG");
                            deviceSeq.ReadDataDic.AddOrUpdate(deviceSeq.SeqName + "Result", "NG");
                        }
                    }
                    else
                    {
                        deviceSeq.ReadDataDic.AddOrUpdate("OpFinalResult", "NG");
                        deviceSeq.ReadDataDic.AddOrUpdate(deviceSeq.SeqName + "Result", "NG");
                    }

                    if (deviceSeq.ReadDataDic.TryGetValue("Op60Displacement", out var displace))
                    {
                        if ((displace.ToString() == "2"))
                        {
                            deviceSeq.ReadDataDic.AddOrUpdate("OpFinalResult", "NG");
                            deviceSeq.ReadDataDic.AddOrUpdate(deviceSeq.SeqName + "Result", "NG");
                        }
                    }
                    else
                    {
                        deviceSeq.ReadDataDic.AddOrUpdate("OpFinalResult", "NG");
                        deviceSeq.ReadDataDic.AddOrUpdate(deviceSeq.SeqName + "Result", "NG");
                    }
                }
                else
                {
                    (deviceSeq.SeqName+ "【字典数据未查询到】产品码，无法重命名压机文件").LogError();
                    _cacheService.LPush("MES-PLC 交互", (deviceSeq.SeqName + "【字典数据未查询到】产品码，无法重命名压机文件"));

                    deviceSeq.ReadDataDic.AddOrUpdate("OpFinalResult", "NG");
                    deviceSeq.ReadDataDic.AddOrUpdate(deviceSeq.SeqName + "Result", "NG");
                }
            }

            // 这一站：将托盘上的料夹走（第一个托盘），后面的托盘会放前一个托盘的料
            if (deviceSeq.SeqName == "Op80")
            {
                // 80 解绑-第一次会报错，无需关心
                if (deviceSeq.ReadDataDic.TryGetValue("RFID", out var rfid)
                    && deviceSeq.ReadDataDic.TryGetValue("ProductCode", out var productCode))
                {
                    var tempDic = new Dictionary<string, object>();
                    tempDic.AddOrUpdate("RFID", rfid);
                    tempDic.AddOrUpdate("ProductCode", productCode);

                    var line = await DbContext.Db.Updateable(tempDic)
                        .AS("Albert_RFID").WhereColumns("RFID").ExecuteCommandAsync();

                    if (line > 0)
                    {
                        (deviceSeq.SeqName + "【更新数据】完成-RFID 表").LogInformation();
                    }
                    else
                    {
                        (deviceSeq.SeqName + "【更新数据】失败-RFID 表").LogError();
                        _cacheService.LPush("MES-PLC 交互", (deviceSeq.SeqName + "【更新数据】失败-RFID 表--80 解绑-第一次会报错，无需关心"));
                    }
                }
                else
                {
                    (deviceSeq.SeqName + "【字典数据未查询到】RFID or ProductCode").LogError();
                    _cacheService.LPush("MES-PLC 交互", (deviceSeq.SeqName + "【字典数据未查询到】RFID or ProductCode"));
                }
            }

            // Op90 站位移判断
            if (deviceSeq.SeqName == "Op90")
            {
                if (deviceSeq.ReadDataDic.TryGetValue("Op90DisplacementResult", out var displace))
                {
                    if ((displace.ToString() == "2"))
                    {
                        deviceSeq.ReadDataDic.AddOrUpdate("OpFinalResult", "NG");
                        deviceSeq.ReadDataDic.AddOrUpdate(deviceSeq.SeqName + "Result", "NG");
                    }
                }
                else
                {
                    deviceSeq.ReadDataDic.AddOrUpdate("OpFinalResult", "NG");
                    deviceSeq.ReadDataDic.AddOrUpdate(deviceSeq.SeqName + "Result", "NG");
                }

                if (deviceSeq.ReadDataDic.TryGetValue("Op90IV3Result", out var iv3Result))
                {
                    if ((iv3Result.ToString() == "2"))
                    {
                        deviceSeq.ReadDataDic.AddOrUpdate("OpFinalResult", "NG");
                        deviceSeq.ReadDataDic.AddOrUpdate(deviceSeq.SeqName + "Result", "NG");
                    }
                }
                else
                {
                    deviceSeq.ReadDataDic.AddOrUpdate("OpFinalResult", "NG");
                    deviceSeq.ReadDataDic.AddOrUpdate(deviceSeq.SeqName + "Result", "NG");
                }
            }

            if (deviceSeq.SeqName == "Op110")
            {
                if (deviceSeq.ReadDataDic.TryGetValue("Op110IV3Result", out var displace))
                {
                    if ((displace.ToString() == "2"))
                    {
                        deviceSeq.ReadDataDic.AddOrUpdate("OpFinalResult", "NG");
                        deviceSeq.ReadDataDic.AddOrUpdate(deviceSeq.SeqName + "Result", "NG");
                    }
                }
                else
                {
                    deviceSeq.ReadDataDic.AddOrUpdate("OpFinalResult", "NG");
                    deviceSeq.ReadDataDic.AddOrUpdate(deviceSeq.SeqName + "Result", "NG");
                }
            }

            // 140 解绑 RFID表中的 RFIDIsUse
            if (deviceSeq.SeqName == "Op140")
            {
                if (deviceSeq.ReadDataDic.TryGetValue("RFID", out var rfid))
                {
                    var tempDic = new Dictionary<string, object>();
                    tempDic.AddOrUpdate("RFID", rfid);
                    tempDic.AddOrUpdate("RFIDIsUse", 0);

                    var line = await DbContext.Db.Updateable(tempDic)
                        .AS("Albert_RFID").WhereColumns("RFID").ExecuteCommandAsync();

                    if (line > 0)
                    {
                        (deviceSeq.SeqName + "【更新数据】完成-RFID 表").LogInformation();
                    }
                    else
                    {
                        (deviceSeq.SeqName + "【更新数据】失败-RFID 表").LogError();
                        _cacheService.LPush("MES-PLC 交互", (deviceSeq.SeqName + "【更新数据】失败-RFID 表"));
                    }
                }
                else
                {
                    (deviceSeq.SeqName + "【字典数据未查询到】 RFID").LogError();
                    _cacheService.LPush("MES-PLC 交互", (deviceSeq.SeqName + "【字典数据未查询到】 RFID"));
                }
            }
        }

        /// <summary>
        /// 重命名
        /// </summary>
        /// <param name="srcFolder"></param>
        private string RemoveFile(string srcFolder, string productCode)
        {
            try
            {
                string[] fileNames = Directory.GetFiles(srcFolder);
                Array.Sort(fileNames, (a, b) => File.GetLastWriteTime(b).CompareTo(File.GetLastWriteTime(a)));

                if (fileNames.Length > 0)
                {
                    string latestFileName = fileNames[0];
                    string newFileName = $"{productCode}-{Path.GetFileName(latestFileName)}";

                    File.Move(latestFileName, Path.Combine(srcFolder, newFileName));

                    $"【压机文件重命名】已将文件 {latestFileName} 重命名为 {newFileName}.".LogInformation();
                    return srcFolder +"\\"+ newFileName;
                }
                else
                {
                    "【压机文件重命名】失败，目录中没有文件.".LogError();
                    _cacheService.LPush("MES-PLC 交互", "【压机文件重命名】失败，目录中没有文件.");
                    return "";
                }
            }
            catch (Exception ex)
            {
                ex.Message.LogError();
                return "";
            }
        }
    }
}
