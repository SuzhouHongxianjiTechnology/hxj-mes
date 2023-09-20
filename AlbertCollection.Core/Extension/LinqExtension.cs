#region copyright
//------------------------------------------------------------------------------
//  此代码版权声明为全文件覆盖，如有原作者特别声明，会在下方手动补充
//  此代码版权（除特别声明外的代码）归作者本人AlbertZhao所有
//  源代码使用协议遵循本仓库的开源协议及附加协议
//  Gitee源代码仓库：https://gitee.com/AlbertZhao/AlbertCollection



//------------------------------------------------------------------------------
#endregion

using System.Linq;

namespace AlbertCollection.Core
{
    /// <summary>
    /// Linq扩展
    /// </summary>
    [SuppressSniffer]
    public static class LinqExtension
    {
        /// <summary>
        /// 是否都包含
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first">第一个列表</param>
        /// <param name="secend">第二个列表</param>
        /// <returns></returns>
        public static bool ContainsAll<T>(this List<T> first, List<T> secend)
        {
            return secend.All(s => first.Any(f => f.Equals(s)));
        }

        public static List<string> StringToList(this string str)
        {
            return new List<string>() { str };
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">数据列表</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页数量</param>
        /// <returns>分页集合</returns>
        public static SqlSugarPagedList<T> LinqPagedList<T>(this List<T> list, int pageIndex, int pageSize)
        {
            var result = list.ToPagedList(pageIndex, pageSize);//获取分页
                                                               //格式化
            return new SqlSugarPagedList<T>
            {
                Current = pageIndex,
                Size = result.PageSize,
                Records = result.Data,
                Total = result.TotalCount,
                Pages = result.TotalPages,
                HasNextPages = result.HasNext,
                HasPrevPages = result.HasPrev
            };
        }
    }
}