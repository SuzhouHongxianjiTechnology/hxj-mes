/*
 *所有关于dv_machinery类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*dv_machineryService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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
using VOL.Core.Const;
using Autofac.Core;
using SqlSugar;
using VOL.BasicConfig.IRepositories;
using VOL.BasicConfig.IServices;
using ICacheService = VOL.Core.CacheManager.ICacheService;
using Microsoft.IdentityModel.Tokens;

namespace VOL.DeviceManager.Services
{
    public partial class dv_machineryService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Idv_machineryRepository _repository;//访问数据库
        private readonly Idv_machinery_typeRepository _repositoryMachineryType; // 设备类型仓库
        private readonly Ibs_coderuleService _coderuleService ;  // 编码规则服务
        private readonly ICacheService _cacheService;
        private readonly DeviceBaseService _deviceBaseService;
        private WebResponseContent webResponse = new();

        [ActivatorUtilitiesConstructor]
        public dv_machineryService(
            Idv_machineryRepository dbRepository,
            Idv_machinery_typeRepository repositoryMachineryType,
            Ibs_coderuleService coderuleService,
            IHttpContextAccessor httpContextAccessor,
            ICacheService cacheService,
            DeviceBaseService deviceBaseService
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            _repositoryMachineryType = repositoryMachineryType;
            _coderuleService = coderuleService;
            _cacheService = cacheService;
            _deviceBaseService = deviceBaseService;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }

        public override PageGridData<dv_machinery> GetPageData(PageDataOptions options)
        {
            // 如果值为 -1，则默认清空所有查询条件
            QueryRelativeList = list =>
            {
                if (list.Count > 0 
                    && list[0].Name == SystemConst.DV_MACHINERY_TYPE_ID 
                    && list[0].Value == "-1")
                {
                    list.Clear();
                }
            };

            return base.GetPageData(options);
        }

        public override WebResponseContent Add(SaveModel saveDataModel)
        {
            AddOnExecuting = (machinery, o) =>
            {
                if (string.IsNullOrEmpty(machinery.machinery_code))
                {
                    var lastCodeRule = repository
                        .FindAsIQueryable(x => true)
                        .OrderByDescending(d => d.machinery_id)
                        .First()?
                        .machinery_code;

                    machinery.machinery_code = _coderuleService.GetCahceCodeRule(lastCodeRule, typeof(dv_machinery).Name).Result;
                }

                //如果返回false,后面代码不会再执行
                if (repository.Exists(x => x.machinery_code == machinery.machinery_code))
                {
                    return webResponse.Error(ErrorConst.DV_CODE_EXIST);
                }

                // 加入存在，则会将 machinery 中的设备类型 ID 所有值查询出来而后赋值给数据库
                var machineryType = _deviceBaseService.GetDvMachineryType(machinery.machinery_type_id).Result;
                machinery.machinery_type_code = machineryType?.machinery_type_code;
                machinery.machinery_type_name = machineryType?.machinery_type_name;

                return webResponse.OK();
            };

            AddOnExecuted = (machinery, o) =>
            {
                var machineryList = _repository.FindAsIQueryable(x => true).ToList();
                _cacheService.AddObject(SystemConst.DV_MACHINERY_LIST, machineryList);

                return webResponse.OK();
            };

            return base.Add(saveDataModel);
        }

        public override WebResponseContent Update(SaveModel saveModel)
        {
            UpdateOnExecuted = (machinery, o, arg3, arg4) =>
            {
                var machineryList = _repository.FindAsIQueryable(x => true).ToList();
                _cacheService.AddObject(SystemConst.DV_MACHINERY_LIST, machineryList);

                return webResponse.OK();
            };

            return base.Update(saveModel);
        }

        public override WebResponseContent Del(object[] keys, bool delList = true)
        {
            DelOnExecuted = objects =>
            {
                var machineryList = _repository.FindAsIQueryable(x => true).ToList();
                _cacheService.AddObject(SystemConst.DV_MACHINERY_LIST, machineryList);

                return webResponse.OK();
            };

            return base.Del(keys, delList);
        }
    }
}
