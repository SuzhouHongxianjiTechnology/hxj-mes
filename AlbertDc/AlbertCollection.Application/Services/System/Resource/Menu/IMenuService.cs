#region copyright
//------------------------------------------------------------------------------
//  此代码版权声明为全文件覆盖，如有原作者特别声明，会在下方手动补充
//  此代码版权（除特别声明外的代码）归作者本人AlbertZhao所有
//  源代码使用协议遵循本仓库的开源协议及附加协议
//  Gitee源代码仓库：https://gitee.com/AlbertZhao/AlbertCollection



//------------------------------------------------------------------------------
#endregion

namespace AlbertCollection.Application
{
    /// <summary>
    /// 菜单服务
    /// </summary>
    public interface IMenuService : ITransient
    {
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="input">添加参数</param>
        /// <returns></returns>
        Task Add(MenuAddInput input);



        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="input">删除菜单参数</param>
        /// <returns></returns>
        Task Delete(List<BaseIdInput> input);

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="input">id</param>
        /// <returns>详细信息</returns>
        Task<SysResourceAc> Detail(BaseIdInput input);

        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="input">菜单编辑参数</param>
        /// <returns></returns>
        Task Edit(MenuEditInput input);

        /// <summary>
        /// 根据模块获取菜单树，为空则为全部模块
        /// </summary>
        /// <param name="input">菜单树查询参数</param>
        /// <returns>菜单树列表</returns>
        Task<List<SysResourceAc>> Tree(MenuPageInput input);
    }
}