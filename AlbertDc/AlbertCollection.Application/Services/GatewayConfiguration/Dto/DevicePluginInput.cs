using HslCommunication.Profinet.Siemens;

namespace AlbertCollection.Application.Services.Driver.Dto;

/// <summary>
/// 插件分页
/// </summary>
public class DriverPluginPageInput : BasePageInput
{
    /// <summary>
    /// 插件功能名称
    /// </summary>
    [Description("插件功能名称")]
    public string Name { get; set; }
}


/// <summary>
/// 插件分组
/// </summary>
public class DriverPluginCategory
{
    /// <summary>
    /// 插件子组
    /// </summary>
    public List<DriverPluginCategory> Children { get; set; }

    /// <summary>
    /// 插件ID
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 插件名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 西门子类型
    /// </summary>
    public SiemensPLCS SimPlcType { get; set; }
}
