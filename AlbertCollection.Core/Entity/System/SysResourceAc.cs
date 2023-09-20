#region copyright
//------------------------------------------------------------------------------
//  此代码版权声明为全文件覆盖，如有原作者特别声明，会在下方手动补充
//  此代码版权（除特别声明外的代码）归作者本人AlbertZhao所有
//  源代码使用协议遵循本仓库的开源协议及附加协议
//  Gitee源代码仓库：https://gitee.com/AlbertZhao/AlbertCollection



//------------------------------------------------------------------------------
#endregion

using SqlSugar.DbConvert;

namespace AlbertCollection.Core
{
    /// <summary>
    /// 资源
    ///</summary>
    [SugarTable("sys_resource_ac", TableDescription = "资源")]
    [Tenant(SqlsugarConst.DB_Default)]
    public class SysResourceAc : BaseEntity, ITree<SysResourceAc>
    {

        /// <summary>
        /// 标题
        ///</summary>
        [SugarColumn(ColumnName = "Title", ColumnDescription = "标题", Length = 200)]
        public virtual string Title { get; set; }

        /// <summary>
        /// 图标
        ///</summary>
        [SugarColumn(ColumnName = "Icon", ColumnDescription = "图标", Length = 200, IsNullable = true)]
        public virtual string Icon { get; set; }


        /// <summary>
        /// 别名
        ///</summary>
        [SugarColumn(ColumnName = "Name", ColumnDescription = "别名", Length = 200, IsNullable = true)]
        public string Name { get; set; }

        /// <summary>
        /// 路径
        ///</summary>
        [SugarColumn(ColumnName = "Component", ColumnDescription = "组件", Length = 200, IsNullable = true)]
        public virtual string Component { get; set; }

        /// <summary>
        /// 分类
        ///</summary>
        [SugarColumn(ColumnDataType = "varchar(50)", ColumnName = "Category", ColumnDescription = "分类", SqlParameterDbType = typeof(EnumToStringConvert))]
        public MenuCategoryEnum Category { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<SysResourceAc> Children { get; set; }

        /// <summary>
        /// 编码
        ///</summary>
        [SugarColumn(ColumnName = "Code", ColumnDescription = "编码", Length = 200, IsNullable = true)]
        public virtual string Code { get; set; }



        /// <summary>
        /// 父id
        ///</summary>
        [SugarColumn(ColumnName = "ParentId", ColumnDescription = "父id", IsNullable = true)]
        public virtual long ParentId { get; set; }

        /// <summary>
        /// 排序码
        ///</summary>
        [SugarColumn(ColumnName = "SortCode", ColumnDescription = "排序码", IsNullable = true)]
        public int SortCode { get; set; }

        /// <summary>
        /// 跳转类型
        ///</summary>
        [SugarColumn(ColumnDataType = "varchar(50)", ColumnName = "TargetType", ColumnDescription = "跳转类型", SqlParameterDbType = typeof(EnumToStringConvert), IsNullable = true)]
        public virtual TargetTypeEnum TargetType { get; set; }

    }
}