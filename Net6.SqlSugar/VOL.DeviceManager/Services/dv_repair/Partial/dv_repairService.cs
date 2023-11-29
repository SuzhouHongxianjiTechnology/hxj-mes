/*
 *所有关于dv_repair类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*dv_repairService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
*/
using VOL.Core.BaseProvider;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;
using System.Linq;
using VOL.Core.Utilities;
using System.Linq.Expressions;
using VOL.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using VOL.DeviceManager.IRepositories;
using VOL.BasicConfig.IServices;
using VOL.Core.CacheManager;
using VOL.Core.Const;
using Newtonsoft.Json;

namespace VOL.DeviceManager.Services
{
    public partial class dv_repairService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Idv_repairRepository _repository;//访问数据库
        private readonly Idv_machineryRepository _machineryRepository; // 设备台账数据库
        private readonly Ibs_coderuleService _coderuleService;  // 编码规则服务
        private readonly ICacheService _cacheService;
        private readonly DeviceBaseService _deviceBaseService;
        private readonly WebResponseContent webResponse = new();

        [ActivatorUtilitiesConstructor]
        public dv_repairService(
            Idv_repairRepository dbRepository,
            Idv_machineryRepository machineryRepository,
            IHttpContextAccessor httpContextAccessor,
            Ibs_coderuleService coderuleService,
            ICacheService cacheService,
            DeviceBaseService deviceBaseService
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            _machineryRepository = machineryRepository;
            _coderuleService = coderuleService;
            _cacheService = cacheService;
            _deviceBaseService = deviceBaseService;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }

        public override WebResponseContent Add(SaveModel saveDataModel)
        {
            AddOnExecuting = (repair, o) =>
            {
                if (string.IsNullOrEmpty(repair.repair_code))
                {
                    var lastCodeRule = repository
                        .FindAsIQueryable(x => true)
                        .OrderByDescending(d => d.repair_id)
                        .First()?
                        .repair_code;

                    repair.repair_code = _coderuleService.GetCahceCodeRule(lastCodeRule, typeof(dv_repair).Name).Result;

                    var repairSearch = _deviceBaseService.GetDvMachinery(repair.machinery_code);
                    repair.machinery_id = repairSearch.machinery_id;
                    repair.machinery_type_id = repairSearch.machinery_type_id;
                }

                //如果返回false,后面代码不会再执行
                if (repository.Exists(x => x.repair_code == repair.repair_code))
                {
                    return webResponse.Error(ErrorConst.DV_CODE_EXIST);
                }

                return webResponse.OK();
            };

            AddOnExecuted = (repair, o) =>
            {
                // 这边将维修状态直接返回到设备状态上
                var machinery = _deviceBaseService.GetDvMachinery(repair.machinery_code);

                if (repair.repair_result == SystemConst.REPAIRED)
                {
                    machinery.status = SystemConst.STOP;
                }
                else
                {
                    machinery.status = SystemConst.REPAIR;
                }
                
                _machineryRepository.Update(machinery);
                _machineryRepository.SaveChanges();
                _cacheService.AddObject(SystemConst.DV_MACHINERY_LIST, _machineryRepository.FindAsIQueryable(x => true).ToList());

                return webResponse.OK();
            };

            return base.Add(saveDataModel);
        }

        public override WebResponseContent Update(SaveModel saveModel)
        {
            UpdateOnExecuted = (repair, o, arg3, arg4) =>
            {
                // 这边将维修状态直接返回到设备状态上
                var machinery = _deviceBaseService.GetDvMachinery(repair.machinery_code);

                if (repair.repair_result == SystemConst.REPAIRED)
                {
                    machinery.status = SystemConst.STOP;
                }
                else
                {
                    machinery.status = SystemConst.REPAIR;
                }

                _machineryRepository.Update(machinery);
                _machineryRepository.SaveChanges();
                _cacheService.AddObject(SystemConst.DV_MACHINERY_LIST, _machineryRepository.FindAsIQueryable(x => true).ToList());

                return webResponse.OK();
            };

            return base.Update(saveModel);
        }
    }
}
