#region copyright
//------------------------------------------------------------------------------
//  此代码版权声明为全文件覆盖，如有原作者特别声明，会在下方手动补充
//  此代码版权（除特别声明外的代码）归作者本人AlbertZhao所有
//  源代码使用协议遵循本仓库的开源协议及附加协议
//  Gitee源代码仓库：https://gitee.com/AlbertZhao/AlbertCollection



//------------------------------------------------------------------------------
#endregion

namespace AlbertCollection.Core;

public class ImportPreviewOutputBase
{
    /// <summary>
    /// 是否有错误
    /// </summary>
    public bool HasError { get; set; }
    public virtual int DataCount { get; }
    public string ErrorStr { get; set; }
}
public class ImportPreviewOutput<T> : ImportPreviewOutputBase where T : class
{
    public override int DataCount { get => Data == null ? 0 : Data.Count; }
    public IList<T> Data { get; set; }
}


