using System.Runtime.CompilerServices;
using HslCommunication.Profinet.Siemens;
using SqlSugar.DbConvert;
using System.Threading;

namespace AlbertCollection.Core.Entity.Device
{
    /// <summary>
    /// 设备信息表
    ///</summary>
    [SugarTable("device_collection", TableDescription = "设备信息表")]
    [Tenant(SqlsugarConst.DB_Default)]
    public class DeviceCollection:BaseEntity
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        [SugarColumn(ColumnName = "Name", ColumnDescription = "设备名称", Length = 20, IsNullable = true)]
        public string Name { get; set; }
        /// <summary>
        /// 设备 IP
        /// </summary>
        [SugarColumn(ColumnName = "IpAddress", ColumnDescription = "设备 IP", Length = 20, IsNullable = true)]
        public string IpAddress { get; set; }
        /// <summary>
        /// 设备端口
        /// </summary>
        [SugarColumn(ColumnName = "Port", ColumnDescription = "设备端口", Length = 20, IsNullable = true)]
        public string Port { get; set; }
        /// <summary>
        /// 设备连接超时时间
        /// </summary>
        [SugarColumn(ColumnName = "ConnectTimeOut", ColumnDescription = "设备连接超时时间", Length = 20, IsNullable = true)]
        public string ConnectTimeOut { get; set; }
        /// <summary>
        /// 设备通讯超时时间
        /// </summary>
        [SugarColumn(ColumnName = "ReceiveTimeOut", ColumnDescription = "设备通讯超时时间", Length = 20, IsNullable = true)]
        public string ReceiveTimeOut { get; set; }
        /// <summary>
        /// 设置心跳地址
        /// </summary>
        [SugarColumn(ColumnName = "SetHeartAddress", ColumnDescription = "设置心跳地址", Length = 20, IsNullable = true)]
        public string SetHeartAddress { get; set; } = "M100";
        /// <summary>
        /// PLC 类型
        /// </summary>
        [SugarColumn(ColumnDataType = "varchar(50)", ColumnName = "SimPlcType", ColumnDescription = "西门子类型", SqlParameterDbType = typeof(EnumToStringConvert))]
        public SiemensPLCS SimPlcType { get; set; }
        /// <summary>
        /// PLC 通讯句柄
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public SiemensS7Net SimTcpNet { get; set; }
        /// <summary>
        /// 设备变化委托
        /// </summary>
        /// <param name="deviceCollection"></param>
        public delegate void DelegateOnDeviceChanged(DeviceCollection deviceCollection);
        /// <summary>
        /// 设备状态变化事件
        /// </summary>
        public event DelegateOnDeviceChanged DeviceStatusChange;
        private DeviceStatusEnum _deviceStatus = DeviceStatusEnum.DisConnect;
        [SugarColumn(IsIgnore = true)]
        public DeviceStatusEnum DeviceStatus
        {
            get
            {
                if (IsOpen)
                {
                    return DeviceStatusEnum.OnLine;
                }
                else
                {
                    return DeviceStatusEnum.OffLine;
                }
            }
            private set
            {
                if (_deviceStatus != value)
                {
                    _deviceStatus = value;
                    DeviceStatusChange?.Invoke(this);
                }
            }
        }
        [Description("活跃时间")]
        public DateTime ActiveTime { get; set; } = DateTime.MinValue;
        /// <summary>
        /// 设备是否启用
        /// </summary>
        [Description("开启")]
        [SugarColumn(IsIgnore = true)]
        public bool IsOpen { get; set; } = false;
        /// <summary>
        /// 设备心跳是否正常
        /// </summary>
        [Description("运行")]
        [SugarColumn(IsIgnore = true)]
        public bool IsHeartbeatOpen { get; set; } = false;
        /// <summary>
        /// 循环线程取消标志
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public CancellationTokenSource StopHeartbeatOpenTokenSource { get; set; } = new();
        /// <summary>
        /// 地址块
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<DeviceSeq> StationSeqs { get; set; }
    }
}
