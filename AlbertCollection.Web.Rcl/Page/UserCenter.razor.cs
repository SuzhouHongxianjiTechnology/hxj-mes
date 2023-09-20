using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using AlbertCollection.Application;
using AlbertCollection.Core;
using System.Linq.Expressions;
using System.Globalization;
using Masa.Blazor.Presets;
using BlazorComponent;
using NewLife.Serialization;
using AlbertCollection.Web.Rcl.Core;
using SqlSugar;
using Microsoft.Extensions.Options;
using AlbertCollection.Web.Rcl;
using System.ComponentModel;
using Masa.Blazor;
using AlbertCollection.Core.Extension;

namespace AlbertCollection.Web.Rcl
{
    public partial class UserCenter
    {
        StringNumber tab;
        List<long> _menusChoice = new();
        UpdateInfoInput _updateInfoInput = new();
        PasswordInfoInput _passwordInfoInput = new();
        [Inject]
        IUserCenterService UserCenterService { get; set; }

        [CascadingParameter]
        MainLayout MainLayout { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                _updateInfoInput.Email = UserResoures.CurrentUser.Email;
                _updateInfoInput.Phone = UserResoures.CurrentUser.Phone;
            }

            base.OnAfterRender(firstRender);
        }

        [Inject]
        public AjaxService AjaxService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _menusChoice = await UserCenterService.GetLoginWorkbench();
            await base.OnInitializedAsync();
        }

        async Task OnShortcutSave()
        {
            await UserCenterService.UpdateWorkbench(_menusChoice);
            await MainLayout.MenuChangeAsync();
            await PopupService.EnqueueSnackbarAsync(T("成功"), AlertTypes.Success);
        }

        async Task OnUpdateUserInfo()
        {
            await UserCenterService.UpdateUserInfo(_updateInfoInput);
            await MainLayout.UserChangeAsync();
            await PopupService.EnqueueSnackbarAsync(T("成功"), AlertTypes.Success);
        }

        async Task OnUpdatePasswordInfo()
        {
            _passwordInfoInput.Id = UserResoures.CurrentUser.Id;
            await UserCenterService.EditPassword(_passwordInfoInput);
            await MainLayout.UserChangeAsync();
            await PopupService.EnqueueSnackbarAsync(T("成功，将重新登录"), AlertTypes.Success);
            await Task.Delay(2000);
            await AjaxService.GotoAsync("/");
        }
    }
}