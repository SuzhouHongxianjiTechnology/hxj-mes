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
    [Entity(TableCnName = "设备点检保养",TableName = "dv_dss_record")]
    public partial class dv_dss_record:BaseEntity
    {
        /// <summary>
       ///记录ID
       /// </summary>
       [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
       [Key]
       [Display(Name ="记录ID")]
       [Column(TypeName="bigint")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public long record_id { get; set; }

       /// <summary>
       ///点检保养编号
       /// </summary>
       [Display(Name ="点检保养编号")]
       [MaxLength(64)]
       [Column(TypeName="nvarchar(64)")]
       [Editable(true)]
       public string record_code { get; set; }

       /// <summary>
       ///点检保养类型
       /// </summary>
       [Display(Name ="点检保养类型")]
       [MaxLength(64)]
       [Column(TypeName="nvarchar(64)")]
       [Editable(true)]
       public string record_type { get; set; }

       /// <summary>
       ///点检保养结果
       /// </summary>
       [Display(Name ="点检保养结果")]
       [MaxLength(10)]
       [Column(TypeName="nvarchar(10)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string record_result { get; set; }

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
       ///周期
       /// </summary>
       [Display(Name ="周期")]
       [MaxLength(10)]
       [Column(TypeName="nvarchar(10)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string cycle_type { get; set; }

       /// <summary>
       ///频率
       /// </summary>
       [Display(Name ="频率")]
       [Column(TypeName="int")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public int cycle_count { get; set; }

       /// <summary>
       ///开始时间
       /// </summary>
       [Display(Name ="开始时间")]
       [Column(TypeName="datetime")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public DateTime start_date { get; set; }

       /// <summary>
       ///结束时间
       /// </summary>
       [Display(Name ="结束时间")]
       [Column(TypeName="datetime")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public DateTime end_date { get; set; }

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
       ///更新者ID
       /// </summary>
       [Display(Name ="更新者ID")]
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