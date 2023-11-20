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
    [Entity(TableCnName = "设备台账",TableName = "dv_machinery")]
    public partial class dv_machinery:BaseEntity
    {
        /// <summary>
       ///设备类型ID
       /// </summary>
       [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
       [Key]
       [Display(Name ="设备类型ID")]
       [Column(TypeName="bigint")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public long machinery_id { get; set; }

       /// <summary>
       ///设备类型编码
       /// </summary>
       [Display(Name ="设备类型编码")]
       [MaxLength(64)]
       [Column(TypeName="nvarchar(64)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string machinery_code { get; set; }

       /// <summary>
       ///设备类型名称
       /// </summary>
       [Display(Name ="设备类型名称")]
       [MaxLength(255)]
       [Column(TypeName="nvarchar(255)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string machinery_name { get; set; }

       /// <summary>
       ///品牌
       /// </summary>
       [Display(Name ="品牌")]
       [MaxLength(255)]
       [Column(TypeName="nvarchar(255)")]
       [Editable(true)]
       public string machinery_brand { get; set; }

       /// <summary>
       ///规格型号
       /// </summary>
       [Display(Name ="规格型号")]
       [MaxLength(255)]
       [Column(TypeName="nvarchar(255)")]
       [Editable(true)]
       public string machinery_spec { get; set; }

       /// <summary>
       ///设备类型ID
       /// </summary>
       [Display(Name ="设备类型ID")]
       [Column(TypeName="bigint")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public long machinery_type_id { get; set; }

       /// <summary>
       ///设备类型编码
       /// </summary>
       [Display(Name ="设备类型编码")]
       [MaxLength(64)]
       [Column(TypeName="nvarchar(64)")]
       [Editable(true)]
       public string machinery_type_code { get; set; }

       /// <summary>
       ///设备类型名称
       /// </summary>
       [Display(Name ="设备类型名称")]
       [MaxLength(255)]
       [Column(TypeName="nvarchar(255)")]
       [Editable(true)]
       public string machinery_type_name { get; set; }

       /// <summary>
       ///设备通讯接口
       /// </summary>
       [Display(Name ="设备通讯接口")]
       [MaxLength(64)]
       [Column(TypeName="nvarchar(64)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string machinery_ip { get; set; }

       /// <summary>
       ///设备配置信息
       /// </summary>
       [Display(Name ="设备配置信息")]
       [Column(TypeName="nvarchar()")]
       [Editable(true)]
       public string machinery_config { get; set; }

       /// <summary>
       ///所属车间ID
       /// </summary>
       [Display(Name ="所属车间ID")]
       [Column(TypeName="bigint")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public long workshop_id { get; set; }

       /// <summary>
       ///所属车间编码
       /// </summary>
       [Display(Name ="所属车间编码")]
       [MaxLength(64)]
       [Column(TypeName="nvarchar(64)")]
       [Editable(true)]
       public string workshop_code { get; set; }

       /// <summary>
       ///所属车间名称
       /// </summary>
       [Display(Name ="所属车间名称")]
       [MaxLength(255)]
       [Column(TypeName="nvarchar(255)")]
       [Editable(true)]
       public string workshop_name { get; set; }

       /// <summary>
       ///设备状态
       /// </summary>
       [Display(Name ="设备状态")]
       [MaxLength(64)]
       [Column(TypeName="nvarchar(64)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
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
       ///创建者
       /// </summary>
       [Display(Name ="创建者")]
       [MaxLength(64)]
       [Column(TypeName="nvarchar(64)")]
       [Editable(true)]
       public string create_by { get; set; }

       /// <summary>
       ///创建时间
       /// </summary>
       [Display(Name ="创建时间")]
       [Column(TypeName="datetime")]
       [Editable(true)]
       public DateTime? create_time { get; set; }

       /// <summary>
       ///更新者
       /// </summary>
       [Display(Name ="更新者")]
       [MaxLength(64)]
       [Column(TypeName="nvarchar(64)")]
       [Editable(true)]
       public string update_by { get; set; }

       /// <summary>
       ///更新时间
       /// </summary>
       [Display(Name ="更新时间")]
       [Column(TypeName="datetime")]
       [Editable(true)]
       public DateTime? update_time { get; set; }

       
    }
}