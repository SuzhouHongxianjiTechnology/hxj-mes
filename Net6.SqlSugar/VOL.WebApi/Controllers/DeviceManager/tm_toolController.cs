/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹tm_toolController编写
 */
using Microsoft.AspNetCore.Mvc;
using VOL.Core.Controllers.Basic;
using VOL.Entity.AttributeManager;
using VOL.DeviceManager.IServices;
namespace VOL.DeviceManager.Controllers
{
    [Route("api/tm_tool")]
    [PermissionTable(Name = "tm_tool")]
    public partial class tm_toolController : ApiBaseController<Itm_toolService>
    {
        public tm_toolController(Itm_toolService service)
        : base(service)
        {
        }
    }
}

