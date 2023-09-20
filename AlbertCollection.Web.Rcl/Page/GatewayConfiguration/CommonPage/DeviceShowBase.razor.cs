using System;
using AlbertCollection.Application.GlobalData;
using AlbertCollection.Core.Entity.Device;

namespace AlbertCollection.Web.Rcl.Page.GatewayConfiguration.CommonPage
{
    public partial class DeviceShowBase
    {
        [Parameter]
        public DeviceCollection Device { get; set; }

        private void HandleClick()
        {
            // 引发点击事件，并传递设备名称
            OnClickFromF?.Invoke(Device?.Name);
        }

        [Parameter]
        public Action<string> OnClickFromF { get; set; }

        bool _connectDisable = false;
        private async Task StartCollect()
        {
            await InvokeAsync(async () =>
            {
                await GlobalDeviceData.TryAddAndInitDevice(Device,false);
                StateHasChanged();
            });
            _connectDisable = true;
        }

        private async Task SingleStartCollect()
        {
            await InvokeAsync(async () =>
            {
                await GlobalDeviceData.TryAddAndInitDevice(Device,true);
                StateHasChanged();
            });
            _connectDisable = true;
        }


        private async Task StopCollect()
        {
            await GlobalDeviceData.FindS7ByName(Device.Name).StopDeviceAsync();
            _connectDisable = false;
        }
    }
}