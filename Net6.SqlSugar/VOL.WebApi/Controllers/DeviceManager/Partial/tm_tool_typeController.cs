/*
 *接口编写处...
*如果接口需要做Action的权限验证，请在Action上使用属性
*如: [ApiActionPermission("tm_tool_type",Enums.ActionPermissionOptions.Search)]
 */
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using VOL.Entity.DomainModels;
using VOL.DeviceManager.IServices;
using VOL.Core.Enums;
using VOL.Core.Filters;
using VOL.Core.Utilities;

namespace VOL.DeviceManager.Controllers
{
    public partial class tm_tool_typeController
    {
        private readonly Itm_tool_typeService _service;//访问业务代码
        private readonly IHttpContextAccessor _httpContextAccessor;

        [ActivatorUtilitiesConstructor]
        public tm_tool_typeController(
            Itm_tool_typeService service,
            IHttpContextAccessor httpContextAccessor
        )
        : base(service)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetAllTmToolTypeTree")]
        [ApiActionPermission(ActionPermissionOptions.Search)]
        public async Task<WebResponseContent> GetAllTmToolTypeTree()
        {
            return await _service.GetAllTmToolTypeTreeAsync();
        }
    }
}
