using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlbertCollection.Application.Cache;
using AlbertCollection.Application.Services.Driver.Dto;
using AlbertCollection.Application.Services.GatewayConfiguration;
using Furion.DynamicApiController;
using Furion.Logging.Extensions;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace AlbertCollection.Application.Controllers
{
    [ApiDescriptionSettings(CateGoryConst.ThingsGatewayCore, Order = 200)]
    [Route("sqloperate")]
    [AllowAnonymous]
    public class RemoveDataFromOtherCompany : IDynamicApiController
    {
        private readonly RemoveDataFromOtherCompanyService _removeService;

        public RemoveDataFromOtherCompany(RemoveDataFromOtherCompanyService removeService)
        {
            _removeService = removeService;
        }

        /// <summary>
        /// 开始搬移数据，会有一个版本号
        /// </summary>
        /// <returns></returns>
        public async Task RemoveData(string otherSqlName)
        {
            await _removeService.RemoveData(otherSqlName);
        }
    }
}
