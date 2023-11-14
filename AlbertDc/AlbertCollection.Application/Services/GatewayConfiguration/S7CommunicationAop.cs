using AlbertCollection.Application.Cache;
using AlbertCollection.Application.Services.Driver.Dto;
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

        /// <summary>
        /// 插入拦截器
        /// </summary>
        /// <param name="deviceSeq"></param>
        /// <returns></returns>
        public override async Task SqlOperateInsertAop(DeviceSeq deviceSeq)
        {
            // 将 Op10 的数据从 rfid 表绑定到数据大表上 Albert_RFID
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
                        deviceSeq.ReadDataDic.AddOrUpdate("Op10Beat", rfidModel.Op10Beat);
                        deviceSeq.ReadDataDic.AddOrUpdate("Op10Result", rfidModel.Op10Result);
                        deviceSeq.ReadDataDic.AddOrUpdate("Op10Time", rfidModel.Op10Time);
                    }
                    else
                    {
                        (deviceSeq.SeqName + "【数据库未查询到】RFID - " + rfid.ToString()).LogError();
                    }
                }
                else
                {
                    (deviceSeq.SeqName+"【数据字典未查询到】RFID 或产品码").LogError();
                }
            }

            // 插入数据，将托盘上 Op150、Op160、Op170、Op180_1、Op180_2 等数据转移到壳体码上 壳体码和执行器码绑定
            if (deviceSeq.SeqName == "Op180_3")
            {
                if (deviceSeq.ReadDataDic.TryGetValue("RFID", out var rfid)
                    && deviceSeq.ReadDataDic.TryGetValue("ShellCode", out var shellCode)
                    && deviceSeq.ReadDataDic.TryGetValue("ProductCode", out var productCode))
                {
                    #region  二码合一，取走
                    var rfidModel = DbContext.Db.Queryable<Albert_RFID>().First(x => x.RFID.ToString() == rfid.ToString());

                    if (rfidModel != null)
                    {
                        // 1.更新 Rfid 表：绑定 Rfid 和 产品码，更新绑定关系
                        rfidModel.ShellCode = shellCode.ToString();
                        rfidModel.ProductCode = productCode.ToString();

                        await DbContext.Db.Updateable(rfidModel)
                            .Where(it => it.RFID == rfid.ToInt(0))
                            .ExecuteCommandAsync();

                        // 2.更新产品表：从 _rfidModel 中获取 Op150 Op160 Op170 Op180_1 Op180_2 数据
                        deviceSeq.ReadDataDic.AddOrUpdate("Op150Beat", rfidModel.Op150Beat);
                        deviceSeq.ReadDataDic.AddOrUpdate("Op150Result", rfidModel.Op150Result);
                        deviceSeq.ReadDataDic.AddOrUpdate("Op150Time", rfidModel.Op150Time);

                        deviceSeq.ReadDataDic.AddOrUpdate("Op160Beat", rfidModel.Op160Beat);
                        deviceSeq.ReadDataDic.AddOrUpdate("Op160Result", rfidModel.Op160Result);
                        deviceSeq.ReadDataDic.AddOrUpdate("Op160Time", rfidModel.Op160Time);

                        deviceSeq.ReadDataDic.AddOrUpdate("Op170Beat", rfidModel.Op170Beat);
                        deviceSeq.ReadDataDic.AddOrUpdate("Op170Result", rfidModel.Op170Result);
                        deviceSeq.ReadDataDic.AddOrUpdate("Op170Time", rfidModel.Op170Time);

                        deviceSeq.ReadDataDic.AddOrUpdate("Op180_1Beat", rfidModel.Op180_1Beat);
                        deviceSeq.ReadDataDic.AddOrUpdate("Op180_1Result", rfidModel.Op180_1Result);
                        deviceSeq.ReadDataDic.AddOrUpdate("Op180_1Time", rfidModel.Op180_1Time);

                        deviceSeq.ReadDataDic.AddOrUpdate("Op180_2Beat", rfidModel.Op180_2Beat);
                        deviceSeq.ReadDataDic.AddOrUpdate("Op180_2Result", rfidModel.Op180_2Result);
                        deviceSeq.ReadDataDic.AddOrUpdate("Op180_2Time", rfidModel.Op180_2Time);
                    }
                    else
                    {
                        (deviceSeq.SeqName + "【数据库未查询到】RFID - " + rfid.ToString()).LogError();
                    }
                    #endregion

                    #region 数据合格性判定
                    // 实际数据来判定是否为 NG 件
                    if (deviceSeq.ReadDataDic.TryGetValue("Op180_3TorqueResult", out var torqueResult))
                    {
                        if ((torqueResult.ToString() == "2"))
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

                    if (deviceSeq.ReadDataDic.TryGetValue("Op180_3TorqueResult2", out var torqueResult2))
                    {
                        if ((torqueResult2.ToString() == "2"))
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
                    #endregion
                }
                else
                {
                    (deviceSeq.SeqName + "数据字典未查询到 RFID 或 ShellCode 为空").LogError();
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
                    }
                }
                else
                {
                    (deviceSeq.SeqName + "【字典数据未查询到】RFID or ProductCode").LogError();
                }
            }

            // Op90 站位移判断
            if (deviceSeq.SeqName == "Op90")
            {
                if (deviceSeq.ReadDataDic.TryGetValue("Op90DisplaceResult", out var displace))
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

            // 120 直接解绑
            if (deviceSeq.SeqName == "Op120")
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
                    }
                }
                else
                {
                    (deviceSeq.SeqName + "【字典数据未查询到】 RFID").LogError();
                }
            }

            // 130 解绑 RFID表中的 RFIDIsUse（130 再次解绑)
            if (deviceSeq.SeqName == "Op130")
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
                    }
                }
                else
                {
                    (deviceSeq.SeqName + "【字典数据未查询到】 RFID").LogError();
                }
            }

            // 190 球阀组装
            if (deviceSeq.SeqName == "Op190")
            {
                if (deviceSeq.ReadDataDic.TryGetValue("RFID", out var rfid))
                {
                    var rfidModel = DbContext.Db.Queryable<Albert_RFID>().First(x => x.RFID.ToString() == rfid.ToString());

                    if (rfidModel != null)
                    {
                        // 球阀组装
                        rfidModel.SteelBall = "N";
                        await DbContext.Db.Updateable(rfidModel)
                            .Where(it => it.RFID == rfid.ToInt(0))
                            .ExecuteCommandAsync();
                    }
                    else
                    {
                        (deviceSeq.SeqName + "【数据库未查询到】RFID - " + rfid.ToString()).LogError();
                    }
                }
                else
                {
                    (deviceSeq.SeqName + "【数据字典未查询到】RFID 或产品码").LogError();
                }
            }

            // 200 堵帽组装
            if (deviceSeq.SeqName == "Op200")
            {
                if (deviceSeq.ReadDataDic.TryGetValue("RFID", out var rfid))
                {
                    var rfidModel = DbContext.Db.Queryable<Albert_RFID>().First(x => x.RFID.ToString() == rfid.ToString());

                    if (rfidModel != null)
                    {
                        rfidModel.PlugCap = "N";

                        // 更新 rfid 绑定关系
                        await DbContext.Db.Updateable(rfidModel)
                            .Where(it => it.RFID == rfid.ToInt(0))
                            .ExecuteCommandAsync();
                    }
                    else
                    {
                        (deviceSeq.SeqName + "【数据库未查询到】RFID - " + rfid.ToString()).LogError();
                    }
                }
                else
                {
                    (deviceSeq.SeqName + "【数据字典未查询到】RFID 或产品码").LogError();
                }
            }

            if (deviceSeq.SeqName == "Op210")
            {
                if (deviceSeq.ReadDataDic.TryGetValue("Op210TorqueResult", out var torqueResult))
                {
                    if (torqueResult.ToString() == "2")
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

            if (deviceSeq.SeqName == "Op220")
            {
                if (deviceSeq.ReadDataDic.TryGetValue("Op220DisplaceResult", out var displaceResult))
                {
                    if (displaceResult.ToString() == "2")
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

            // 需要从氦检数据库抓取数据
            if (deviceSeq.SeqName == "Op230_3")
            {

            }

            // 需要从氦检数据库抓取数据
            if (deviceSeq.SeqName == "Op230_4")
            {

            }

            if (deviceSeq.SeqName == "Op240_2")
            {
                RemoveOtherData(deviceSeq, "Op240");
            }

            if (deviceSeq.SeqName == "Op250_2")
            {
                RemoveOtherData(deviceSeq,"Op250");
            }

            // 涂油组装
            // 三码合一
            if (deviceSeq.SeqName == "Op280")
            {
                if (deviceSeq.ReadDataDic.TryGetValue("RFID", out var rfid))
                {
                    var rfidModel = DbContext.Db.Queryable<Albert_RFID>().First(x => x.RFID.ToString() == rfid.ToString());

                    if (rfidModel != null)
                    {
                        rfidModel.Spring = "N";
                        // 执行器码绑定给产品
                        deviceSeq.ReadDataDic.AddOrUpdate("RunCode", rfidModel?.RunCode);
                        // 更新 rfid 绑定关系
                        await DbContext.Db.Updateable(rfidModel)
                            .Where(it => it.RFID == rfid.ToInt(0))
                            .ExecuteCommandAsync();
                    }
                    else
                    {
                        (deviceSeq.SeqName + "【数据库未查询到】RFID - " + rfid.ToString()).LogError();
                    }
                }
                else
                {
                    (deviceSeq.SeqName + "【数据字典未查询到】RFID 或产品码").LogError();
                }


                if (deviceSeq.ReadDataDic.TryGetValue("Op280TorqueResult", out var torqueResult))
                {
                    if (torqueResult.ToString() == "2")
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

                if (deviceSeq.ReadDataDic.TryGetValue("Op280TorqueResult2", out var torqueResult2))
                {
                    if (torqueResult2.ToString() == "2")
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

                if (deviceSeq.ReadDataDic.TryGetValue("Op280TorqueResult3", out var torqueResult3))
                {
                    if (torqueResult3.ToString() == "2")
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

                if (deviceSeq.ReadDataDic.TryGetValue("ShellCode", out var shellCodeObj))
                {
                    if (shellCodeObj?.ToString()?.Length > 2)
                    {
                        var shellCode = shellCodeObj?.ToString()?.Substring(2);
                        deviceSeq.ReadDataDic.AddOrUpdate("ShellCode", shellCode);
                    }
                }
            }

            if (deviceSeq.SeqName == "Op290_2")
            {
                RemoveOtherData(deviceSeq, "Op290");
            }

            if (deviceSeq.SeqName == "Op300_2")
            {
                RemoveOtherData(deviceSeq, "Op300");
            }

            // 螺柱组装
            if (deviceSeq.SeqName == "Op320")
            {
                if (deviceSeq.ReadDataDic.TryGetValue("RFID", out var rfid))
                {
                    var rfidModel = DbContext.Db.Queryable<Albert_RFID>().First(x => x.RFID.ToString() == rfid.ToString());

                    if (rfidModel != null)
                    {
                        rfidModel.Nut = "N";

                        // 更新 rfid 绑定关系
                        await DbContext.Db.Updateable(rfidModel)
                            .Where(it => it.RFID == rfid.ToInt(0))
                            .ExecuteCommandAsync();
                    }
                    else
                    {
                        (deviceSeq.SeqName + "【数据库未查询到】RFID - " + rfid.ToString()).LogError();
                    }
                }
                else
                {
                    (deviceSeq.SeqName + "【数据字典未查询到】RFID 或产品码").LogError();
                }

                if (deviceSeq.ReadDataDic.TryGetValue("Op320TorqueResult", out var torqueResult))
                {
                    if (torqueResult.ToString() == "2")
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

            // 人工下料，rfid 壳体码解绑
            if (deviceSeq.SeqName == "Op350")
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
                    }
                }
                else
                {
                    (deviceSeq.SeqName + "【字典数据未查询到】 RFID").LogError();
                }
            }
        }

        private async Task RemoveOtherData(DeviceSeq deviceSeq,string station)
        {
            if (App.GetConfig<bool>("OpenOther"))
            {
                if (deviceSeq.ReadDataDic.TryGetValue("ShellCode", out var shellCode))
                {
                    switch (station)
                    {
                        case "Op240":

                            // 这种用来切换租户 ID
                            var op240Data = await DbContext.Db
                                .GetConnectionScope("Op240")
                                .Queryable<tbl_record_data_240>()
                                .AS("tbl_record_data")
                                .Where(x => x.Barcode == shellCode.ToString())
                                .FirstAsync();

                            if (op240Data != null)
                            {
                                var line = DbContext.Db.Insertable(op240Data).ExecuteCommand();

                                if (line > 0)
                                {
                                    $"{deviceSeq.SeqName}数据搬运成功".LogInformation();
                                }
                                else
                                {
                                    $"{deviceSeq.SeqName}数据搬运失败".LogError();
                                }
                            }
                            else
                            {
                                $"{deviceSeq.SeqName}为查询到相关数据".LogError();
                            }

                            break;
                        case "Op250":
                            // 这种用来切换租户 ID
                            var op250Data = await DbContext.Db
                                .GetConnectionScope("Op250")
                                .Queryable<tbl_record_data_250>()
                                .AS("tbl_record_data")
                                .Where(x => x.Barcode == shellCode.ToString())
                                .FirstAsync();

                            if (op250Data != null)
                            {
                                var line = DbContext.Db.Insertable(op250Data).ExecuteCommand();

                                if (line > 0)
                                {
                                    $"{deviceSeq.SeqName}数据搬运成功".LogInformation();
                                }
                                else
                                {
                                    $"{deviceSeq.SeqName}数据搬运失败".LogError();
                                }
                            }
                            else
                            {
                                $"{deviceSeq.SeqName}为查询到相关数据".LogError();
                            }
                            break;
                        case "Op290":
                            // 这种用来切换租户 ID
                            var op290Data = await DbContext.Db
                                .GetConnectionScope("Op290")
                                .Queryable<tbl_record_data_290>()
                                .AS("tbl_record_data")
                                .Where(x => x.Barcode == shellCode.ToString())
                                .FirstAsync();

                            if (op290Data != null)
                            {
                                var line = DbContext.Db.Insertable(op290Data).ExecuteCommand();

                                if (line > 0)
                                {
                                    $"{deviceSeq.SeqName}数据搬运成功".LogInformation();
                                }
                                else
                                {
                                    $"{deviceSeq.SeqName}数据搬运失败".LogError();
                                }
                            }
                            else
                            {
                                $"{deviceSeq.SeqName}为查询到相关数据".LogError();
                            }
                            break;
                        case "Op300":
                            // 这种用来切换租户 ID
                            var op300Data = await DbContext.Db
                                .GetConnectionScope("Op300")
                                .Queryable<tbl_record_data_300>()
                                .AS("tbl_record_data")
                                .Where(x => x.Barcode == shellCode.ToString())
                                .FirstAsync();

                            if (op300Data != null)
                            {
                                var line = DbContext.Db.Insertable(op300Data).ExecuteCommand();

                                if (line > 0)
                                {
                                    $"{deviceSeq.SeqName}数据搬运成功".LogInformation();
                                }
                                else
                                {
                                    $"{deviceSeq.SeqName} 数据搬运失败".LogError();
                                }
                            }
                            else
                            {
                                $"{deviceSeq.SeqName}为查询到相关数据".LogError();
                            }
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    $"{deviceSeq.SeqName}电气未给产品码".LogInformation();
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
