using BlazorComponent;
using AlbertCollection.Application.GlobalData;
using AlbertCollection.Core.Entity.Device;

namespace AlbertCollection.Web.Rcl.Page.GatewayConfiguration.CommonPage
{
    public partial class StationShowBase
    {
        [Parameter]
        public DeviceSeq DeviceSeq { get; set; }

        private async Task StartCollect()
        {
            GlobalDeviceData.StartStationSeq(DeviceSeq.DeviceName, DeviceSeq.SeqName);
        }

        private async Task StopCollect()
        {
            GlobalDeviceData.StopStationSeq(DeviceSeq.DeviceName, DeviceSeq.SeqName);
        }
    }
}