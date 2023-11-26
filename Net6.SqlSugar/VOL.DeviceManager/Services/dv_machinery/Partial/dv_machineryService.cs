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
using VOL.Core.CacheManager;
using VOL.DeviceManager.IRepositories;
using VOL.Core.Const;
using Autofac.Core;

namespace VOL.DeviceManager.Services
{
    public partial class dv_machineryService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Idv_machineryRepository _repository;//访问数据库
        private readonly Idv_machinery_typeRepository _repositoryMachineryType; // 设备类型仓库
        private readonly ICacheService _cacheService;
        private WebResponseContent webResponse = new();

        [ActivatorUtilitiesConstructor]
        public dv_machineryService(
            Idv_machineryRepository dbRepository,
            Idv_machinery_typeRepository repositoryMachineryType,
            IHttpContextAccessor httpContextAccessor,
            ICacheService cacheService
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            _repositoryMachineryType = repositoryMachineryType;
            _cacheService = cacheService;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }

        public override PageGridData<dv_machinery> GetPageData(PageDataOptions options)
        {
            // 如果值为 -1，则默认清空所有查询条件
            QueryRelativeList = list =>
            {
                if (list.Count > 0 && list[0].Name == "machinery_type_id" && list[0].Value == "-1")
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
                //如果返回false,后面代码不会再执行
                if (repository.Exists(x => x.machinery_code == machinery.machinery_code))
                {
                    return webResponse.Error(ErrorConst.DV_MACHINERY_CODE_EXIST);
                }

                // 加入存在，则会将 machinery 中的设备类型 ID 所有值查询出来而后赋值给数据库
                var machineryType = GetDvMachineryType(machinery.machinery_type_id);
                machinery.machinery_type_code = machineryType?.machinery_type_code;
                machinery.machinery_type_name = machineryType?.machinery_type_name;

                return webResponse.OK();
            };

            return base.Add(saveDataModel);
        }

        private dv_machinery_type? GetDvMachineryType(long machineryTypeId)
        {
            var machineryTypeList = _cacheService.Get<List<dv_machinery_type>>(SystemConst.DV_MACHINERY_TYPE);

            if (machineryTypeList != null && machineryTypeList.Count > 0)
            {
                return machineryTypeList.First(x => x.machinery_type_id == machineryTypeId);
            }
            else
            {
                return _repositoryMachineryType.FindFirst(x => x.machinery_type_id == machineryTypeId);
            }
        }
    }
}
