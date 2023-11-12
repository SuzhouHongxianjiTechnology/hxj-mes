using Masa.Blazor;

namespace AlbertCollection.Web.Rcl.Core
{
    public partial class UserMenu
    {
        [Inject]
        private UserResoures UserResoures { get; set; }

        [Inject]
        public AjaxService AjaxService { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        private async Task LoginoutAsync()
        {
            var ajaxOption = new AjaxOption{Url = "/auth/b/loginOut", };
            var str = await AjaxService.GetMessageAsync(ajaxOption);
            var ret = str?.ToJsonEntity<UnifyResult<string>>();
            if (ret?.Code != 200)
            {
                await PopupService.EnqueueSnackbarAsync(T("注销失败"), AlertTypes.Error);
            }
            else
            {
                await PopupService.EnqueueSnackbarAsync(T("注销成功"), AlertTypes.Success);
                await Task.Delay(500);
                await AjaxService.GotoAsync("/");
            }
        }
    }
}