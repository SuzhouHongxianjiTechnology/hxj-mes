#region copyright
//------------------------------------------------------------------------------
//  此代码版权声明为全文件覆盖，如有原作者特别声明，会在下方手动补充
//  此代码版权（除特别声明外的代码）归作者本人AlbertZhao所有
//  源代码使用协议遵循本仓库的开源协议及附加协议
//  Gitee源代码仓库：https://gitee.com/AlbertZhao/AlbertCollection



//------------------------------------------------------------------------------
#endregion

using Masa.Blazor.Presets;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Hosting;

using AlbertCollection.Core.Utils;
using AlbertCollection.Web.Rcl.Core;

namespace AlbertCollection.Web.Rcl
{
    public partial class Login
    {
        private string CaptchaValue;

        private LoginInput loginModel = new LoginInput();

        [Inject]
        public AjaxService AjaxService { get; set; }

        [Inject]
        public IAuthService AuthService { get; set; }

        [Inject]
        public IConfigService ConfigService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; } = default!;

        [Parameter]
        public string UserLogoUrl { get; set; } = BlazorConst.ResourceUrl + "images/defaultUser.svg";

        [Parameter]
        public string Welcome { get; set; }

        private ValidCodeOutPut CaptchaInfo { get; set; }

        private string Password { get; set; }

        private string SYS_DEFAULT_REMARK { get; set; }

        private string SYS_DEFAULT_TITLE { get; set; }

        private async Task Enter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                await LoginAsync();
            }
        }
        private PImageCaptcha captcha;
        private async Task LoginAsync()
        {
            loginModel.ValidCodeReqNo = CaptchaInfo?.ValidCodeReqNo;
            loginModel.ValidCode = CaptchaValue;
            loginModel.Password = CryptogramUtil.Sm4Encrypt(Password);
            if (IsMobile)
            {
                loginModel.Device = AuthDeviceTypeEnum.APP;
            }
            else
            {
                loginModel.Device = AuthDeviceTypeEnum.PC;
            }

            var ajaxOption = new AjaxOption { Url = "/auth/b/login", Data = loginModel, };
            var str = await AjaxService.GetMessageAsync(ajaxOption);
            if (str != null)
            {
                var ret = str.ToJsonEntity<UnifyResult<LoginOutPut>>();
                if (ret.Code != 200)
                {
                    if (captcha != null)
                    {
                        await captcha.RefreshCode();
                    }
                    await PopupService.EnqueueSnackbarAsync(new(T("登录错误") + ": " + ret.Msg.ToString(), AlertTypes.Error));
                }
                else
                {
                    await PopupService.EnqueueSnackbarAsync(new(T("登录成功"), AlertTypes.Success));
                    await Task.Delay(500);
                    if (NavigationManager.ToAbsoluteUri(NavigationManager.Uri).AbsolutePath == "/Login")
                        await AjaxService.GotoAsync("index");
                    else
                        await AjaxService.GotoAsync(NavigationManager.Uri);
                }
            }
            else
            {
                if (captcha != null)
                {
                    await captcha.RefreshCode();
                }
                await PopupService.EnqueueSnackbarAsync(new(T("登录错误"), AlertTypes.Error));
            }
        }
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            if (App.HostEnvironment.IsDevelopment())
            {
                loginModel.Account = "superAdmin";
                Password = "superAdmin";
            }
            GetCaptchaInfo();
            SYS_DEFAULT_TITLE = (await ConfigService.GetByConfigKey(CateGoryConst.Config_SYS_BASE, DevConfigConst.SYS_DEFAULT_TITLE)).ConfigValue;
            SYS_DEFAULT_REMARK = (await ConfigService.GetByConfigKey(CateGoryConst.Config_SYS_BASE, DevConfigConst.SYS_DEFAULT_REMARK))?.ConfigValue;
            _showCaptcha = (await ConfigService.GetByConfigKey(CateGoryConst.Config_SYS_BASE, DevConfigConst.SYS_DEFAULT_CAPTCHA_OPEN))?.ConfigValue?.ToBoolean() == true;
            Welcome = T("欢迎使用") + SYS_DEFAULT_TITLE + "!";
            await base.OnInitializedAsync();
        }

        private void GetCaptchaInfo()
        {
            CaptchaInfo = AuthService.GetCaptchaInfo();
        }

        private Task<string> RefreshCode()
        {
            CaptchaInfo = AuthService.GetCaptchaInfo();
            return Task.FromResult(CaptchaInfo.CodeValue);
        }
    }
}