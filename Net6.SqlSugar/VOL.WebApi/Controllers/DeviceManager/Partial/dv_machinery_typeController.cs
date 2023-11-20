/*
 *接口编写处...
*如果接口需要做Action的权限验证，请在Action上使用属性
*如: [ApiActionPermission("dv_machinery_type",Enums.ActionPermissionOptions.Search)]
 */
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using SqlSugar;
using VOL.Core.Enums;
using VOL.Core.Extensions;
using VOL.Core.Filters;
using VOL.Core.ManageUser;
using VOL.Core.UserManager;
using VOL.DeviceManager.IRepositories;
using VOL.Entity.DomainModels;
using VOL.DeviceManager.IServices;

namespace VOL.DeviceManager.Controllers
{
    public partial class dv_machinery_typeController
    {
        private readonly Idv_machinery_typeService _service;//访问业务代码
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Idv_machinery_typeRepository _repository;

        [ActivatorUtilitiesConstructor]
        public dv_machinery_typeController(
            Idv_machinery_typeService service,
            IHttpContextAccessor httpContextAccessor,
            Idv_machinery_typeRepository repository
        )
        : base(service)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
            _repository = repository;
        }

        public override ActionResult GetPageData(PageDataOptions loadData)
        {
            if (loadData.Value.GetInt() == 1)
            {
                return GetTreeTableRootData(loadData).Result;
            }
            return base.GetPageData(loadData);
        }

        /// <summary>
        /// treetable 获取子节点数据
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("getTreeTableRootData")]
        [ApiActionPermission(ActionPermissionOptions.Search)]
        public async Task<ActionResult> GetTreeTableRootData([FromBody] PageDataOptions options)
        {
            //页面加载根节点数据条件x => x.ParentId == 0,自己根据需要设置
            var query = _repository.FindAsIQueryable(x => x.parent_type_id == null);

            var rows = await query.TakeOrderByPage(options.Page, options.Rows)
                .OrderBy(x => x.machinery_type_id).Select(s => new
                {
                    s.machinery_type_id,
                    s.machinery_type_code,
                    s.machinery_type_name,
                    s.parent_type_id,
                    s.enable_flag,
                    s.remark,
                    s.create_by,
                    s.create_time,
                    s.update_by,
                    s.update_time,
                    hasChildren = SqlFunc.Subqueryable<dv_machinery_type>().Where(x => x.parent_type_id == s.machinery_type_id).Any()
                }).ToListAsync();
            return JsonNormal(new { total = await query.CountAsync(), rows });
        }

        /// <summary>
        ///treetable 获取子节点数据
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("getTreeTableChildrenData")]
        [ApiActionPermission(ActionPermissionOptions.Search)]
        public async Task<ActionResult> GetTreeTableChildrenData(long machineryId)
        {
            //点击节点时，加载子节点数据
            var query = _repository.FindAsIQueryable(x => true);
            var rows = await query.Where(x => x.parent_type_id == machineryId)
                .Select(s => new
                {
                    s.machinery_type_id,
                    s.machinery_type_code,
                    s.machinery_type_name,
                    s.parent_type_id,
                    s.enable_flag,
                    s.remark,
                    s.create_by,
                    s.create_time,
                    s.update_by,
                    s.update_time,
                    hasChildren = SqlFunc.Subqueryable<dv_machinery_type>().Where(x => x.parent_type_id == s.machinery_type_id).Any()
                }).ToListAsync();
            return JsonNormal(new { rows });
        }
    }
}
