using HslCommunication.Profinet.Siemens;
using SqlSugar.DbConvert;

namespace AlbertCollection.Core.Entity.Device;
/// <summary>
/// 插件信息表
/// </summary>
[SugarTable("device-plugin", TableDescription = "插件信息表")]
[Tenant(SqlsugarConst.DB_CustomId)]
public class DevicePlugin : BaseEntity
{
    /// <summary>
    /// 插件名称
    /// </summary>
    [SugarColumn(ColumnName = "PluginName", ColumnDescription = "插件名称")]
    public string PluginName { get; set; }
    /// <summary>
    /// 插件功能类型名称
    /// </summary>
    [SugarColumn(ColumnName = "PluginFunction", ColumnDescription = "插件功能类型名称")]
    public string PluginFunction { get; set; }
    /// <summary>
    /// 插件类型
    /// </summary>
    [SugarColumn(ColumnDataType = "varchar(50)", ColumnName = "SimPlcType", ColumnDescription = "西门子类型", SqlParameterDbType = typeof(EnumToStringConvert))]
    public SiemensPLCS SimPlcType { get; set; }

    /// <summary>
    /// 插件其他信息
    /// </summary>
    [SugarColumn(ColumnName = "PluginExtraInfo", ColumnDescription = "插件其他信息")]
    public string PluginExtraInfo { get; set; }

}
