#region copyright
//------------------------------------------------------------------------------
//  此代码版权声明为全文件覆盖，如有原作者特别声明，会在下方手动补充
//  此代码版权（除特别声明外的代码）归作者本人AlbertZhao所有
//  源代码使用协议遵循本仓库的开源协议及附加协议
//  Gitee源代码仓库：https://gitee.com/AlbertZhao/AlbertCollection



//------------------------------------------------------------------------------
#endregion

namespace AlbertCollection.Core
{
    /// <summary>
    /// 系统关系表
    ///</summary>
    [SugarTable("sys_relation_ac", TableDescription = "系统关系表")]
    [Tenant(SqlsugarConst.DB_Default)]
    public class SysRelationAc : PrimaryKeyEntity
    {
        /// <summary>
        /// 分类
        ///</summary>
        [SugarColumn(ColumnName = "Category", ColumnDescription = "分类", Length = 200, IsNullable = false)]
        public string Category { get; set; }

        /// <summary>
        /// 对象ID
        ///</summary>
        [SugarColumn(ColumnName = "ObjectId", ColumnDescription = "对象ID", IsNullable = false)]
        public long ObjectId { get; set; }

        /// <summary>
        /// 目标ID
        ///</summary>
        [SugarColumn(ColumnName = "TargetId", ColumnDescription = "目标ID", IsNullable = true)]
        public string TargetId { get; set; }
    }
}