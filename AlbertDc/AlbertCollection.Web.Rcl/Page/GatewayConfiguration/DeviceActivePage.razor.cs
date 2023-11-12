using AlbertCollection.Web.Rcl;
using AlbertCollection.Application.Services.Driver.Dto;
using AlbertCollection.Core.Entity.Device;
using BlazorComponent;

namespace ThingsGateway.Web.Page
{
    public partial class DeviceActivePage
    {
        private IAppDataTable _datatable;
        private DriverPluginPageInput search = new();
        [CascadingParameter]
        MainLayout MainLayout { get; set; }

        private async Task<SqlSugarPagedList<DevicePlugin>> QueryCall(DriverPluginPageInput input)
        {
            var data = await DriverPluginService.PageAsync(input);
            return data;
        }

        private void FilterHeaders(List<DataTableHeader<DevicePlugin>> datas)
        {
            datas.RemoveWhere(it => it.Value == nameof(DevicePlugin.CreateUserId));
            datas.RemoveWhere(it => it.Value == nameof(DevicePlugin.UpdateUserId));
            datas.RemoveWhere(it => it.Value == nameof(DevicePlugin.IsDelete));
            datas.RemoveWhere(it => it.Value == nameof(DevicePlugin.ExtJson));
            datas.RemoveWhere(it => it.Value == nameof(DevicePlugin.Id));
            datas.RemoveWhere(it => it.Value == nameof(DevicePlugin.ExtJson));
            foreach (var item in datas)
            {
                item.Sortable = false;
                item.Filterable = false;
                item.Divider = false;
                item.Align = DataTableHeaderAlign.Start;
                item.CellClass = " table-minwidth ";
                switch (item.Value)
                {
                    case nameof(DevicePlugin.PluginFunction):
                        item.Sortable = true;
                        break;
                }
            }
        }

        private void Filters(List<Filters> datas)
        {
            foreach (var item in datas)
            {
                switch (item.Key)
                {
                    case nameof(DevicePlugin.CreateTime):
                    case nameof(DevicePlugin.UpdateTime):
                    case nameof(DevicePlugin.CreateUser):
                    case nameof(DevicePlugin.UpdateUser):
                        item.Value = false;
                        break;
                }
            }
        }
    }
}