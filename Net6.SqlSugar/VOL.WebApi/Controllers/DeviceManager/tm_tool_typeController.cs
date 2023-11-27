/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹tm_tool_typeController编写
 */
using Microsoft.AspNetCore.Mvc;
using VOL.Core.Controllers.Basic;
using VOL.Entity.AttributeManager;
using VOL.DeviceManager.IServices;
namespace VOL.DeviceManager.Controllers
{
    [Route("api/tm_tool_type")]
    [PermissionTable(Name = "tm_tool_type")]
    public partial class tm_tool_typeController : ApiBaseController<Itm_tool_typeService>
    {
        public tm_tool_typeController(Itm_tool_typeService service)
        : base(service)
        {
        }
    }
}

