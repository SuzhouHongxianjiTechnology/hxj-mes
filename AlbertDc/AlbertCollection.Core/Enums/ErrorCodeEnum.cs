#region copyright
//------------------------------------------------------------------------------
//  此代码版权声明为全文件覆盖，如有原作者特别声明，会在下方手动补充
//  此代码版权（除特别声明外的代码）归作者本人AlbertZhao所有
//  源代码使用协议遵循本仓库的开源协议及附加协议
//  Gitee源代码仓库：https://gitee.com/AlbertZhao/AlbertCollection



//------------------------------------------------------------------------------
#endregion

using Furion.FriendlyException;

namespace AlbertCollection.Core
{
    /// <summary>
    /// 通用错误码
    /// </summary>
    [ErrorCodeType]
    public enum ErrorCodeEnum
    {
        /// <summary>
        /// 系统异常
        /// </summary>
        [ErrorCodeItemMetadata("系统异常")]
        A0000,

        /// <summary>
        /// 数据不存在
        /// </summary>
        [ErrorCodeItemMetadata("数据不存在")]
        A0001,

        /// <summary>
        /// 删除失败
        /// </summary>
        [ErrorCodeItemMetadata("删除失败")]
        A0002,

        /// <summary>
        /// 操作失败
        /// </summary>
        [ErrorCodeItemMetadata("操作失败")]
        A0003,

        /// <summary>
        /// 没有权限
        /// </summary>
        [ErrorCodeItemMetadata("没有权限")]
        A0004,
    }
}