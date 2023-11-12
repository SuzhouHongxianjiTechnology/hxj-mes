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

using System;
using System.Linq;

namespace AlbertCollection.Web.Rcl
{
    public partial class User
    {
        private IAppDataTable _datatable;
        private UserPageInput search = new();

        private async Task AddCall(UserAddInput input)
        {
            await SysUserService.Add(input);
        }

        private async Task DeleteCall(IEnumerable<SysUserAc> users)
        {
            await SysUserService.Delete(users.ToList().ConvertAll(it => it.Id.ToIdInput()));
        }

        private async Task EditCall(UserEditInput users)
        {
            await SysUserService.Edit(users);
        }

        private void FilterHeaders(List<DataTableHeader<SysUserAc>> datas)
        {
            datas.RemoveWhere(it => it.Value == nameof(SysUserAc.Password));
            datas.RemoveWhere(it => it.Value == nameof(SysUserAc.ButtonCodeList));
            datas.RemoveWhere(it => it.Value == nameof(SysUserAc.PermissionCodeList));
            datas.RemoveWhere(it => it.Value == nameof(SysUserAc.RoleCodeList));
            datas.RemoveWhere(it => it.Value == nameof(SysUserAc.RoleIdList));
            datas.RemoveWhere(it => it.Value == nameof(SysUserAc.CreateUserId));
            datas.RemoveWhere(it => it.Value == nameof(SysUserAc.UpdateUserId));
            datas.RemoveWhere(it => it.Value == nameof(SysUserAc.IsDelete));
            datas.RemoveWhere(it => it.Value == nameof(SysUserAc.ExtJson));
            datas.RemoveWhere(it => it.Value == nameof(SysUserAc.Id));

            foreach (var item in datas)
            {
                item.Sortable = false;
                item.Filterable = false;
                item.Divider = false;
                item.Align = DataTableHeaderAlign.Start;
                item.CellClass = " table-minwidth ";
                switch (item.Value)
                {
                    case nameof(SysUserAc.Account):
                        item.Sortable = true;
                        break;

                    case nameof(SysUserAc.LastLoginTime):
                        item.Sortable = true;
                        break;

                    case nameof(SysUserAc.LatestLoginTime):
                        item.Sortable = true;
                        break;

                    case nameof(SysUserAc.UserStatus):
                        item.Sortable = true;
                        break;

                    case BlazorConst.TB_Actions:
                        item.CellClass = "";
                        break;
                }
            }
        }
        private void Filters(List<Filters> datas)
        {
            foreach (var item in datas)
            {
                switch (item.Key)
                {
                    case nameof(SysUserAc.Email):
                    case nameof(SysUserAc.LastLoginDevice):
                    case nameof(SysUserAc.LastLoginIp):
                    case nameof(SysUserAc.LastLoginTime):
                    case nameof(SysUserAc.SortCode):
                    case nameof(SysUserAc.CreateTime):
                    case nameof(SysUserAc.UpdateTime):
                    case nameof(SysUserAc.CreateUser):
                    case nameof(SysUserAc.UpdateUser):
                        item.Value = false;
                        break;

                }
            }
        }

        private async Task<SqlSugarPagedList<SysUserAc>> QueryCall(UserPageInput input)
        {
            var data = await SysUserService.Page(input);
            return data;
        }

        private async Task ResetPassword(SysUserAc sysUser)
        {
            await SysUserService.ResetPassword(sysUser.Id.ToIdInput());
            await PopupService.EnqueueSnackbarAsync(new(T("成功"), AlertTypes.Success));
        }

        private async Task UserStatusChange(SysUserAc context, bool enable)
        {
            try
            {
                if (enable)
                    await SysUserService.EnableUser(context.Id.ToIdInput());
                else
                    await SysUserService.DisableUser(context.Id.ToIdInput());
            }
            catch (Exception ex)
            {
                await PopupService.EnqueueSnackbarAsync(new(ex.Message, AlertTypes.Error));
            }
            finally
            {
                await _datatable?.QueryClickAsync();
            }
        }
    }
}