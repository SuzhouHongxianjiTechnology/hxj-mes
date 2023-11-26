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
    [Entity(TableCnName = "编码规则",TableName = "bs_coderule")]
    public partial class bs_coderule:BaseEntity
    {
        /// <summary>
       ///编号规则主键
       /// </summary>
       [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
       [Key]
       [Display(Name ="编号规则主键")]
       [Column(TypeName="int")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public int coderule_id { get; set; }

       /// <summary>
       ///编号生成规则
       /// </summary>
       [Display(Name ="编号生成规则")]
       [MaxLength(50)]
       [Column(TypeName="nvarchar(50)")]
       [Editable(true)]
       public string generative { get; set; }

       /// <summary>
       ///目标表单
       /// </summary>
       [Display(Name ="目标表单")]
       [MaxLength(50)]
       [Column(TypeName="nvarchar(50)")]
       [Editable(true)]
       public string fromcode { get; set; }

       /// <summary>
       ///编号前缀
       /// </summary>
       [Display(Name ="编号前缀")]
       [MaxLength(50)]
       [Column(TypeName="nvarchar(50)")]
       [Editable(true)]
       public string prefix { get; set; }

       /// <summary>
       ///时间规则
       /// </summary>
       [Display(Name ="时间规则")]
       [MaxLength(50)]
       [Column(TypeName="nvarchar(50)")]
       [Editable(true)]
       public string time { get; set; }

       /// <summary>
       ///流水号位
       /// </summary>
       [Display(Name ="流水号位")]
       [Column(TypeName="int")]
       [Editable(true)]
       public int? serialnumber { get; set; }

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
       [MaxLength(50)]
       [Column(TypeName="nvarchar(50)")]
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
       [MaxLength(50)]
       [Column(TypeName="nvarchar(50)")]
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