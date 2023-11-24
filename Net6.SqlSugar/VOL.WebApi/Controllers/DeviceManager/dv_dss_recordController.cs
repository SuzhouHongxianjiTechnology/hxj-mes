/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹dv_dss_recordController编写
 */
using Microsoft.AspNetCore.Mvc;
using VOL.Core.Controllers.Basic;
using VOL.Entity.AttributeManager;
using VOL.DeviceManager.IServices;
namespace VOL.DeviceManager.Controllers
{
    [Route("api/dv_dss_record")]
    [PermissionTable(Name = "dv_dss_record")]
    public partial class dv_dss_recordController : ApiBaseController<Idv_dss_recordService>
    {
        public dv_dss_recordController(Idv_dss_recordService service)
        : base(service)
        {
        }
    }
}

