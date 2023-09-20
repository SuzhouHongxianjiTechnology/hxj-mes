#region copyright
//------------------------------------------------------------------------------
//  此代码版权声明为全文件覆盖，如有原作者特别声明，会在下方手动补充
//  此代码版权（除特别声明外的代码）归作者本人AlbertZhao所有
//  源代码使用协议遵循本仓库的开源协议及附加协议
//  Gitee源代码仓库：https://gitee.com/AlbertZhao/AlbertCollection



//------------------------------------------------------------------------------
#endregion

using BlazorComponent.I18n;

using AlbertCollection.Web.Rcl.Core;

namespace AlbertCollection.Web.Rcl
{
    public partial class MainLayout
    {
        private bool? _drawerOpen = true;

        [Inject]
        public I18n I18n { get; set; }

        [Inject]
        private UserResoures UserResoures { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await UserResoures.InitAllAsync();
            Navs = UserResoures.Menus.Parse();
            await base.OnInitializedAsync();
        }
        public async Task MenuChangeAsync()
        {
            await UserResoures.InitMenuAsync();
            Navs = UserResoures.Menus.Parse();
            StateHasChanged();
        }
        public async Task UserChangeAsync()
        {
            await UserResoures.InitUserAsync();
            StateHasChanged();
        }
        private void LanguageChange(string name)
        {
            I18n.SetCulture(new CultureInfo(name));
        }
    }
}