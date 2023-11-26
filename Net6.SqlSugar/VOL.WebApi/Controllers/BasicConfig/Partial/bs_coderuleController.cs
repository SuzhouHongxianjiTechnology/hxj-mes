/*
 *接口编写处...
*如果接口需要做Action的权限验证，请在Action上使用属性
*如: [ApiActionPermission("bs_coderule",Enums.ActionPermissionOptions.Search)]
 */
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using VOL.Entity.DomainModels;
using VOL.BasicConfig.IServices;
using VOL.Core.CacheManager;

namespace VOL.BasicConfig.Controllers
{
    public partial class bs_coderuleController
    {
        private readonly Ibs_coderuleService _service;//访问业务代码
        private readonly IHttpContextAccessor _httpContextAccessor;
        

        [ActivatorUtilitiesConstructor]
        public bs_coderuleController(
            Ibs_coderuleService service,
            IHttpContextAccessor httpContextAccessor
        )
        : base(service)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
