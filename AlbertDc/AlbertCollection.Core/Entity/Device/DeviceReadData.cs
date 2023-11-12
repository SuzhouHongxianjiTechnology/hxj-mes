using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbertCollection.Core.Entity.Device
{
    /// <summary>
    /// 读取数据配置表
    /// </summary>
    [SugarTable("device_readdata", TableDescription = "读取数据配置表")]
    [Tenant(SqlsugarConst.DB_Default)]
    public class DeviceReadData: BaseEntity
    {
        /// <summary>
        /// Seq ID
        /// </summary>
        [SugarColumn(ColumnName = "SeqId", ColumnDescription = "Seq ID")]
        public long SeqId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(ColumnName = "Sort", ColumnDescription = "排序")]
        public int Sort { get; set; }
        /// <summary>
        /// 列名
        /// </summary>
        [SugarColumn(ColumnName = "SqlColumnName", ColumnDescription = "列名", Length = 20, IsNullable = true)]
        public string SqlColumnName { get; set; }
        /// <summary>
        /// 地址块类型和地址
        /// </summary>
        [SugarColumn(ColumnName = "TypeAndDb", ColumnDescription = "地址块类型和地址", Length = 20, IsNullable = true)]
        public string TypeAndDb { get; set; }
    }
}
