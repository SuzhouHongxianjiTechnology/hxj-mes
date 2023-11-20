/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹dv_machinery_typeController编写
 */
using Microsoft.AspNetCore.Mvc;
using VOL.Core.Controllers.Basic;
using VOL.Entity.AttributeManager;
using VOL.DeviceManager.IServices;
namespace VOL.DeviceManager.Controllers
{
    [Route("api/dv_machinery_type")]
    [PermissionTable(Name = "dv_machinery_type")]
    public partial class dv_machinery_typeController : ApiBaseController<Idv_machinery_typeService>
    {
        public dv_machinery_typeController(Idv_machinery_typeService service)
        : base(service)
        {
        }
    }
}

