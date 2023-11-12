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
    public partial class Spa
    {
        private IAppDataTable _datatable;
        private SpaPageInput search = new();

        private async Task AddCall(SpaAddInput input)
        {
            await SpaService.Add(input);
        }

        private async Task DeleteCall(IEnumerable<SysResourceAc> input)
        {
            await SpaService.Delete(input.ToList().ConvertAll(it => new BaseIdInput()
            { Id = it.Id }));
        }

        private async Task EditCall(SpaEditInput input)
        {
            await SpaService.Edit(input);
        }
        private void Filters(List<Filters> datas)
        {
            foreach (var item in datas)
            {
                switch (item.Key)
                {
                    case nameof(SysResourceAc.Code):
                    case nameof(SysResourceAc.Category):
                    case nameof(SysResourceAc.CreateTime):
                    case nameof(SysResourceAc.UpdateTime):
                    case nameof(SysResourceAc.CreateUser):
                    case nameof(SysResourceAc.TargetType):
                    case nameof(SysResourceAc.UpdateUser):
                        item.Value = false;
                        break;
                }
            }
        }

        private void FilterHeaders(List<DataTableHeader<SysResourceAc>> datas)
        {

            datas.RemoveWhere(it => it.Value == nameof(SysResourceAc.ParentId));
            datas.RemoveWhere(it => it.Value == nameof(SysResourceAc.CreateUserId));
            datas.RemoveWhere(it => it.Value == nameof(SysResourceAc.UpdateUserId));

            datas.RemoveWhere(it => it.Value == nameof(SysResourceAc.IsDelete));
            datas.RemoveWhere(it => it.Value == nameof(SysResourceAc.ExtJson));
            datas.RemoveWhere(it => it.Value == nameof(SysResourceAc.Id));

            datas.RemoveWhere(it => it.Value == nameof(SysResourceAc.Children));

            foreach (var item in datas)
            {
                item.Sortable = false;
                item.Filterable = false;
                item.Divider = false;
                item.Align = DataTableHeaderAlign.Start;
                item.CellClass = " table-minwidth ";
                switch (item.Value)
                {
                    case nameof(SysResourceAc.Name):
                        item.Sortable = true;
                        break;

                    case nameof(SysResourceAc.SortCode):
                        item.Sortable = true;
                        break;
                }
            }
        }

        private async Task<SqlSugarPagedList<SysResourceAc>> QueryCall(SpaPageInput input)
        {
            var data = await SpaService.Page(input);
            return data;
        }
    }
}