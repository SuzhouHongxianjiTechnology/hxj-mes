using System.ComponentModel.DataAnnotations.Schema;

namespace AlbertCollection.Application.Services.Driver.Dto
{
    public class tbl_record_data_240
    {
        /// <summary>
       ///序号
       /// </summary>
       [Key]
       [Display(Name ="序号")]
       [Column(TypeName="int")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public int ID { get; set; }

       /// <summary>
       ///GUID
       /// </summary>
       [Display(Name ="GUID")]
       [MaxLength(50)]
       [Column(TypeName="nvarchar(50)")]
       [Editable(true)]
       public string GUID { get; set; }

       /// <summary>
       ///开始时间
       /// </summary>
       [Display(Name ="开始时间")]
       [Column(TypeName="datetime")]
       [Editable(true)]
       public DateTime? StartTime { get; set; }

       /// <summary>
       ///站号
       /// </summary>
       [Display(Name ="站号")]
       [MaxLength(50)]
       [Column(TypeName="nvarchar(50)")]
       [Editable(true)]
       public string Station { get; set; }

       /// <summary>
       ///操作员
       /// </summary>
       [Display(Name ="操作员")]
       [MaxLength(50)]
       [Column(TypeName="nvarchar(50)")]
       [Editable(true)]
       public string Operator { get; set; }

       /// <summary>
       ///批次号
       /// </summary>
       [Display(Name ="批次号")]
       [MaxLength(50)]
       [Column(TypeName="nvarchar(50)")]
       [Editable(true)]
       public string BatchNO { get; set; }

       /// <summary>
       ///产品条码
       /// </summary>
       [Display(Name ="产品条码")]
       [MaxLength(50)]
       [Column(TypeName="nvarchar(50)")]
       [Editable(true)]
       public string Barcode { get; set; }

       /// <summary>
       ///产品型号
       /// </summary>
       [Display(Name ="产品型号")]
       [MaxLength(50)]
       [Column(TypeName="nvarchar(50)")]
       [Editable(true)]
       [SugarColumn(ColumnName = "Product Model")]
        public string ProductModel { get; set; }

       /// <summary>
       ///生产类型
       /// </summary>
       [Display(Name ="生产类型")]
       [MaxLength(50)]
       [Column(TypeName="nvarchar(50)")]
       [Editable(true)]
       [SugarColumn(ColumnName = "Product Type")]
        public string ProductType { get; set; }

       /// <summary>
       ///NG原因
       /// </summary>
       [Display(Name ="NG原因")]
       [MaxLength(255)]
       [Column(TypeName="nvarchar(255)")]
       [Editable(true)]
       [SugarColumn(ColumnName = "NG Classification")]
        public string NGClassification { get; set; }

       /// <summary>
       ///是否返工
       /// </summary>
       [Display(Name ="是否返工")]
       [MaxLength(20)]
       [Column(TypeName="nvarchar(20)")]
       [Editable(true)]
       public string IsRework { get; set; }

       /// <summary>
       ///重复测试
       /// </summary>
       [Display(Name ="重复测试")]
       [MaxLength(20)]
       [Column(TypeName="nvarchar(20)")]
       [Editable(true)]
       public string IsRetest { get; set; }

       /// <summary>
       ///小球进口压力均值1
       /// </summary>
       [Display(Name ="小球进口压力均值1")]
       [MaxLength(20)]
       [Column(TypeName="nvarchar(20)")]
       [Editable(true)]
       public string 小球进口压力均值1 { get; set; }

       /// <summary>
       ///小球开启扭矩峰值1
       /// </summary>
       [Display(Name ="小球开启扭矩峰值1")]
       [MaxLength(20)]
       [Column(TypeName="nvarchar(20)")]
       [Editable(true)]
       public string 小球开启扭矩峰值1 { get; set; }

       /// <summary>
       ///小球进口压力均值2
       /// </summary>
       [Display(Name ="小球进口压力均值2")]
       [MaxLength(20)]
       [Column(TypeName="nvarchar(20)")]
       [Editable(true)]
       public string 小球进口压力均值2 { get; set; }

       /// <summary>
       ///小球开启扭矩峰值2
       /// </summary>
       [Display(Name ="小球开启扭矩峰值2")]
       [MaxLength(20)]
       [Column(TypeName="nvarchar(20)")]
       [Editable(true)]
       public string 小球开启扭矩峰值2 { get; set; }

       /// <summary>
       ///结果
       /// </summary>
       [Display(Name ="结果")]
       [MaxLength(20)]
       [Column(TypeName="nvarchar(20)")]
       [Editable(true)]
       public string TotalResult { get; set; }
    }
}