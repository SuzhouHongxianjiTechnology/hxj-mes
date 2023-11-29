using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOL.Core.BaseProvider;
using VOL.Core.CacheManager;
using VOL.Core.Const;
using VOL.Core.Extensions.AutofacManager;
using VOL.DeviceManager.IRepositories;
using VOL.Entity.DomainModels;

namespace VOL.DeviceManager.Services
{
    public class DeviceBaseService: IDependency
    {
        private readonly ICacheService _cacheService;
        private readonly Idv_machineryRepository _repoMachinery; // 设备台账仓库
        private readonly Idv_machinery_typeRepository _repoMachineryType;  // 设备类型仓库
        

        public DeviceBaseService(
            ICacheService cacheService,
            Idv_machineryRepository repoMachinery,
            Idv_machinery_typeRepository repoMachineryType)
        {
            _cacheService = cacheService;
            _repoMachinery = repoMachinery;
            _repoMachineryType = repoMachineryType;
        }

        /// <summary>
        /// 获取所有设备类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<dv_machinery_type>?> GetDvMachineryTypeListAsync()
        {
            var machineryTypeList = _cacheService.Get<List<dv_machinery_type>>(SystemConst.DV_MACHINERY_TYPE_LIST);

            if (machineryTypeList == null)
            {
                machineryTypeList = await _repoMachineryType.FindAsIQueryable(x => true).ToListAsync();
                _cacheService.AddObject(SystemConst.DV_MACHINERY_TYPE, machineryTypeList);
            }

            return machineryTypeList;
        }

        /// <summary>
        /// 根据 ID 获取指定设备类型
        /// </summary>
        /// <param name="machineryTypeId"></param>
        /// <returns></returns>
        public async Task<dv_machinery_type?> GetDvMachineryType(long machineryTypeId)
        {
            var machineryType =
                (await GetDvMachineryTypeListAsync()).First(x => x.machinery_type_id == machineryTypeId);

            return machineryType;
        }

        /// <summary>
        /// 根据设备编码获取指定设备
        /// </summary>
        /// <param name="machineryCode"></param>
        /// <returns></returns>
        public dv_machinery GetDvMachinery(string machineryCode)
        {
            var machineryList = _cacheService.Get<List<dv_machinery>>(SystemConst.DV_MACHINERY_LIST);

            if (machineryList == null)
            {
                machineryList = _repoMachinery.FindAsIQueryable(x => true).ToList();
                _cacheService.AddObject(SystemConst.DV_MACHINERY_LIST, machineryList);
            }

            return machineryList.First(x => x.machinery_code == machineryCode);
        }
    }
}
