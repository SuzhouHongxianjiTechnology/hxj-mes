/*
 *所有关于tm_tool_return类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*tm_tool_returnService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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

namespace VOL.DeviceManager.Services
{
    public partial class tm_tool_returnService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Itm_tool_returnRepository _repository;//访问数据库

        [ActivatorUtilitiesConstructor]
        public tm_tool_returnService(
            Itm_tool_returnRepository dbRepository,
            IHttpContextAccessor httpContextAccessor
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }

        public override PageGridData<tm_tool_return> GetPageData(PageDataOptions options)
        {
            // 如果值为 -1，则默认清空所有查询条件
            QueryRelativeList = list =>
            {
                if (list.Count > 0
                    && list[0].Name == SystemConst.DV_TM_TOOL_TYPE_CODE
                    && list[0].Value == "-1")
                {
                    list.Clear();
                }
            };

            return base.GetPageData(options);
        }
    }
}
