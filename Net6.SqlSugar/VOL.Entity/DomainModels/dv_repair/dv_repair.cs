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
    [Entity(TableCnName = "设备维修",TableName = "dv_repair")]
    public partial class dv_repair:BaseEntity
    {
        /// <summary>
       ///维修单ID
       /// </summary>
       [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
       [Key]
       [Display(Name ="维修单ID")]
       [Column(TypeName="bigint")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public long repair_id { get; set; }

       /// <summary>
       ///维修单编号
       /// </summary>
       [Display(Name ="维修单编号")]
       [MaxLength(64)]
       [Column(TypeName="nvarchar(64)")]
       [Editable(true)]
       public string repair_code { get; set; }

       /// <summary>
       ///维修单名称
       /// </summary>
       [Display(Name ="维修单名称")]
       [MaxLength(255)]
       [Column(TypeName="nvarchar(255)")]
       [Editable(true)]
       public string repair_name { get; set; }

       /// <summary>
       ///设备ID
       /// </summary>
       [Display(Name ="设备ID")]
       [Column(TypeName="bigint")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public long machinery_id { get; set; }

       /// <summary>
       ///设备编码
       /// </summary>
       [Display(Name ="设备编码")]
       [MaxLength(64)]
       [Column(TypeName="nvarchar(64)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string machinery_code { get; set; }

       /// <summary>
       ///设备名称
       /// </summary>
       [Display(Name ="设备名称")]
       [MaxLength(255)]
       [Column(TypeName="nvarchar(255)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string machinery_name { get; set; }

       /// <summary>
       ///设备类型ID
       /// </summary>
       [Display(Name ="设备类型ID")]
       [Column(TypeName="bigint")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public long machinery_type_id { get; set; }

       /// <summary>
       ///报修日期
       /// </summary>
       [Display(Name ="报修日期")]
       [Column(TypeName="datetime")]
       [Editable(true)]
       public DateTime? require_date { get; set; }

       /// <summary>
       ///维修完成日期
       /// </summary>
       [Display(Name ="维修完成日期")]
       [Column(TypeName="datetime")]
       [Editable(true)]
       public DateTime? finish_date { get; set; }

       /// <summary>
       ///验收日期
       /// </summary>
       [Display(Name ="验收日期")]
       [Column(TypeName="datetime")]
       [Editable(true)]
       public DateTime? confirm_date { get; set; }

       /// <summary>
       ///维修结果
       /// </summary>
       [Display(Name ="维修结果")]
       [MaxLength(64)]
       [Column(TypeName="nvarchar(64)")]
       [Editable(true)]
       public string repair_result { get; set; }

       /// <summary>
       ///维修人员
       /// </summary>
       [Display(Name ="维修人员")]
       [MaxLength(64)]
       [Column(TypeName="nvarchar(64)")]
       [Editable(true)]
       public string accepted_by { get; set; }

       /// <summary>
       ///验收人员
       /// </summary>
       [Display(Name ="验收人员")]
       [MaxLength(64)]
       [Column(TypeName="nvarchar(64)")]
       [Editable(true)]
       public string confirm_by { get; set; }

       /// <summary>
       ///单据状态
       /// </summary>
       [Display(Name ="单据状态")]
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
       public int? CreateID { get; set; }

       /// <summary>
       ///创建者
       /// </summary>
       [Display(Name ="创建者")]
       [MaxLength(64)]
       [Column(TypeName="nvarchar(64)")]
       public string Creator { get; set; }

       /// <summary>
       ///创建时间
       /// </summary>
       [Display(Name ="创建时间")]
       [Column(TypeName="datetime")]
       public DateTime? CreateDate { get; set; }

       /// <summary>
       ///更新者ID
       /// </summary>
       [Display(Name ="更新者ID")]
       [Column(TypeName="int")]
       public int? ModifyID { get; set; }

       /// <summary>
       ///更新者
       /// </summary>
       [Display(Name ="更新者")]
       [MaxLength(64)]
       [Column(TypeName="nvarchar(64)")]
       public string Modifier { get; set; }

       /// <summary>
       ///更新时间
       /// </summary>
       [Display(Name ="更新时间")]
       [Column(TypeName="datetime")]
       public DateTime? ModifyDate { get; set; }

       
    }
}