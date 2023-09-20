#region copyright
//------------------------------------------------------------------------------
//  此代码版权声明为全文件覆盖，如有原作者特别声明，会在下方手动补充
//  此代码版权（除特别声明外的代码）归作者本人AlbertZhao所有
//  源代码使用协议遵循本仓库的开源协议及附加协议
//  Gitee源代码仓库：https://gitee.com/AlbertZhao/AlbertCollection



//------------------------------------------------------------------------------
#endregion

using Microsoft.AspNetCore.Components.Forms;

namespace AlbertCollection.Application;

/// <summary>
/// 文件管理服务
/// </summary>
public interface IFileService : ITransient
{

    /// <summary>
    /// 验证上传文件
    /// </summary>
    /// <param name="file">文件</param>
    /// <param name="maxSzie">最大体积(M)</param>
    /// <param name="allowTypes">允许上传类型</param>
    void ImportVerification(IBrowserFile file, int maxSzie = 30, string[] allowTypes = null);



}
