/*
 *所有关于tm_tool类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*tm_toolService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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
using System.Net;
using VOL.BasicConfig.Services;
using VOL.Core.Const;
using VOL.BasicConfig.IServices;

namespace VOL.DeviceManager.Services
{
    public partial class tm_toolService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Itm_toolRepository _repository;//访问数据库
        private readonly Ibs_coderuleService _coderuleService;  // 编码规则服务
        private readonly WebResponseContent webResponse = new();

        [ActivatorUtilitiesConstructor]
        public tm_toolService(
            Itm_toolRepository dbRepository,
            IHttpContextAccessor httpContextAccessor,
            Ibs_coderuleService coderuleService
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            _coderuleService = coderuleService;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }


        public override WebResponseContent Add(SaveModel saveDataModel)
        {
            AddOnExecuting = (tool, o) =>
            {
                if (string.IsNullOrEmpty(tool.tool_code))
                {
                    var lastCodeRule = repository
                        .FindAsIQueryable(x => true)
                        .OrderByDescending(d => d.tool_id)
                    .First()?
                    .tool_code;

                    tool.tool_code = _coderuleService.GetCahceCodeRule(lastCodeRule, typeof(tm_tool).Name).Result;
                }

                //如果返回false,后面代码不会再执行
                if (repository.Exists(x => x.tool_code == tool.tool_code))
                {
                    return webResponse.Error(ErrorConst.DV_CODE_EXIST);
                }

                return webResponse.OK();
            };

            return base.Add(saveDataModel);
        }
    }
}
