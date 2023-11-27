/*
 *所有关于dv_dss_record类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*dv_dss_recordService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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

namespace VOL.DeviceManager.Services
{
    public partial class dv_dss_recordService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Idv_dss_recordRepository _repository;//访问数据库
        private readonly Ibs_coderuleService _coderuleService;  // 编码规则服务
        private readonly ICacheService _cacheService;
        private WebResponseContent webResponse = new();

        [ActivatorUtilitiesConstructor]
        public dv_dss_recordService(
            Idv_dss_recordRepository dbRepository,
            IHttpContextAccessor httpContextAccessor,
            Ibs_coderuleService coderuleService, 
            ICacheService cacheService
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
            AddOnExecuting = (dsRecord, o) =>
            {
                if (string.IsNullOrEmpty(dsRecord.record_code))
                {
                    var lastCodeRule = repository
                        .FindAsIQueryable(x => true)
                        .OrderByDescending(d => d.record_id)
                        .First()?
                        .record_code;

                    dsRecord.record_code = _coderuleService.GetCahceCodeRule(lastCodeRule, typeof(dv_dss_record).Name).Result;
                }

                //如果返回false,后面代码不会再执行
                if (repository.Exists(x => x.record_code == dsRecord.record_code))
                {
                    return webResponse.Error(ErrorConst.DV_CODE_EXIST);
                }

                return webResponse.OK();
            };

            return base.Add(saveDataModel);
        }
    }
}
