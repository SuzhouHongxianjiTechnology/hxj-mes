using AlbertCollection.Application.Services.Driver;
using AlbertCollection.Application.Services.Driver.Dto;
using AlbertCollection.Core.Entity.Device;
using HslCommunication.Profinet.Siemens;

namespace AlbertCollection.Application.Services.GatewayConfiguration
{
    /// <inheritdoc cref="IDriverPluginService"/>
    [Injection(Proxy = typeof(OperDispatchProxy))]
    public class DriverPluginService: DbRepository<DevicePlugin>, IDriverPluginService
    {
        private readonly SysCacheService _sysCacheService;

        /// <inheritdoc cref="IDriverPluginService"/>
        public DriverPluginService(SysCacheService sysCacheService)
        {
            _sysCacheService = sysCacheService;
        }

        public List<DevicePlugin> GetCacheList()
        {
            //先从Cache拿
            var driverPlugins = _sysCacheService.Get<List<DevicePlugin>>(CacheConst.Cache_DriverPlugin, "");
            if (driverPlugins == null)
            {
                driverPlugins = Context.Queryable<DevicePlugin>()
                .Select((u) => new DevicePlugin { Id = u.Id.SelectAll() })
                .ToList();
                if (driverPlugins != null)
                {
                    //插入Cache
                    _sysCacheService.Set(CacheConst.Cache_DriverPlugin, "", driverPlugins);
                }
            }
            return driverPlugins;
        }

        public DevicePlugin GetDriverPluginById(long Id)
        {
            var data = GetCacheList();
            return data.FirstOrDefault(it => it.Id == Id);
        }

        public List<DriverPluginCategory> GetDriverPluginChildrenList(SiemensPLCS? driverTypeEnum = null)
        {
            var data = GetCacheList();
            if (driverTypeEnum != null)
            {
                data = data.Where(a => a.SimPlcType == driverTypeEnum).ToList();
            }
            var driverPluginCategories = data.GroupBy(a => a.PluginName).Select(it =>
            {
                var childrens = new List<DriverPluginCategory>();
                foreach (var item in it)
                {
                    childrens.Add(new DriverPluginCategory
                    {
                        Id = item.Id,
                        Name = item.PluginFunction,
                        SimPlcType = item.SimPlcType,
                    }
                    );
                }
                return new DriverPluginCategory
                {
                    Id = YitIdHelper.NextId(),
                    Name = it.Key,
                    Children = childrens,
                };
            });
            return driverPluginCategories.ToList();
        }

        /// <inheritdoc/>
        public long? GetIdByName(string name)
        {
            var data = GetCacheList();
            return data.FirstOrDefault(it => it.PluginFunction == name)?.Id;
        }
        /// <inheritdoc/>
        public string GetNameById(long id)
        {
            var data = GetCacheList();
            return data.FirstOrDefault(it => it.Id == id)?.PluginFunction;
        }
        /// <inheritdoc/>
        public async Task<SqlSugarPagedList<DevicePlugin>> PageAsync(DriverPluginPageInput input)
        {
            var query = Context.Queryable<DevicePlugin>()
             .WhereIF(!string.IsNullOrEmpty(input.Name), u => u.PluginFunction.Contains(input.Name))//根据关键字查询
             .OrderByIF(!string.IsNullOrEmpty(input.SortField), $"{input.SortField} {input.SortOrder}")
             .OrderBy(u => u.Id)//排序
             .Select((u) => new DevicePlugin { Id = u.Id.SelectAll() })
             ;
            var pageInfo = await query.ToPagedListAsync(input.Current, input.Size);//分页
            return pageInfo;
        }
    }
}
