using TouchSocket.Core;

namespace ThingsGateway.Web.Page
{
    public partial class HardwareInfoPage
    {
        private System.Timers.Timer DelayTimer;
        protected override Task OnInitializedAsync()
        {
            DelayTimer = new System.Timers.Timer(8000);
            DelayTimer.Elapsed += timer_Elapsed;
            DelayTimer.AutoReset = true;
            DelayTimer.Start();
            return base.OnInitializedAsync();
        }

        async void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            await InvokeAsync(StateHasChanged);
        }

        protected override async Task DisposeAsync(bool disposing)
        {
            await base.DisposeAsync(disposing);
            DelayTimer?.SafeDispose();
        }
    }
}