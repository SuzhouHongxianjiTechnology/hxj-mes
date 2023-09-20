#region copyright
//------------------------------------------------------------------------------
//  此代码版权声明为全文件覆盖，如有原作者特别声明，会在下方手动补充
//  此代码版权（除特别声明外的代码）归作者本人AlbertZhao所有
//  源代码使用协议遵循本仓库的开源协议及附加协议
//  Gitee源代码仓库：https://gitee.com/AlbertZhao/AlbertCollection



//------------------------------------------------------------------------------
#endregion

namespace AlbertCollection.Core
{
    /// <summary>
    /// 登录设备类型枚举
    /// </summary>
    public enum AuthDeviceTypeEnum
    {
        /// <summary>
        /// PC端
        /// </summary>
        [Description("PC端")]
        PC,

        /// <summary>
        /// 移动端
        /// </summary>
        [Description("移动端")]
        APP,

        /// <summary>
        /// Api
        /// </summary>
        [Description("Api")]
        Api,
    }
}