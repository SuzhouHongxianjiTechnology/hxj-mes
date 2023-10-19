using System.ComponentModel.DataAnnotations.Schema;

namespace AlbertCollection.Application.Services.GatewayConfiguration.Dto
{
    public class Albert_RFID
    {
        /// <summary>
       ///RFID主键
       /// </summary>
       [Key]
       [Display(Name ="RFID主键")]
       [Column(TypeName="int")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public int RFIDPkInt { get; set; }

       /// <summary>
       ///RFID编号
       /// </summary>
       [Display(Name ="RFID编号")]
       [Column(TypeName="int")]
       public int? RFID { get; set; }

       /// <summary>
       ///RFID被占用
       /// </summary>
       [Display(Name ="RFID被占用")]
       [Column(TypeName="int")]
       public int? RFIDIsUse { get; set; }

       /// <summary>
       ///产品码(轴承码）
       /// </summary>
       [Display(Name ="产品码(轴承码）")]
       [MaxLength(50)]
       [Column(TypeName="nvarchar(50)")]
       public string ProductCode { get; set; }

       /// <summary>
       ///壳体码
       /// </summary>
       [Display(Name ="壳体码")]
       [MaxLength(50)]
       [Column(TypeName="nvarchar(50)")]
       public string ShellCode { get; set; }

       /// <summary>
       ///Op10节拍
       /// </summary>
       [Display(Name ="Op10节拍")]
       [MaxLength(10)]
       [Column(TypeName="nvarchar(10)")]
       public string Op10Beat { get; set; }

       /// <summary>
       ///Op10结果
       /// </summary>
       [Display(Name ="Op10结果")]
       [MaxLength(10)]
       [Column(TypeName="nvarchar(10)")]
       public string Op10Result { get; set; }

       /// <summary>
       ///Op10加工时间
       /// </summary>
       [Display(Name ="Op10加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op10Time { get; set; }

       /// <summary>
       ///钢球在位(Y/N)
       /// </summary>
       [Display(Name ="钢球在位(Y/N)")]
       [MaxLength(1)]
       [Column(TypeName="nvarchar(1)")]
       public string SteelBall { get; set; }

       /// <summary>
       ///堵帽在位(Y/N)
       /// </summary>
       [Display(Name ="堵帽在位(Y/N)")]
       [MaxLength(1)]
       [Column(TypeName="nvarchar(1)")]
       public string PlugCap { get; set; }

       /// <summary>
       ///螺帽在位(Y/N)
       /// </summary>
       [Display(Name ="螺帽在位(Y/N)")]
       [MaxLength(1)]
       [Column(TypeName="nvarchar(1)")]
       public string Nut { get; set; }

       /// <summary>
       ///弹簧在位(Y/N)
       /// </summary>
       [Display(Name ="弹簧在位(Y/N)")]
       [MaxLength(1)]
       [Column(TypeName="nvarchar(1)")]
       public string Spring { get; set; }

       /// <summary>
       ///轴承在位(Y/N)
       /// </summary>
       [Display(Name ="轴承在位(Y/N)")]
       [MaxLength(1)]
       [Column(TypeName="nvarchar(1)")]
       public string Bearing { get; set; }

       /// <summary>
       ///壳体在位(Y/N)
       /// </summary>
       [Display(Name ="壳体在位(Y/N)")]
       [MaxLength(1)]
       [Column(TypeName="nvarchar(1)")]
       public string Case { get; set; }

       /// <summary>
       ///Op150节拍
       /// </summary>
       [Display(Name ="Op150节拍")]
       [MaxLength(10)]
       [Column(TypeName="nvarchar(10)")]
       public string Op150Beat { get; set; }

       /// <summary>
       ///Op150结果
       /// </summary>
       [Display(Name ="Op150结果")]
       [MaxLength(10)]
       [Column(TypeName="nvarchar(10)")]
       public string Op150Result { get; set; }

       /// <summary>
       ///Op150加工时间
       /// </summary>
       [Display(Name ="Op150加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op150Time { get; set; }

       /// <summary>
       ///Op160节拍
       /// </summary>
       [Display(Name ="Op160节拍")]
       [MaxLength(10)]
       [Column(TypeName="nvarchar(10)")]
       public string Op160Beat { get; set; }

       /// <summary>
       ///Op160结果
       /// </summary>
       [Display(Name ="Op160结果")]
       [MaxLength(10)]
       [Column(TypeName="nvarchar(10)")]
       public string Op160Result { get; set; }

       /// <summary>
       ///Op160加工时间
       /// </summary>
       [Display(Name ="Op160加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op160Time { get; set; }
    }
}