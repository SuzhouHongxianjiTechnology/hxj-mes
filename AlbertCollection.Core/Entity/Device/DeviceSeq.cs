using AlbertCollection.Core.Enums;
using SqlSugar.DbConvert;
using System.Threading;

namespace AlbertCollection.Core.Entity.Device
{
    /// <summary>
    /// 批量信息表
    ///</summary>
    [SugarTable("device-seq", TableDescription = "批量信息表")]
    [Tenant(SqlsugarConst.DB_Default)]
    public class DeviceSeq : BaseEntity
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [SugarColumn(ColumnName = "DeivceId", ColumnDescription = "设备ID")]
        public long DeivceId { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        [SugarColumn(ColumnName = "DeviceName", ColumnDescription = "设备名称", Length = 20, IsNullable = true)]
        public string DeviceName { get; set; }
        /// <summary>
        /// 批量名
        /// </summary>
        [SugarColumn(ColumnName = "SeqName", ColumnDescription = "批量名", Length = 20, IsNullable = true)]
        public string SeqName { get; set; }
        /// <summary>
        ///  Rfid上升沿
        /// </summary>
        [SugarColumn(ColumnName = "RfidRisingEdge", ColumnDescription = "Rfid 上升沿", Length = 50, IsNullable = true)]
        public string RfidRisingEdge { get; set; }
        /// <summary>
        ///  Rfid上升沿相应地址
        /// </summary>
        [SugarColumn(ColumnName = "RfidResponseEdge", ColumnDescription = "Rfid 上升沿响应地址", Length = 50, IsNullable = true)]
        public string RfidResponseEdge { get; set; }
        /// <summary>
        ///  Rfid Label
        /// </summary>
        [SugarColumn(ColumnName = "RfidLabel", ColumnDescription = "Rfid 标签", Length = 50, IsNullable = true)]
        public string RfidLabel { get; set; }
        /// <summary>
        ///  Rfid Label
        /// </summary>
        [SugarColumn(ColumnName = "StationAllow", ColumnDescription = "工作是否允许做", Length = 50, IsNullable = true)]
        public string StationAllow { get; set; }
        /// <summary>
        ///  上升沿
        /// </summary>
        [SugarColumn(ColumnName = "RisingEdge", ColumnDescription = "上升沿", Length = 50, IsNullable = true)]
        public string RisingEdge { get; set; }
        /// <summary>
        ///  上升沿相应地址
        /// </summary>
        [SugarColumn(ColumnName = "ResponseEdge", ColumnDescription = "上升沿响应地址", Length = 50, IsNullable = true)]
        public string ResponseEdge { get; set; }
        
        /// <summary>
        /// 批量类型
        /// </summary>
        [SugarColumn(ColumnDataType = "varchar(50)", ColumnName = "SqlOperate", ColumnDescription = "批量类型", SqlParameterDbType = typeof(EnumToStringConvert))]
        public SqlOperateEnum SqlOperate { get; set; }
        /// <summary>
        /// 是否开启采集
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public bool IsOpen { get; set; } = false;
        /// <summary>
        /// 循环线程取消标志
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public CancellationTokenSource StopStationTokenSource { get; set; }
        /// <summary>
        /// 读取数据。格式：Op10Result;string,DB104.12,2
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<DeviceReadData> ReadData { get; set; }
        /// <summary>
        /// 读取数据存储
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public Dictionary<string, object> ReadDataDic { get; set; } = new();
        /// <summary>
        /// 更新数据
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<DeviceUpdateData> UpdateData { get; set; }
        /// <summary>
        /// 更新数据存储
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public Dictionary<string, object> UpdateDataDic { get; set; } = new();
        /// <summary>
        /// 写入PLC数据。格式：LastResult;string,DB106.10
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<DeviceWriteData> WriteData { get; set; }
        /// <summary>
        /// 写入PLC数据存储
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public Dictionary<string, object> WriteDataDic { get; set; } = new();
    }
}
