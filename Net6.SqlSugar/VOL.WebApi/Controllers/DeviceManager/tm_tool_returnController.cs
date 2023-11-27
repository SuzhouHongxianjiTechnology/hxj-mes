/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹tm_tool_returnController编写
 */
using Microsoft.AspNetCore.Mvc;
using VOL.Core.Controllers.Basic;
using VOL.Entity.AttributeManager;
using VOL.DeviceManager.IServices;
namespace VOL.DeviceManager.Controllers
{
    [Route("api/tm_tool_return")]
    [PermissionTable(Name = "tm_tool_return")]
    public partial class tm_tool_returnController : ApiBaseController<Itm_tool_returnService>
    {
        public tm_tool_returnController(Itm_tool_returnService service)
        : base(service)
        {
        }
    }
}

