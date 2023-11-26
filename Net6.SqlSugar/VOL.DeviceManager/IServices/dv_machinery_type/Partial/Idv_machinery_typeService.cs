/*
*所有关于dv_machinery_type类的业务代码接口应在此处编写
*/
using VOL.Core.BaseProvider;
using VOL.Entity.DomainModels;
using VOL.Core.Utilities;
using System.Linq.Expressions;
namespace VOL.DeviceManager.IServices
{
    public partial interface Idv_machinery_typeService
    {
        Task<WebResponseContent> GetAllMachineryTypeTreeAsync();
    }
 }
