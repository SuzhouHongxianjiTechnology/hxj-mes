using AlbertCollection.Application.BgWorkers;
using AlbertCollection.Application.GlobalData;
using AlbertCollection.Application.Services.Driver.Dto;
using AlbertCollection.Core.Const;
using AlbertCollection.Core.Entity.Device;
using HslCommunication.Profinet.Siemens;
using Microsoft.Extensions.Logging;

namespace AlbertCollection.Web.Rcl
{
    public partial class DeviceDebug
    {
        string _searchName;
        string _ip = "127.0.0.1";
        string _port = "102";
        string _rack;
        string _slot;
        string _readAddress;
        string _strLength;
        string _wAddress;
        string _value;
        string _result;
        string _text = "初始化测试";
        bool IsShowTreeView = true;
        bool connectDisable = false;
        DriverPluginCategory selectedDevice;
        string? selectedName;
        List<DriverPluginCategory> DriverPlugins;
        List<string> _readButtonList = new()
        {DeviceConst.DEV_TYPE_BOOL, DeviceConst.DEV_TYPE_BYTE, DeviceConst.DEV_TYPE_SHORT, DeviceConst.DEV_TYPE_USHORT, DeviceConst.DEV_TYPE_INT, DeviceConst.DEV_TYPE_UINT, DeviceConst.DEV_TYPE_LONG, DeviceConst.DEV_TYPE_ULONG, DeviceConst.DEV_TYPE_FLOAT, DeviceConst.DEV_TYPE_DOUBLE, DeviceConst.DEV_TYPE_STRING, };
        List<string> writeButtonList = new()
        {DeviceConst.DEV_TYPE_BOOL, DeviceConst.DEV_TYPE_BYTE, DeviceConst.DEV_TYPE_SHORT, DeviceConst.DEV_TYPE_USHORT, DeviceConst.DEV_TYPE_UINT, DeviceConst.DEV_TYPE_INT, DeviceConst.DEV_TYPE_LONG, DeviceConst.DEV_TYPE_ULONG, DeviceConst.DEV_TYPE_FLOAT, DeviceConst.DEV_TYPE_DOUBLE, DeviceConst.DEV_TYPE_STRING, };
        protected override void OnInitialized()
        {
            DriverPlugins = DriverPluginService.GetDriverPluginChildrenList();
            base.OnInitialized();
        }

        private async Task ConnectPlc()
        {
            // 测试初始化代码
            if (string.IsNullOrEmpty(selectedName))
            {
                BackMessage.AddMessage(DeviceConst.DEV_PARAM_LOSS_PLC, LogLevel.Error);
                return;
            }

            if (string.IsNullOrEmpty(_ip) && string.IsNullOrEmpty(_port))
            {
                BackMessage.AddMessage(DeviceConst.DEV_PARAM_LOSS_IP, LogLevel.Error);
                return;
            }

            await GlobalDeviceData.TryAddAndInitDevice(new DeviceCollection()
            {Name = selectedDevice?.Name, IpAddress = _ip, Port = _port, SimPlcType = selectedDevice?.SimPlcType ?? SiemensPLCS.S1500 },true);
            connectDisable = true;
        }

        private async Task DisConnectPlc()
        {
            connectDisable = false;
            await GlobalDeviceData.FindS7ByName(selectedName).StopDeviceAsync();
            await Task.Delay(500);
            // 强制刷新，让消息队列刷新到界面上
            await InvokeAsync(StateHasChanged);
        }

        private void ReadDataByS7(string type)
        {
            if (string.IsNullOrEmpty(selectedName))
            {
                BackMessage.AddMessage(DeviceConst.DEV_PARAM_LOSS_PLC, LogLevel.Error);
                return;
            }

            GlobalDeviceData.FindS7ByName(selectedName).ReadDataCommon(type, _readAddress, _strLength, out var _result);
            BackMessage.AddMessage(string.IsNullOrEmpty(_result) ? DeviceConst.DEV_READ_NG : DeviceConst.DEV_READ_OK + "-" + _result, LogLevel.Information);
        }

        private void WriteDataByS7(string type)
        {
            if (string.IsNullOrEmpty(selectedName))
            {
                BackMessage.AddMessage(DeviceConst.DEV_PARAM_LOSS_PLC, LogLevel.Error);
                return;
            }

            GlobalDeviceData.FindS7ByName(selectedName).WriteDataCommon(type, _wAddress, _value, out var result);
            BackMessage.AddMessage(result ? DeviceConst.DEV_WRITE_OK + "-" + _value : DeviceConst.DEV_WRITE_NG + "-" + _value, LogLevel.Information);
        }
    }
}