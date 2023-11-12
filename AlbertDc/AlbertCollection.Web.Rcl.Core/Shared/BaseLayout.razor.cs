using AlbertCollection.Web.Rcl.Core;
using Masa.Blazor;

namespace AlbertCollection.Web.Rcl
{
    public partial class BaseLayout
    {
        [Inject]
        UserResoures UserResoures { get; set; }

        [Inject]
        public MasaBlazor MasaBlazor { get; set; }

        [Inject]
        IPopupService PopupService { get; set; }

        [Inject]
        JsInitVariables JsInitVariables { get; set; }

        public bool IsMobile { get; set; }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await JsInitVariables.SetTimezoneOffsetAsync();
            await base.OnAfterRenderAsync(firstRender);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            MasaBlazor.Breakpoint.MobileBreakpoint = 666;
            MasaBlazor.Breakpoint.OnUpdate += BreakpointOnOnUpdate;
        }

        private void BreakpointOnOnUpdate(object sender, BreakpointChangedEventArgs e)
        {
            if (e.MobileChanged)
            {
                IsMobile = MasaBlazor.Breakpoint.Mobile;
                StateHasChanged();
            }
        }

        public void Dispose()
        {
            MasaBlazor.Breakpoint.OnUpdate -= BreakpointOnOnUpdate;
        }
    }
}