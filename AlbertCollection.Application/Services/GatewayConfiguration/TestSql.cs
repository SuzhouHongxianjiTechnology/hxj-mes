using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlbertCollection.Application.Services.Driver.Dto;
using Furion.Logging.Extensions;

namespace AlbertCollection.Application.Services.GatewayConfiguration
{
    public static class TestSql
    {
        public static void Test()
        {
            "开始测试数据搬运".LogInformation();
            var op240Data = DbContext.Db
                .GetConnectionScope("Op240")
                .Queryable<tbl_record_data_240>()
                .AS("tbl_record_data")
                .ToList();

            DbContext.Db.Insertable<tbl_record_data_240>(op240Data).ExecuteCommand();

            var op250Data = DbContext.Db
                .GetConnectionScope("Op250")
                .Queryable<tbl_record_data_250>()
                .AS("tbl_record_data")
                .ToList();

            DbContext.Db.Insertable(op250Data).ExecuteCommand();

            var op290Data = DbContext.Db
                .GetConnectionScope("Op290")
                .Queryable<tbl_record_data_290>()
                .AS("tbl_record_data")
                .ToList();

            DbContext.Db.Insertable(op290Data).ExecuteCommand();

            var op300Data = DbContext.Db
                .GetConnectionScope("Op300")
                .Queryable<tbl_record_data_300>()
                .AS("tbl_record_data")
                .ToList();

            DbContext.Db.Insertable(op300Data).ExecuteCommand();

            "数据搬运结束".LogInformation();
        }
    }
}
