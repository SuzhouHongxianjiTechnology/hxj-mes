#region copyright
//------------------------------------------------------------------------------
//  此代码版权声明为全文件覆盖，如有原作者特别声明，会在下方手动补充
//  此代码版权（除特别声明外的代码）归作者本人AlbertZhao所有
//  源代码使用协议遵循本仓库的开源协议及附加协议
//  Gitee源代码仓库：https://gitee.com/AlbertZhao/AlbertCollection



//------------------------------------------------------------------------------
#endregion

using BlazorComponent;

using SqlSugar;

using System.Linq;

namespace AlbertCollection.Web.Rcl
{
    public partial class Role
    {
        private IAppDataTable _datatable;
        private RolePageInput search = new();

        private async Task AddCall(RoleAddInput input)
        {
            await SysRoleService.Add(input);
        }

        private async Task DeleteCall(IEnumerable<SysRoleAc> sysRoles)
        {
            await SysRoleService.Delete(sysRoles.ToList().ConvertAll(it => new BaseIdInput()
            { Id = it.Id }));
        }

        private async Task EditCall(RoleEditInput input)
        {
            await SysRoleService.Edit(input);
        }

        private void FilterHeaders(List<DataTableHeader<SysRoleAc>> datas)
        {
            datas.RemoveWhere(it => it.Value == nameof(SysRoleAc.ExtJson));
            datas.RemoveWhere(it => it.Value == nameof(SysRoleAc.Id));
            datas.RemoveWhere(it => it.Value == nameof(SysRoleAc.Code));
            foreach (var item in datas)
            {
                item.Sortable = false;
                item.Filterable = false;
                item.Divider = false;
                item.Align = DataTableHeaderAlign.Start;
                item.CellClass = " table-minwidth ";
                switch (item.Value)
                {
                    case nameof(SysRoleAc.Name):
                        item.Sortable = true;
                        break;

                    case nameof(SysRoleAc.SortCode):
                        item.Sortable = true;
                        break;
                }
            }
        }

        private async Task<SqlSugarPagedList<SysRoleAc>> QueryCall(RolePageInput input)
        {
            var data = await SysRoleService.Page(input);
            return data;
        }
    }
}