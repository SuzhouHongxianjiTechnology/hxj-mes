using AlbertCollection.Core.Entity.Device;

namespace AlbertCollection.Application.Services.GatewayConfiguration
{
    /// <summary>
    /// 西门子服务
    /// </summary>
    public abstract class BaseCommunication
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public abstract BaseCommunication Init(bool singleCollect);
        /// <summary>
        /// 获取设备
        /// </summary>
        /// <returns></returns>
        public abstract DeviceCollection GetDevice();
        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <returns></returns>
        public abstract Task<bool> StopDeviceAsync();
        /// <summary>
        /// 更新设备
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public abstract BaseCommunication UpdateDevice(DeviceCollection device);
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="strAddress"></param>
        /// <returns></returns>
        public abstract BaseCommunication ReadData(string strAddress, out string result);
        /// <summary>
        /// 通用读数据
        /// </summary>
        /// <param name="strType"></param>
        /// <param name="address"></param>
        /// <param name="strReadUshort">字符串读取长度</param>
        /// <returns></returns>
        public abstract BaseCommunication ReadDataCommon(string strType, string address, string strReadUshort, out string result);
        /// <summary>
        /// 批量读取数据
        /// </summary>
        /// <param name="address"></param>
        /// <param name="readLength"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public abstract BaseCommunication ReadByteArray(string address, string readLength, out byte[] result);
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="strAddress"></param>
        /// <param name="setValue"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public abstract BaseCommunication WriteData(string strAddress, string setValue, out bool result);
        /// <summary>
        /// 通用写数据
        /// </summary>
        /// <param name="strType"></param>
        /// <param name="address"></param>
        /// <param name="setValue"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public abstract BaseCommunication WriteDataCommon(string strType, string address, string setValue, out bool result);
        /// <summary>
        /// 开启单工站采集
        /// </summary>
        /// <param name="stationName">工站名</param>
        public abstract void StartStation(string stationName);
        /// <summary>
        /// 停止工站采集
        /// </summary>
        /// <param name="stationName"></param>
        /// <returns></returns>
        public abstract bool StopStation(string stationName);
    }
}
