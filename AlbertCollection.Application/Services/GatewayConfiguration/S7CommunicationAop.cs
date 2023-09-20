using AlbertCollection.Application.Services.GatewayConfiguration.Dto;
using AlbertCollection.Core.Entity.Device;
using Furion.Logging.Extensions;
using TouchSocket.Core;

namespace AlbertCollection.Application.Services.GatewayConfiguration
{
    /// <summary>
    /// 暴露给外界用，所有拦截操作都写在这里
    /// </summary>
    public class S7CommunicationAop : S7Communication
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="device"></param>
        public S7CommunicationAop(DeviceCollection device) : base(device)
        {
            _device = device;
        }

        public override async Task SqlOperateNoneAop(DeviceSeq deviceSeq)
        {
            // 用于第一站数据，节拍和加工时间绑定到 rfid 表上，方便 Op20 站搬运数据
            // 更新 Rfid
            if (deviceSeq.SeqName == "Op10")
            {
                deviceSeq.ReadDataDic.AddOrUpdate("RFIDIsUse", 1);
                deviceSeq.ReadDataDic.AddOrUpdate("Op10Result", "OK");
                deviceSeq.ReadDataDic.AddOrUpdate("Op10Time", DateTime.Now);
                deviceSeq.ReadDataDic.AddOrUpdate("Op10Result", "OK");
                // ToDo;有问题不更新数据
                // 数据更新,以 ProductCode
                var line = await DbContext.Db.Updateable(deviceSeq.ReadDataDic)
                    .AS("Albert_RFID").WhereColumns("RFID").ExecuteCommandAsync();

                if (line <= 0)
                {
                    (deviceSeq.SeqName + "未更新任何数据").LogError();
                }
            }

            if (deviceSeq.SeqName == "140")
            {
                deviceSeq.ReadDataDic.AddOrUpdate("RFIDIsUse", 0);

                var line = await DbContext.Db.Updateable(deviceSeq.ReadDataDic)
                    .AS("Albert_RFID").WhereColumns("RFID").ExecuteCommandAsync();

                if (line <= 0)
                {
                    (deviceSeq.SeqName + "未更新任何数据").LogError();
                }
            }
        }

        public override async Task SqlOperateInsertAop(DeviceSeq deviceSeq)
        {
            if (deviceSeq.SeqName == "Op20")
            {
                // 从 _rfidModel 中获取 Op10Beat，Op10Result,Op10Time
                deviceSeq.ReadDataDic.TryGetValue("RFID", out var rfid);
                var rfidModel = DbContext.Db.Queryable<Albert_RFID>().First(x => x.RFID.ToString() == rfid);

                if (rfidModel != null)
                {
                    deviceSeq.ReadDataDic.AddOrUpdate("Op10Beat", rfidModel.OP10Beat);
                    deviceSeq.ReadDataDic.AddOrUpdate("Op10Result", rfidModel.Op10Result);
                    deviceSeq.ReadDataDic.AddOrUpdate("Op10Time", rfidModel.Op10Time);
                }
                else
                {
                    (deviceSeq.SeqName + "未在数据库中查询到-" + rfid.ToString()).LogError();
                }
            }
        }

        public override async Task SqlOperateUpdateAop(DeviceSeq deviceSeq)
        {
            // 下面可能被覆写，当前站结果+最终站结果
            deviceSeq.ReadDataDic.AddOrUpdate("OpFinalResult", "OK");
            deviceSeq.ReadDataDic.AddOrUpdate(deviceSeq.SeqName + "Result", "OK");

            // 异步搬运文件，不影响产线节拍
            if (deviceSeq.SeqName == "Op60")
            {
                if (deviceSeq.ReadDataDic.TryGetValue("ProductCode", out var productCode))
                {
                    Task.Run(() =>
                    {
                        var srcFolder =  App.GetConfig<string>("Op60PressFolder");
                        RemoveFile(srcFolder, productCode.ToString());
                    });
                }
                else
                {
                    (deviceSeq.SeqName+"未查询到产品码，无法重命名压机文件").LogError();
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
                    return srcFolder + "\\latestFileName";
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

        public override async Task SqlOperateFinalAop(DeviceSeq deviceSeq)
        {
            // 1.Op20 要更新 Rfid 表 label 和 product 的绑定关系
            if (deviceSeq.SeqName.Contains("Op20"))
            {
                var rfidResult = deviceSeq.ReadDataDic.TryGetValue("RFID", out var rfid);
                var productCodeResult = deviceSeq.ReadDataDic.TryGetValue("ProductCode", out var productCode);
                if (rfidResult && productCodeResult)
                {
                    var rfidModel = DbContext.Db.Queryable<Albert_RFID>().First(x => x.RFID.ToString() == rfid.ToString());

                    if (rfidModel != null)
                    {
                        // 绑定 Rfid 和 产品码
                        rfidModel.ProductCode = productCode.ToString();
                        // 更新绑定关系
                        await DbContext.Db.Updateable(rfidModel)
                            .Where(it => it.RFID == rfid.ToInt(0))
                            .ExecuteCommandAsync();
                    }
                    else
                    {
                        (deviceSeq.SeqName + rfid + "Rfid 未在数据库中查询到").LogError();
                    }
                }
                else
                {
                    "Op20 站未从字典中获取到 Rfid 或 产品码，请检查".LogError();
                }
            }
        }

        //public override Task WriteDataDicAop(DeviceSeq deviceSeq)
        //{
        //    //这种用来切换租户 ID
        //    var test = DbContext.Db.GetConnectionScope("019").Queryable<object>().AS("app_transaction").ToList();

        //    if (deviceSeq.SeqName.Contains("RFID"))
        //    {
        //        // 走数据库查询出结果 OK NG（根据标签码查询产品码，根据产品码来判断是否可以做）
        //        deviceSeq.ReadDataDic.TryGetValue("Label", out var Label);


        //        if (deviceSeq.WriteDataDic.ContainsKey("AllowWork"))
        //        {
        //            // OP10 OP20 站都没有码
        //            if (deviceSeq.SeqName.Contains("OP10")||deviceSeq.SeqName.Contains("OP20"))
        //            {
        //                // 1. 第一站直接写 1,允许运行
        //                deviceSeq.WriteDataDic["AllowWork"] = "1";
        //            }
        //            else
        //            {
        //                // 其他分支需要查询数据库获取结果
        //                // 1. 根据标签码，查询出数据库中的产品码，产品码是否可做
        //                //var productCodeResult = test
        //                // 1 表示可以工作  deviceSeq.WriteDataDic["AllowWork"] = "1";
        //            }
        //        }
        //    }

        //    return Task.CompletedTask;
        //}
    }
}
