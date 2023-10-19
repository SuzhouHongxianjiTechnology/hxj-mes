using System.ComponentModel.DataAnnotations.Schema;

namespace AlbertCollection.Application.Services.GatewayConfiguration.Dto
{
    public class Albert_DataSecond
    {
        /// <summary>
       ///数据主键
       /// </summary>
       [Key]
       [Display(Name ="数据主键")]
       [Column(TypeName="int")]
       [Required(AllowEmptyStrings=false)]
       public int DataPkInt { get; set; }

       /// <summary>
       ///工单号
       /// </summary>
       [Display(Name ="工单号")]
       [MaxLength(50)]
       [Column(TypeName="nvarchar(50)")]
       [Required(AllowEmptyStrings=false)]
       public string WorkorderCode { get; set; }

       /// <summary>
       ///是否返工
       /// </summary>
       [Display(Name ="是否返工")]
       [MaxLength(1)]
       [Column(TypeName="nvarchar(1)")]
       public string IsRework { get; set; }

       /// <summary>
       ///壳体码
       /// </summary>
       [Display(Name ="壳体码")]
       [MaxLength(20)]
       [Column(TypeName="nvarchar(20)")]
       public string ShellCode { get; set; }

       /// <summary>
       ///轴承码(1线）
       /// </summary>
       [Display(Name ="轴承码(1线）")]
       [MaxLength(20)]
       [Column(TypeName="nvarchar(20)")]
       [Required(AllowEmptyStrings=false)]
       public string ProductCode { get; set; }

       /// <summary>
       ///执行器码(270)
       /// </summary>
       [Display(Name ="执行器码(270)")]
       [MaxLength(50)]
       [Column(TypeName="nvarchar(50)")]
       public string RunCode { get; set; }

       /// <summary>
       ///RFID
       /// </summary>
       [Display(Name ="RFID")]
       [MaxLength(50)]
       [Column(TypeName="nvarchar(50)")]
       public string RFID { get; set; }

