using System.Linq;
using AlbertCollection.Application.GlobalData;
using AlbertCollection.Core.Entity.Device;

namespace AlbertCollection.Web.Rcl.Page.GatewayConfiguration
{
    public partial class CollectDevicePage
    {
        private List<DeviceCollection> _deviceCollections;
        private DeviceCollection _device;

        protected override Task OnParametersSetAsync()
        {
            base.OnParametersSetAsync();

            _deviceCollections = GlobalDeviceData.DeviceCollectionList;

            return Task.CompletedTask;
        }

        private async void HandleDeviceClick(string deviceName)
        {
            await InvokeAsync(() =>
            {
                _device = _deviceCollections.FirstOrDefault(d => d.Name == deviceName);
                // 必须整个页面 render，不然 Station 无法更新
                StateHasChanged();
            });
        }
    }
}