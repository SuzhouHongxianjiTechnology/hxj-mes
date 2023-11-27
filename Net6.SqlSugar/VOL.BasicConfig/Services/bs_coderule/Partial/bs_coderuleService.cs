/*
 *所有关于bs_coderule类的业务代码应在此处编写
 *可使用repository.调用常用方法，获取EF/Dapper等信息
 *如果需要事务请使用repository.DbContextBeginTransaction
 *也可使用DBServerProvider.手动获取数据库相关信息
 *用户信息、权限、角色等使用UserContext.Current操作
 *bs_coderuleService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
 */
using VOL.Entity.DomainModels;
using VOL.Core.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using VOL.BasicConfig.IRepositories;
using VOL.Core.CacheManager;
using VOL.Core.Extensions;

namespace VOL.BasicConfig.Services
{
    public partial class bs_coderuleService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Ibs_coderuleRepository _repository;//访问数据库
        private readonly ICacheService _cacheService; // 缓存
        private WebResponseContent webResponse = new();

        [ActivatorUtilitiesConstructor]
        public bs_coderuleService(
            Ibs_coderuleRepository dbRepository,
            IHttpContextAccessor httpContextAccessor,
            ICacheService cacheService
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            _cacheService = cacheService;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }

        public override WebResponseContent Add(SaveModel saveDataModel)
        {
            //此处saveModel是从前台提交的原生数据，可对数据进修改过滤
            AddOnExecuting = (bs_coderule coderule, object list) =>
            {
                //如果返回false,后面代码不会再执行、
                if (repository.Exists(x => x.fromcode == coderule.fromcode))
                {
                    return webResponse.Error("目标表单已存在");
                }

                _cacheService.AddObject(coderule.fromcode, coderule);
                return webResponse.OK();
            };
            return base.Add(saveDataModel);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="saveDataModel"></param>
        /// <returns></returns>
        public override WebResponseContent Update(SaveModel saveDataModel)
        {
            //编辑方法保存数据库前处理
            UpdateOnExecuting = (bs_coderule coderule, object addList, object updateList, List<object> delKeys) =>
            {
                if (repository.Exists(x => x.fromcode == coderule.fromcode && x.coderule_id != coderule.coderule_id))
                {
                    return webResponse.Error("目标表单已存在");
                }

                _cacheService.AddObject(coderule.fromcode, coderule);
                return webResponse.OK();
            };
            return base.Update(saveDataModel);
        }

        /// <summary>
        /// 给所有人调用，缓存编码规则
        /// </summary>
        /// <param name="lastCodeRule"></param>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public async Task<string> GetCahceCodeRule(string lastCodeRule, string cacheKey)
        {
            //查询当天最新的订单号

            // var lastCodeRule = repository
            //     .FindAsIQueryable(x => true)
            //     .OrderByDescending(d => d.CreateDate)
            //     .Take(1)
            //     .Single()?
            //     .machinery_code;

            // typeof(dv_machinery).Name
            var codeRule = _cacheService.Get<bs_coderule>(cacheKey);

            // 如果为空则从数据库中获取
            if (codeRule == null)
            {
                codeRule = await _repository.FindAsIQueryable(x =>
                        x.fromcode == cacheKey)
                    .FirstAsync();

                if (codeRule == null)
                {
                    return DateTime.Now.ToString("yyyyMMddHHmmssffff");
                }
                else
                {
                    _cacheService.AddObject(codeRule.fromcode, codeRule);
                }
            }

            string rule = codeRule.prefix + DateTime.Now.ToString(codeRule.time);

            if (string.IsNullOrEmpty(lastCodeRule))
            {
                rule += "1".PadLeft(codeRule.serialnumber, '0');
            }
            else
            {
                rule += (lastCodeRule.Substring(lastCodeRule.Length - codeRule.serialnumber).GetInt() + 1).ToString("0".PadLeft(codeRule.serialnumber, '0'));
            }

            return rule;
        }
    }
}
