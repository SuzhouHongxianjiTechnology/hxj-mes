/*
*所有关于bs_coderule类的业务代码接口应在此处编写
*/
using VOL.Core.BaseProvider;
using VOL.Entity.DomainModels;
using VOL.Core.Utilities;
using System.Linq.Expressions;
namespace VOL.BasicConfig.IServices
{
    public partial interface Ibs_coderuleService
    {
        Task<string> GetCahceCodeRule(string lastCodeRule, string cacheKey);
    }
 }
