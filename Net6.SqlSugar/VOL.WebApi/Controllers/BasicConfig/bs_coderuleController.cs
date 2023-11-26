/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹bs_coderuleController编写
 */
using Microsoft.AspNetCore.Mvc;
using VOL.Core.Controllers.Basic;
using VOL.Entity.AttributeManager;
using VOL.BasicConfig.IServices;
namespace VOL.BasicConfig.Controllers
{
    [Route("api/bs_coderule")]
    [PermissionTable(Name = "bs_coderule")]
    public partial class bs_coderuleController : ApiBaseController<Ibs_coderuleService>
    {
        public bs_coderuleController(Ibs_coderuleService service)
        : base(service)
        {
        }
    }
}

