#region copyright
//------------------------------------------------------------------------------
//  此代码版权声明为全文件覆盖，如有原作者特别声明，会在下方手动补充
//  此代码版权（除特别声明外的代码）归作者本人AlbertZhao所有
//  源代码使用协议遵循本仓库的开源协议及附加协议
//  Gitee源代码仓库：https://gitee.com/AlbertZhao/AlbertCollection
//------------------------------------------------------------------------------
#endregion

using AlbertCollection.Core.Entity.Device;

namespace AlbertCollection.Application.SeedData
{
    /// <summary>
    /// 设备插件种子数据
    /// </summary>
    public class DevicePluginSeedData : ISqlSugarEntitySeedData<DevicePlugin>
    {
        /// <inheritdoc/>
        public IEnumerable<DevicePlugin> SeedData()
        {
            return SeedDataUtil.GetSeedData<DevicePlugin>("deivce_plugin.json");
        }
    }
}