using AlbertCollection.Application.Services.Driver.Dto;
using AlbertCollection.Core.Entity.Device;
using HslCommunication.Profinet.Siemens;

namespace AlbertCollection.Application.Services.GatewayConfiguration;

/// <summary>
/// 设备插件
/// </summary>
public interface IDriverPluginService : ITransient
{
    /// <summary>
    /// 获取缓存
    /// </summary>
    List<DevicePlugin> GetCacheList();
    /// <summary>
    /// 根据ID获取插件信息
    /// </summary>
    DevicePlugin GetDriverPluginById(long Id);
    /// <summary>
    /// 根据分类获取插件树
    /// </summary>
    List<DriverPluginCategory> GetDriverPluginChildrenList(SiemensPLCS? driverTypeEnum = null);

    /// <summary>
    /// 根据ID获取名称
    /// </summary>
    long? GetIdByName(string name);
    /// <summary>
    /// 根据名称获取ID
    /// </summary>
    string GetNameById(long id);
    /// <summary>
    /// 分页
    /// </summary>
    Task<SqlSugarPagedList<DevicePlugin>> PageAsync(DriverPluginPageInput input);
}
