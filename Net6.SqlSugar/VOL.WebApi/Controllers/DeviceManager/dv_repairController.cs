/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹dv_repairController编写
 */
using Microsoft.AspNetCore.Mvc;
using VOL.Core.Controllers.Basic;
using VOL.Entity.AttributeManager;
using VOL.DeviceManager.IServices;
namespace VOL.DeviceManager.Controllers
{
    [Route("api/dv_repair")]
    [PermissionTable(Name = "dv_repair")]
    public partial class dv_repairController : ApiBaseController<Idv_repairService>
    {
        public dv_repairController(Idv_repairService service)
        : base(service)
        {
        }
    }
}

