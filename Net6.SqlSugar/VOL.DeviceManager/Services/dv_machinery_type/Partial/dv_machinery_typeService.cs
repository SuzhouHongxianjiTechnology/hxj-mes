/*
 *所有关于dv_machinery_type类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*dv_machinery_typeService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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
using SqlSugar;
using VOL.DeviceManager.IRepositories;
using VOL.Core.Const;
using ICacheService = VOL.Core.CacheManager.ICacheService;
using System.Reflection.PortableExecutable;
using VOL.BasicConfig.Services;
using VOL.BasicConfig.IServices;

namespace VOL.DeviceManager.Services
{
    public partial class dv_machinery_typeService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Idv_machinery_typeRepository _repository;//访问数据库
        private readonly Ibs_coderuleService _coderuleService;  // 编码规则服务
        private readonly ICacheService _cacheService;
        private WebResponseContent webResponse = new();

        [ActivatorUtilitiesConstructor]
        public dv_machinery_typeService(
            Idv_machinery_typeRepository dbRepository,
            IHttpContextAccessor httpContextAccessor,
            ICacheService cacheService,
            Ibs_coderuleService coderuleService
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            _coderuleService = coderuleService;
            _cacheService = cacheService;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }

        public override WebResponseContent Add(SaveModel saveDataModel)
        {
            AddOnExecuting = (type, o) =>
            {
                if (string.IsNullOrEmpty(type.machinery_type_code))
                {
                    var lastCodeRule = repository
                        .FindAsIQueryable(x => true)
                        .OrderByDescending(d => d.machinery_type_id)
                    .First()?
                    .machinery_type_code;

                    type.machinery_type_code = _coderuleService.GetCahceCodeRule(lastCodeRule, typeof(dv_machinery_type).Name).Result;
                }

                //如果返回false,后面代码不会再执行
                if (repository.Exists(x => x.machinery_type_code == type.machinery_type_code))
                {
                    return webResponse.Error(ErrorConst.DV_CODE_EXIST);
                }


                var machineryTypeList = _repository.FindAsIQueryable(x => true).ToList();
                _cacheService.AddObject(SystemConst.DV_MACHINERY_TYPE, machineryTypeList);

                return webResponse.OK();
            };

            AddOnExecuted = (type, o) =>
            {
                var machineryTypeList = _repository.FindAsIQueryable(x => true).ToList();
                _cacheService.AddObject(SystemConst.DV_MACHINERY_TYPE, machineryTypeList);

                return webResponse.OK();
            };

            return base.Add(saveDataModel);
        }

        public override WebResponseContent Update(SaveModel saveModel)
        {
            UpdateOnExecuted = (type, o, arg3, arg4) =>
            {
                var machineryTypeList = _repository.FindAsIQueryable(x => true).ToList();
                _cacheService.AddObject(SystemConst.DV_MACHINERY_TYPE, machineryTypeList);

                return webResponse.OK();
            };

            return base.Update(saveModel);
        }

        public override WebResponseContent Del(object[] keys, bool delList = true)
        {
            DelOnExecuted = objects =>
            {
                var machineryTypeList = _repository.FindAsIQueryable(x => true).ToList();
                _cacheService.AddObject(SystemConst.DV_MACHINERY_TYPE, machineryTypeList);

                return webResponse.OK();
            };

            return base.Del(keys, delList);
        }

        /// <summary>
        /// 获取所有设备类型节点树
        /// </summary>
        /// <returns></returns>
        public async Task<WebResponseContent> GetAllMachineryTypeTreeAsync()
        {
            try
            {
                var machineryTypeList =await GetDvMachineryTypeListAsync();

                var data = machineryTypeList
                    .Select(d => new
                    {
                        d.machinery_type_id,
                        d.parent_type_id,
                        d.machinery_type_code,
                        d.machinery_type_name
                    }).ToList();

                webResponse.Data = data;
                return webResponse.OK();

            }
            catch (Exception e)
            {
                return webResponse.Error();
            }
        }

        private async Task<List<dv_machinery_type>?> GetDvMachineryTypeListAsync()
        {
            var machineryTypeList = _cacheService.Get<List<dv_machinery_type>>(SystemConst.DV_MACHINERY_TYPE);

            if (machineryTypeList == null)
            {
                machineryTypeList = await _repository.FindAsIQueryable(x => true).ToListAsync();
                _cacheService.AddObject(SystemConst.DV_MACHINERY_TYPE, machineryTypeList);
            }

            return machineryTypeList;
        }
    }
}
