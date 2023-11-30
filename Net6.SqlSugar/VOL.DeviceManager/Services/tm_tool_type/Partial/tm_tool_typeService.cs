/*
 *所有关于tm_tool_type类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*tm_tool_typeService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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
using VOL.Core.Const;

namespace VOL.DeviceManager.Services
{
    public partial class tm_tool_typeService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Itm_tool_typeRepository _repository;//访问数据库
        private readonly Ibs_coderuleService _coderuleService;  // 编码规则服务
        private readonly DeviceBaseService _deviceBaseService;
        private readonly WebResponseContent webResponse = new();

        [ActivatorUtilitiesConstructor]
        public tm_tool_typeService(
            Itm_tool_typeRepository dbRepository,
            IHttpContextAccessor httpContextAccessor,
            Ibs_coderuleService coderuleService,
            DeviceBaseService deviceBaseService
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            _coderuleService = coderuleService;
            _deviceBaseService = deviceBaseService;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }

        public override WebResponseContent Add(SaveModel saveDataModel)
        {
            AddOnExecuting = (toolType, o) =>
            {
                if (string.IsNullOrEmpty(toolType.tool_type_code))
                {
                    var lastCodeRule = repository
                        .FindAsIQueryable(x => true)
                        .OrderByDescending(d => d.tool_type_id)
                        .First()?
                        .tool_type_code;

                    toolType.tool_type_code = _coderuleService.GetCahceCodeRule(lastCodeRule, typeof(tm_tool_type).Name).Result;
                }

                //如果返回false,后面代码不会再执行
                if (repository.Exists(x => x.tool_type_code == toolType.tool_type_code))
                {
                    return webResponse.Error(ErrorConst.DV_CODE_EXIST);
                }

                return webResponse.OK();
            };

            return base.Add(saveDataModel);
        }

        /// <summary>
        /// 获取所有设备类型节点树
        /// </summary>
        /// <returns></returns>
        public async Task<WebResponseContent> GetAllTmToolTypeTreeAsync()
        {
            try
            {
                var tmToolTypeList = await _deviceBaseService.GetTmToolTypeListAsync();

                var data = tmToolTypeList
                    .Select(d => new
                    {
                        d.tool_type_id,
                        d.tool_type_code,
                        d.tool_type_name
                    }).ToList();

                webResponse.Data = data;
                return webResponse.OK();

            }
            catch (Exception e)
            {
                return webResponse.Error();
            }
        }
    }
}
