using AlbertCollection.Application.BgWorkers;
using AlbertCollection.Application.Services.GatewayConfiguration;
using AlbertCollection.Core.Const;
using System.Collections.Concurrent;
using AlbertCollection.Application.Cache;
using AlbertCollection.Core.Entity.Device;

namespace AlbertCollection.Application.GlobalData
{
    /// <summary>
    /// 全局设备
    /// </summary>
    public class GlobalDeviceData : ISingleton
    {
        private ConcurrentDictionary<string, S7CommunicationAop> _globalDeviceDic = new();

        private readonly ICacheRedisService _cacheService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cacheService"></param>
        public GlobalDeviceData(ICacheRedisService cacheService)
        {
            this._cacheService = cacheService;
        }

        /// <summary>
        /// 全局设备对象
        /// </summary>
        public ConcurrentDictionary<string, S7CommunicationAop> GlobalDeviceDic
        {
            get => _globalDeviceDic;
            set => _globalDeviceDic = value;
        }

        /// <summary>
        /// 设备集合
        /// </summary>
        public List<DeviceCollection> DeviceCollectionList = App.GetConfig<List<DeviceCollection>>("DeviceCollections:DeviceCollection");

        /// <summary>
        /// 查找设备，之后读写都可以走 xxx.ReadData().WriteData()
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns></returns>
        public BaseCommunication FindS7ByName(string deviceName)
        {
            _globalDeviceDic.TryGetValue(deviceName,out var s7Instance);
            if (s7Instance == null)
            {
                BackMessage.AddMessage(DeviceConst.DEV_FIND_NG, LogLevel.Information);
            }

            return s7Instance;
        }

        /// <summary>
        /// 添加设备到集合
        /// </summary>
        /// <param name="device"></param>
        public void TryAddOrUpdateDevice(DeviceCollection device)
        {
            var dc = DeviceCollectionList.FirstOrDefault(x => x.Name == device.Name);
            if(dc == null)
            {
                DeviceCollectionList.Add(device);
            }
            else
            {
                dc = device;
            }
            
        }

        /// <summary>
        /// 添加设备并初始化
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public async Task TryAddAndInitDevice(DeviceCollection device, bool singleCollect)
        {
            _globalDeviceDic.TryGetValue(device.Name, out var s7Instance);
            if (s7Instance == null)
            {
                TryAddOrUpdateDevice(device);
                var s7InstanceNew = new S7CommunicationAop(device,_cacheService);
                s7InstanceNew.Init(singleCollect);
                _globalDeviceDic.TryAdd(device.Name,s7InstanceNew);
            }
            else
            {
                // 1. close 调心跳
                await s7Instance.StopDeviceAsync();
                // 2. 更新 device 的 ip 和 port， 重新初始化
                s7Instance.UpdateDevice(device).Init(singleCollect);
            }
        }

        /// <summary>
        /// 开启单工站采集
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="stationName"></param>
        /// <returns></returns>
        public bool StartStationSeq(string deviceName,string stationName)
        {
            var s7Instance = FindS7ByName(deviceName);
            if (s7Instance == null)
            {
                BackMessage.AddMessage(DeviceConst.DEV_FIND_NG, LogLevel.Information);
                return false;
            }

            if (s7Instance.GetDevice().IsOpen)
            {
                s7Instance.StartStation(stationName);
                return true;      
            }
            else
            {
                BackMessage.AddMessage(DeviceConst.DEV_INIT_NG, LogLevel.Information);
                return false;
            }
        }

        /// <summary>
        /// 停止工站采集
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="stationName"></param>
        /// <returns></returns>
        public bool StopStationSeq(string deviceName,string stationName) 
        {
            var s7Instance = FindS7ByName(deviceName);
            if (s7Instance == null)
            {
                BackMessage.AddMessage(DeviceConst.DEV_FIND_NG, LogLevel.Information);
                return false;
            }

            return s7Instance.StopStation(stationName);
        }
    }
}