       /// <summary>
       ///最终结果
       /// </summary>
       [Display(Name ="最终结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string OpFinalResult { get; set; }

       /// <summary>
       ///最终站
       /// </summary>
       [Display(Name ="最终站")]
       [MaxLength(10)]
       [Column(TypeName="nvarchar(10)")]
       public string OpFinalStation { get; set; }

       /// <summary>
       ///最终时间
       /// </summary>
       [Display(Name ="最终时间")]
       [Column(TypeName="datetime")]
       public DateTime? OpFinalDate { get; set; }

       /// <summary>
       ///Op150加工结果
       /// </summary>
       [Display(Name ="Op150加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op150Result { get; set; }

       /// <summary>
       ///Op150加工时间
       /// </summary>
       [Display(Name ="Op150加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op150Time { get; set; }

       /// <summary>
       ///Op150节拍
       /// </summary>
       [Display(Name ="Op150节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op150Beat { get; set; }

       /// <summary>
       ///Op160加工结果
       /// </summary>
       [Display(Name ="Op160加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op160Result { get; set; }

       /// <summary>
       ///Op160加工时间
       /// </summary>
       [Display(Name ="Op160加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op160Time { get; set; }

       /// <summary>
       ///Op160节拍
       /// </summary>
       [Display(Name ="Op160节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op160Beat { get; set; }

       /// <summary>
       ///Op170加工结果
       /// </summary>
       [Display(Name ="Op170加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op170Result { get; set; }

       /// <summary>
       ///Op170加工时间
       /// </summary>
       [Display(Name ="Op170加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op170Time { get; set; }

       /// <summary>
       ///Op170节拍
       /// </summary>
       [Display(Name ="Op170节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op170Beat { get; set; }

       /// <summary>
       ///Op180_1加工结果
       /// </summary>
       [Display(Name ="Op180_1加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op180_1Result { get; set; }

       /// <summary>
       ///Op180_1加工时间
       /// </summary>
       [Display(Name ="Op180_1加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op180_1Time { get; set; }

       /// <summary>
       ///Op180_1节拍
       /// </summary>
       [Display(Name ="Op180_1节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op180_1Beat { get; set; }

       /// <summary>
       ///Op180_2加工结果
       /// </summary>
       [Display(Name ="Op180_2加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op180_2Result { get; set; }

       /// <summary>
       ///Op180_2加工时间
       /// </summary>
       [Display(Name ="Op180_2加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op180_2Time { get; set; }

       /// <summary>
       ///Op180_2节拍
       /// </summary>
       [Display(Name ="Op180_2节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op180_2Beat { get; set; }

       /// <summary>
       ///Op180_3加工结果
       /// </summary>
       [Display(Name ="Op180_3加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op180_3Result { get; set; }

       /// <summary>
       ///Op180_3加工时间
       /// </summary>
       [Display(Name ="Op180_3加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op180_3Time { get; set; }

       /// <summary>
       ///Op180_3节拍
       /// </summary>
       [Display(Name ="Op180_3节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op180_3Beat { get; set; }

       /// <summary>
       ///Op180_3扭矩数据
       /// </summary>
       [Display(Name ="Op180_3扭矩数据")]
       [MaxLength(10)]
       [Column(TypeName="nvarchar(10)")]
       public string Op180_3Torque { get; set; }

       /// <summary>
       ///Op180_3角度数据
       /// </summary>
       [Display(Name ="Op180_3角度数据")]
       [MaxLength(10)]
       [Column(TypeName="nvarchar(10)")]
       public string Op180_3Angle { get; set; }

       /// <summary>
       ///Op180_3扭矩结果
       /// </summary>
       [Display(Name ="Op180_3扭矩结果")]
       [MaxLength(2)]
       [Column(TypeName="nvarchar(2)")]
       public string Op180_3TorqueResult { get; set; }

       /// <summary>
       ///Op180_3扭矩数据2
       /// </summary>
       [Display(Name ="Op180_3扭矩数据2")]
       [MaxLength(10)]
       [Column(TypeName="nvarchar(10)")]
       public string Op180_3Torque2 { get; set; }

       /// <summary>
       ///Op180_3角度数据2
       /// </summary>
       [Display(Name ="Op180_3角度数据2")]
       [MaxLength(10)]
       [Column(TypeName="nvarchar(10)")]
       public string Op180_3Angle2 { get; set; }

       /// <summary>
       ///Op180_3扭矩结果2
       /// </summary>
       [Display(Name ="Op180_3扭矩结果2")]
       [MaxLength(2)]
       [Column(TypeName="nvarchar(2)")]
       public string Op180_3TorqueResult2 { get; set; }

       /// <summary>
       ///Op190加工结果
       /// </summary>
       [Display(Name ="Op190加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op190Result { get; set; }

       /// <summary>
       ///Op190加工时间
       /// </summary>
       [Display(Name ="Op190加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op190Time { get; set; }

       /// <summary>
       ///Op190节拍
       /// </summary>
       [Display(Name ="Op190节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op190Beat { get; set; }

       /// <summary>
       ///Op200_1加工结果
       /// </summary>
       [Display(Name ="Op200_1加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op200_1Result { get; set; }

       /// <summary>
       ///Op200_1加工时间
       /// </summary>
       [Display(Name ="Op200_1加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op200_1Time { get; set; }

       /// <summary>
       ///Op200_1节拍
       /// </summary>
       [Display(Name ="Op200_1节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op200_1Beat { get; set; }

       /// <summary>
       ///Op200_2加工结果
       /// </summary>
       [Display(Name ="Op200_2加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op200_2Result { get; set; }

       /// <summary>
       ///Op200_2加工时间
       /// </summary>
       [Display(Name ="Op200_2加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op200_2Time { get; set; }

       /// <summary>
       ///Op200_2节拍
       /// </summary>
       [Display(Name ="Op200_2节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op200_2Beat { get; set; }

       /// <summary>
       ///Op210加工结果
       /// </summary>
       [Display(Name ="Op210加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op210Result { get; set; }

       /// <summary>
       ///Op210加工时间
       /// </summary>
       [Display(Name ="Op210加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op210Time { get; set; }

       /// <summary>
       ///Op210节拍
       /// </summary>
       [Display(Name ="Op210节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op210Beat { get; set; }

       /// <summary>
       ///Op210扭矩数据
       /// </summary>
       [Display(Name ="Op210扭矩数据")]
       [MaxLength(10)]
       [Column(TypeName="nvarchar(10)")]
       public string Op210Torque { get; set; }

       /// <summary>
       ///Op210角度数据
       /// </summary>
       [Display(Name ="Op210角度数据")]
       [MaxLength(10)]
       [Column(TypeName="nvarchar(10)")]
       public string Op210Angle { get; set; }

       /// <summary>
       ///Op210扭矩结果
       /// </summary>
       [Display(Name ="Op210扭矩结果")]
       [MaxLength(2)]
       [Column(TypeName="nvarchar(2)")]
       public string Op210TorqueResult { get; set; }

       /// <summary>
       ///Op220加工结果
       /// </summary>
       [Display(Name ="Op220加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op220Result { get; set; }

       /// <summary>
       ///Op220加工时间
       /// </summary>
       [Display(Name ="Op220加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op220Time { get; set; }

       /// <summary>
       ///Op220节拍
       /// </summary>
       [Display(Name ="Op220节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op220Beat { get; set; }

       /// <summary>
       ///Op220位移结果
       /// </summary>
       [Display(Name ="Op220位移结果")]
       [MaxLength(2)]
       [Column(TypeName="nvarchar(2)")]
       public string Op220DisplaceResult { get; set; }

       /// <summary>
       ///Op220位移值
       /// </summary>
       [Display(Name ="Op220位移值")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op220Displace { get; set; }

       /// <summary>
       ///Op230_1加工结果
       /// </summary>
       [Display(Name ="Op230_1加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op230_1Result { get; set; }

       /// <summary>
       ///Op230_1加工时间
       /// </summary>
       [Display(Name ="Op230_1加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op230_1Time { get; set; }

       /// <summary>
       ///Op230_1节拍
       /// </summary>
       [Display(Name ="Op230_1节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op230_1Beat { get; set; }

       /// <summary>
       ///Op230_2加工结果
       /// </summary>
       [Display(Name ="Op230_2加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op230_2Result { get; set; }

       /// <summary>
       ///Op230_2加工时间
       /// </summary>
       [Display(Name ="Op230_2加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op230_2Time { get; set; }

       /// <summary>
       ///Op230_2节拍
       /// </summary>
       [Display(Name ="Op230_2节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op230_2Beat { get; set; }

       /// <summary>
       ///Op230_3加工结果
       /// </summary>
       [Display(Name ="Op230_3加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op230_3Result { get; set; }

       /// <summary>
       ///Op230_3加工时间
       /// </summary>
       [Display(Name ="Op230_3加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op230_3Time { get; set; }

       /// <summary>
       ///Op230_3节拍
       /// </summary>
       [Display(Name ="Op230_3节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op230_3Beat { get; set; }

       /// <summary>
       ///Op230_4加工结果
       /// </summary>
       [Display(Name ="Op230_4加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op230_4Result { get; set; }

       /// <summary>
       ///Op230_4加工时间
       /// </summary>
       [Display(Name ="Op230_4加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op230_4Time { get; set; }

       /// <summary>
       ///Op230_4节拍
       /// </summary>
       [Display(Name ="Op230_4节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op230_4Beat { get; set; }

       /// <summary>
       ///Op230氦检数据
       /// </summary>
       [Display(Name ="Op230氦检数据")]
       [Column(TypeName="int")]
       public int? Op230Extra { get; set; }

       /// <summary>
       ///Op240_1加工结果
       /// </summary>
       [Display(Name ="Op240_1加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op240_1Result { get; set; }

       /// <summary>
       ///Op240_1加工时间
       /// </summary>
       [Display(Name ="Op240_1加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op240_1Time { get; set; }

       /// <summary>
       ///Op240_1节拍
       /// </summary>
       [Display(Name ="Op240_1节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op240_1Beat { get; set; }

       /// <summary>
       ///Op240_2加工结果
       /// </summary>
       [Display(Name ="Op240_2加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op240_2Result { get; set; }

       /// <summary>
       ///Op240_2加工时间
       /// </summary>
       [Display(Name ="Op240_2加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op240_2Time { get; set; }

       /// <summary>
       ///Op240_2节拍
       /// </summary>
       [Display(Name ="Op240_2节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op240_2Beat { get; set; }

       /// <summary>
       ///Op240高品扭矩
       /// </summary>
       [Display(Name ="Op240高品扭矩")]
       [Column(TypeName="int")]
       public int? Op240Extra { get; set; }

       /// <summary>
       ///Op250_1加工结果
       /// </summary>
       [Display(Name ="Op250_1加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op250_1Result { get; set; }

       /// <summary>
       ///Op250_1加工时间
       /// </summary>
       [Display(Name ="Op250_1加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op250_1Time { get; set; }

       /// <summary>
       ///Op250_1节拍
       /// </summary>
       [Display(Name ="Op250_1节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op250_1Beat { get; set; }

       /// <summary>
       ///Op250_2加工结果
       /// </summary>
       [Display(Name ="Op250_2加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op250_2Result { get; set; }

       /// <summary>
       ///Op250_2加工时间
       /// </summary>
       [Display(Name ="Op250_2加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op250_2Time { get; set; }

       /// <summary>
       ///Op250_2节拍
       /// </summary>
       [Display(Name ="Op250_2节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op250_2Beat { get; set; }

       /// <summary>
       ///Op250高品内漏率
       /// </summary>
       [Display(Name ="Op250高品内漏率")]
       [Column(TypeName="int")]
       public int? Op250Extra { get; set; }

       /// <summary>
       ///Op260加工结果
       /// </summary>
       [Display(Name ="Op260加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op260Result { get; set; }

       /// <summary>
       ///Op260加工时间
       /// </summary>
       [Display(Name ="Op260加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op260Time { get; set; }

       /// <summary>
       ///Op260节拍
       /// </summary>
       [Display(Name ="Op260节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op260Beat { get; set; }

       /// <summary>
       ///Op270加工结果
       /// </summary>
       [Display(Name ="Op270加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op270Result { get; set; }

       /// <summary>
       ///Op270加工时间
       /// </summary>
       [Display(Name ="Op270加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op270Time { get; set; }

       /// <summary>
       ///Op270节拍
       /// </summary>
       [Display(Name ="Op270节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op270Beat { get; set; }

       /// <summary>
       ///Op280加工结果
       /// </summary>
       [Display(Name ="Op280加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op280Result { get; set; }

       /// <summary>
       ///Op280加工时间
       /// </summary>
       [Display(Name ="Op280加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op280Time { get; set; }

       /// <summary>
       ///Op280节拍
       /// </summary>
       [Display(Name ="Op280节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op280Beat { get; set; }

       /// <summary>
       ///Op280扭矩数据
       /// </summary>
       [Display(Name ="Op280扭矩数据")]
       [MaxLength(10)]
       [Column(TypeName="nvarchar(10)")]
       public string Op280Torque { get; set; }

       /// <summary>
       ///Op280角度数据
       /// </summary>
       [Display(Name ="Op280角度数据")]
       [MaxLength(10)]
       [Column(TypeName="nvarchar(10)")]
       public string Op280Angle { get; set; }

       /// <summary>
       ///Op280扭矩结果
       /// </summary>
       [Display(Name ="Op280扭矩结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op280TorqueResult { get; set; }

       /// <summary>
       ///Op280扭矩数据2
       /// </summary>
       [Display(Name ="Op280扭矩数据2")]
       [MaxLength(10)]
       [Column(TypeName="nvarchar(10)")]
       public string Op280Torque2 { get; set; }

       /// <summary>
       ///Op280角度数据2
       /// </summary>
       [Display(Name ="Op280角度数据2")]
       [MaxLength(10)]
       [Column(TypeName="nvarchar(10)")]
       public string Op280Angle2 { get; set; }

       /// <summary>
       ///Op280扭矩结果2
       /// </summary>
       [Display(Name ="Op280扭矩结果2")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op280TorqueResult2 { get; set; }

       /// <summary>
       ///Op280扭矩数据3
       /// </summary>
       [Display(Name ="Op280扭矩数据3")]
       [MaxLength(10)]
       [Column(TypeName="nvarchar(10)")]
       public string Op280Torque3 { get; set; }

       /// <summary>
       ///Op280角度数据3
       /// </summary>
       [Display(Name ="Op280角度数据3")]
       [MaxLength(10)]
       [Column(TypeName="nvarchar(10)")]
       public string Op280Angle3 { get; set; }

       /// <summary>
       ///Op280扭矩结果3
       /// </summary>
       [Display(Name ="Op280扭矩结果3")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op280TorqueResult3 { get; set; }

       /// <summary>
       ///Op290_1加工结果
       /// </summary>
       [Display(Name ="Op290_1加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op290_1Result { get; set; }

       /// <summary>
       ///Op290_1加工时间
       /// </summary>
       [Display(Name ="Op290_1加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op290_1Time { get; set; }

       /// <summary>
       ///Op290_1节拍
       /// </summary>
       [Display(Name ="Op290_1节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op290_1Beat { get; set; }

       /// <summary>
       ///Op290_2加工结果
       /// </summary>
       [Display(Name ="Op290_2加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op290_2Result { get; set; }

       /// <summary>
       ///Op290_2加工时间
       /// </summary>
       [Display(Name ="Op290_2加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op290_2Time { get; set; }

       /// <summary>
       ///Op290_2节拍
       /// </summary>
       [Display(Name ="Op290_2节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op290_2Beat { get; set; }

       /// <summary>
       ///Op290高品间隙测量
       /// </summary>
       [Display(Name ="Op290高品间隙测量")]
       [Column(TypeName="int")]
       public int? Op290Extra { get; set; }

       /// <summary>
       ///Op300_1加工结果
       /// </summary>
       [Display(Name ="Op300_1加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op300_1Result { get; set; }

       /// <summary>
       ///Op300_1加工时间
       /// </summary>
       [Display(Name ="Op300_1加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op300_1Time { get; set; }

       /// <summary>
       ///Op300_1节拍
       /// </summary>
       [Display(Name ="Op300_1节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op300_1Beat { get; set; }

       /// <summary>
       ///Op300_2加工结果
       /// </summary>
       [Display(Name ="Op300_2加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op300_2Result { get; set; }

       /// <summary>
       ///Op300_2加工时间
       /// </summary>
       [Display(Name ="Op300_2加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op300_2Time { get; set; }

       /// <summary>
       ///Op300_2节拍
       /// </summary>
       [Display(Name ="Op300_2节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op300_2Beat { get; set; }

       /// <summary>
       ///Op300高品EOL测试
       /// </summary>
       [Display(Name ="Op300高品EOL测试")]
       [Column(TypeName="int")]
       public int? Op300Extra { get; set; }

       /// <summary>
       ///Op310_1加工结果
       /// </summary>
       [Display(Name ="Op310_1加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op310_1Result { get; set; }

       /// <summary>
       ///Op310_1加工时间
       /// </summary>
       [Display(Name ="Op310_1加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op310_1Time { get; set; }

       /// <summary>
       ///Op310_1节拍
       /// </summary>
       [Display(Name ="Op310_1节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op310_1Beat { get; set; }

       /// <summary>
       ///Op310_2加工结果
       /// </summary>
       [Display(Name ="Op310_2加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op310_2Result { get; set; }

       /// <summary>
       ///Op310_2加工时间
       /// </summary>
       [Display(Name ="Op310_2加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op310_2Time { get; set; }

       /// <summary>
       ///Op310_2节拍
       /// </summary>
       [Display(Name ="Op310_2节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op310_2Beat { get; set; }

       /// <summary>
       ///Op310EOL测试升级
       /// </summary>
       [Display(Name ="Op310EOL测试升级")]
       [Column(TypeName="int")]
       public int? Op310Extra { get; set; }

       /// <summary>
       ///Op320加工结果
       /// </summary>
       [Display(Name ="Op320加工结果")]
       [MaxLength(10)]
       [Column(TypeName="nvarchar(10)")]
       public string Op320Result { get; set; }

       /// <summary>
       ///Op320加工时间
       /// </summary>
       [Display(Name ="Op320加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op320Time { get; set; }

       /// <summary>
       ///Op320节拍
       /// </summary>
       [Display(Name ="Op320节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op320Beat { get; set; }

       /// <summary>
       ///Op320扭矩数据
       /// </summary>
       [Display(Name ="Op320扭矩数据")]
       [MaxLength(10)]
       [Column(TypeName="nvarchar(10)")]
       public string Op320Torque { get; set; }

       /// <summary>
       ///Op320角度数据
       /// </summary>
       [Display(Name ="Op320角度数据")]
       [MaxLength(10)]
       [Column(TypeName="nvarchar(10)")]
       public string Op320Angle { get; set; }

       /// <summary>
       ///Op320扭矩结果
       /// </summary>
       [Display(Name ="Op320扭矩结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op320TorqueResult { get; set; }

       /// <summary>
       ///Op330加工结果
       /// </summary>
       [Display(Name ="Op330加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op330Result { get; set; }

       /// <summary>
       ///Op330加工时间
       /// </summary>
       [Display(Name ="Op330加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op330Time { get; set; }

       /// <summary>
       ///Op330节拍
       /// </summary>
       [Display(Name ="Op330节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op330Beat { get; set; }

       /// <summary>
       ///Op340加工结果
       /// </summary>
       [Display(Name ="Op340加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op340Result { get; set; }

       /// <summary>
       ///Op340加工时间
       /// </summary>
       [Display(Name ="Op340加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op340Time { get; set; }

       /// <summary>
       ///Op340节拍
       /// </summary>
       [Display(Name ="Op340节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op340Beat { get; set; }

       /// <summary>
       ///Op350加工结果
       /// </summary>
       [Display(Name ="Op350加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op350Result { get; set; }

       /// <summary>
       ///Op350加工时间
       /// </summary>
       [Display(Name ="Op350加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op350Time { get; set; }

       /// <summary>
       ///Op350节拍
       /// </summary>
       [Display(Name ="Op350节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op350Beat { get; set; }

       /// <summary>
       ///Op360加工结果
       /// </summary>
       [Display(Name ="Op360加工结果")]
       [MaxLength(5)]
       [Column(TypeName="nvarchar(5)")]
       public string Op360Result { get; set; }

       /// <summary>
       ///Op360加工时间
       /// </summary>
       [Display(Name ="Op360加工时间")]
       [Column(TypeName="datetime")]
       public DateTime? Op360Time { get; set; }

       /// <summary>
       ///Op360节拍
       /// </summary>
       [Display(Name ="Op360节拍")]
       [DisplayFormat(DataFormatString="20,6")]
       [Column(TypeName="decimal")]
       public decimal? Op360Beat { get; set; }

       /// <summary>
       ///修改ID
       /// </summary>
       [Display(Name ="修改ID")]
       [Column(TypeName="int")]
       public int? ModifyID { get; set; }

       /// <summary>
       ///修改人
       /// </summary>
       [Display(Name ="修改人")]
       [MaxLength(30)]
       [Column(TypeName="nvarchar(30)")]
       public string Modifier { get; set; }

       /// <summary>
       ///修改时间
       /// </summary>
       [Display(Name ="修改时间")]
       [Column(TypeName="datetime")]
       public DateTime? ModifyDate { get; set; }
    }
}