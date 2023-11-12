using HslCommunication.Profinet.Siemens;

namespace AlbertCollection.Application.Services.GatewayConfiguration.Dto
{
    public class DeviceCollectionInput
    {

        /// <summary>
        /// 设备名称（唯一 ID)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 设备地址
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// 设备端口
        /// </summary>
        public string Port { get; set; }
        /// <summary>
        /// PLC 类型
        /// </summary>
        public SiemensPLCS SimPlcType { get; set; }
    }
}
