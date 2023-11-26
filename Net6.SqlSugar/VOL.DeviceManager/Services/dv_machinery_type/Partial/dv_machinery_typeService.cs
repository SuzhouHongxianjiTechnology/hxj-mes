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
using VOL.Core.CacheManager;
using VOL.DeviceManager.IRepositories;
using VOL.Core.Const;

namespace VOL.DeviceManager.Services
{
    public partial class dv_machinery_typeService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Idv_machinery_typeRepository _repository;//访问数据库
        private readonly ICacheService _cacheService;
        private WebResponseContent webResponse = new();

        [ActivatorUtilitiesConstructor]
        public dv_machinery_typeService(
            Idv_machinery_typeRepository dbRepository,
            IHttpContextAccessor httpContextAccessor,
            ICacheService cacheService
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            _cacheService= cacheService;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
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
