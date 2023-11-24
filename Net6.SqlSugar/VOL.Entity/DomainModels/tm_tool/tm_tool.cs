/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果数据库字段发生变化，请在代码生器重新生成此Model
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using VOL.Entity.SystemModels;

namespace VOL.Entity.DomainModels
{
    [Entity(TableCnName = "易损件管理",TableName = "tm_tool")]
    public partial class tm_tool:BaseEntity
    {
        /// <summary>
       ///易损件ID
       /// </summary>
       [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
       [Key]
       [Display(Name ="易损件ID")]
       [Column(TypeName="bigint")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public long tool_id { get; set; }

       /// <summary>
       ///易损件编码
       /// </summary>
       [Display(Name ="易损件编码")]
       [MaxLength(64)]
       [Column(TypeName="nvarchar(64)")]
       [Editable(true)]
       public string tool_code { get; set; }

       /// <summary>
       ///易损件名称
       /// </summary>
       [Display(Name ="易损件名称")]
       [MaxLength(255)]
       [Column(TypeName="nvarchar(255)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string tool_name { get; set; }

       /// <summary>
       ///品牌
       /// </summary>
       [Display(Name ="品牌")]
       [MaxLength(255)]
       [Column(TypeName="nvarchar(255)")]
       [Editable(true)]
       public string brand { get; set; }

       /// <summary>
       ///型号
       /// </summary>
       [Display(Name ="型号")]
       [MaxLength(255)]
       [Column(TypeName="nvarchar(255)")]
       [Editable(true)]
       public string spec { get; set; }

       /// <summary>
       ///易损件类型
       /// </summary>
       [Display(Name ="易损件类型")]
       [MaxLength(64)]
       [Column(TypeName="nvarchar(64)")]
       [Editable(true)]
       public string tool_type { get; set; }

       /// <summary>
       ///保养维护类型
       /// </summary>
       [Display(Name ="保养维护类型")]
       [MaxLength(20)]
       [Column(TypeName="nvarchar(20)")]
       [Editable(true)]
       public string mainten_type { get; set; }

       /// <summary>
       ///总次数
       /// </summary>
       [Display(Name ="总次数")]
       [Column(TypeName="int")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public int quantity { get; set; }

       /// <summary>
       ///已用次数
       /// </summary>
       [Display(Name ="已用次数")]
       [Column(TypeName="int")]
       [Editable(true)]
       public int? quantity_avail { get; set; }

       /// <summary>
       ///剩余次数
       /// </summary>
       [Display(Name ="剩余次数")]
       [Column(TypeName="int")]
       [Editable(true)]
       public int? quantity_avail_remain { get; set; }

       /// <summary>
       ///下一次保养周期
       /// </summary>
       [Display(Name ="下一次保养周期")]
       [Column(TypeName="int")]
       [Editable(true)]
       public int? next_mainten_period { get; set; }

       /// <summary>
       ///下一次保养日期
       /// </summary>
       [Display(Name ="下一次保养日期")]
       [Column(TypeName="datetime")]
       [Editable(true)]
       public DateTime? next_mainten_date { get; set; }

       /// <summary>
       ///状态
       /// </summary>
       [Display(Name ="状态")]
       [MaxLength(64)]
       [Column(TypeName="nvarchar(64)")]
       [Editable(true)]
       public string status { get; set; }

       /// <summary>
       ///备注
       /// </summary>
       [Display(Name ="备注")]
       [MaxLength(500)]
       [Column(TypeName="nvarchar(500)")]
       [Editable(true)]
       public string remark { get; set; }

       /// <summary>
       ///预留字段1
       /// </summary>
       [Display(Name ="预留字段1")]
       [MaxLength(64)]
       [Column(TypeName="nvarchar(64)")]
       [Editable(true)]
       public string attr1 { get; set; }

       /// <summary>
       ///预留字段2
       /// </summary>
       [Display(Name ="预留字段2")]
       [MaxLength(255)]
       [Column(TypeName="nvarchar(255)")]
       [Editable(true)]
       public string attr2 { get; set; }

       /// <summary>
       ///预留字段3
       /// </summary>
       [Display(Name ="预留字段3")]
       [Column(TypeName="int")]
       [Editable(true)]
       public int? attr3 { get; set; }

       /// <summary>
       ///预留字段4
       /// </summary>
       [Display(Name ="预留字段4")]
       [Column(TypeName="int")]
       [Editable(true)]
       public int? attr4 { get; set; }

       /// <summary>
       ///创建者ID
       /// </summary>
       [Display(Name ="创建者ID")]
       [Column(TypeName="int")]
       [Editable(true)]
       public int? CreateID { get; set; }

       /// <summary>
       ///创建者
       /// </summary>
       [Display(Name ="创建者")]
       [MaxLength(64)]
       [Column(TypeName="nvarchar(64)")]
       [Editable(true)]
       public string Creator { get; set; }

       /// <summary>
       ///创建时间
       /// </summary>
       [Display(Name ="创建时间")]
       [Column(TypeName="datetime")]
       [Editable(true)]
       public DateTime? CreateDate { get; set; }

       /// <summary>
       ///创建者ID
       /// </summary>
       [Display(Name ="创建者ID")]
       [Column(TypeName="int")]
       [Editable(true)]
       public int? ModifyID { get; set; }

       /// <summary>
       ///更新者
       /// </summary>
       [Display(Name ="更新者")]
       [MaxLength(64)]
       [Column(TypeName="nvarchar(64)")]
       [Editable(true)]
       public string Modifier { get; set; }

       /// <summary>
       ///更新时间
       /// </summary>
       [Display(Name ="更新时间")]
       [Column(TypeName="datetime")]
       [Editable(true)]
       public DateTime? ModifyDate { get; set; }

       
    }
}