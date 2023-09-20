using System.ComponentModel.DataAnnotations.Schema;

namespace AlbertCollection.Application.Services.GatewayConfiguration.Dto
{
    public class Albert_DataFirst
    {
        /// <summary>
        ///数据主键
        /// </summary>
        [Key]
        [Display(Name = "数据主键")]
        [Column(TypeName = "int")]
        [Editable(true)]
        [Required(AllowEmptyStrings = false)]
        public int DataPkInt { get; set; }

        /// <summary>
        ///工单号
        /// </summary>
        [Display(Name = "工单号")]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        [Editable(true)]
        [Required(AllowEmptyStrings = false)]
        public string WorkorderCode { get; set; }

        /// <summary>
        ///产品码
        /// </summary>
        [Display(Name = "产品码")]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        [Editable(true)]
        [Required(AllowEmptyStrings = false)]
        public string ProductCode { get; set; }

        /// <summary>
        ///RFID
        /// </summary>
        [Display(Name = "RFID")]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        [Editable(true)]
        public string RFID { get; set; }

        /// <summary>
        ///最终结果
        /// </summary>
        [Display(Name = "最终结果")]
        [MaxLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        [Editable(true)]
        public string OpFinalResult { get; set; }

        /// <summary>
        ///最终站
        /// </summary>
        [Display(Name = "最终站")]
        [MaxLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        [Editable(true)]
        public string OpFinalStation { get; set; }

        /// <summary>
        ///最终时间
        /// </summary>
        [Display(Name = "最终时间")]
        [Column(TypeName = "datetime")]
        [Editable(true)]
        public DateTime? OpFinalDate { get; set; }

        /// <summary>
        ///是否返工
        /// </summary>
        [Display(Name = "是否返工")]
        [MaxLength(1)]
        [Column(TypeName = "nvarchar(1)")]
        public string IsRework { get; set; }

        /// <summary>
        ///物料码
        /// </summary>
        [Display(Name = "物料码")]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        [Editable(true)]
        public string MaterialCode { get; set; }

        /// <summary>
        ///Op10加工结果
        /// </summary>
        [Display(Name = "Op10加工结果")]
        [MaxLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        [Editable(true)]
        public string Op10Result { get; set; }

        /// <summary>
        ///Op10加工时间
        /// </summary>
        [Display(Name = "Op10加工时间")]
        [Column(TypeName = "datetime")]
        [Editable(true)]
        public DateTime? Op10Time { get; set; }

        /// <summary>
        ///Op10节拍
        /// </summary>
        [Display(Name = "Op10节拍")]
        [DisplayFormat(DataFormatString = "20,6")]
        [Column(TypeName = "decimal")]
        public decimal? Op10Beat { get; set; }

        /// <summary>
        ///Op20加工结果
        /// </summary>
        [Display(Name = "Op20加工结果")]
        [MaxLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        [Editable(true)]
        public string Op20Result { get; set; }

        /// <summary>
        ///Op20加工时间
        /// </summary>
        [Display(Name = "Op20加工时间")]
        [Column(TypeName = "datetime")]
        [Editable(true)]
        public DateTime? Op20Time { get; set; }

        /// <summary>
        ///Op20节拍
        /// </summary>
        [Display(Name = "Op20节拍")]
        [DisplayFormat(DataFormatString = "20,6")]
        [Column(TypeName = "decimal")]
        public decimal? Op20Beat { get; set; }

        /// <summary>
        ///Op30加工结果
        /// </summary>
        [Display(Name = "Op30加工结果")]
        [MaxLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        [Editable(true)]
        public string Op30Result { get; set; }

        /// <summary>
        ///Op30加工时间
        /// </summary>
        [Display(Name = "Op30加工时间")]
        [Column(TypeName = "datetime")]
        [Editable(true)]
        public DateTime? Op30Time { get; set; }

        /// <summary>
        ///Op30节拍
        /// </summary>
        [Display(Name = "Op30节拍")]
        [DisplayFormat(DataFormatString = "20,6")]
        [Column(TypeName = "decimal")]
        public decimal? Op30Beat { get; set; }

        /// <summary>
        ///Op40加工结果
        /// </summary>
        [Display(Name = "Op40加工结果")]
        [MaxLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        [Editable(true)]
        public string Op40Result { get; set; }

        /// <summary>
        ///Op40加工时间
        /// </summary>
        [Display(Name = "Op40加工时间")]
        [Column(TypeName = "datetime")]
        [Editable(true)]
        public DateTime? Op40Time { get; set; }

        /// <summary>
        ///Op40节拍
        /// </summary>
        [Display(Name = "Op40节拍")]
        [DisplayFormat(DataFormatString = "20,6")]
        [Column(TypeName = "decimal")]
        public decimal? Op40Beat { get; set; }

        /// <summary>
        ///Op50加工结果
        /// </summary>
        [Display(Name = "Op50加工结果")]
        [MaxLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        [Editable(true)]
        public string Op50Result { get; set; }

        /// <summary>
        ///Op50加工时间
        /// </summary>
        [Display(Name = "Op50加工时间")]
        [Column(TypeName = "datetime")]
        [Editable(true)]
        public DateTime? Op50Time { get; set; }

        /// <summary>
        ///Op50节拍
        /// </summary>
        [Display(Name = "Op50节拍")]
        [DisplayFormat(DataFormatString = "20,6")]
        [Column(TypeName = "decimal")]
        public decimal? Op50Beat { get; set; }

        /// <summary>
        ///Op60加工结果
        /// </summary>
        [Display(Name = "Op60加工结果")]
        [MaxLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        [Editable(true)]
        public string Op60Result { get; set; }

        /// <summary>
        ///Op60加工时间
        /// </summary>
        [Display(Name = "Op60加工时间")]
        [Column(TypeName = "datetime")]
        [Editable(true)]
        public DateTime? Op60Time { get; set; }

        /// <summary>
        ///Op60压力数据
        /// </summary>
        [Display(Name = "Op60压力数据")]
        [MaxLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        [Editable(true)]
        public string Op60Pressure { get; set; }

        /// <summary>
        ///Op60压力文件
        /// </summary>
        [Display(Name = "Op60压力文件")]
        [MaxLength(90)]
        [Column(TypeName = "nvarchar(90)")]
        [Editable(true)]
        public string Op60PressureFile { get; set; }

        /// <summary>
        ///Op60压力下限
        /// </summary>
        [Display(Name = "Op60压力下限")]
        [MaxLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        [Editable(true)]
        public string Op60PressureLower { get; set; }

        /// <summary>
        ///Op60压力上限
        /// </summary>
        [Display(Name = "Op60压力上限")]
        [MaxLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        [Editable(true)]
        public string Op60PressureUpper { get; set; }

        /// <summary>
        ///Op60位移数据
        /// </summary>
        [Display(Name = "Op60位移数据")]
        [MaxLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        [Editable(true)]
        public string Op60Displacement { get; set; }

        /// <summary>
        ///Op60位移文件
        /// </summary>
        [Display(Name = "Op60位移文件")]
        [MaxLength(90)]
        [Column(TypeName = "nvarchar(90)")]
        [Editable(true)]
        public string Op60DisplacementFile { get; set; }

        /// <summary>
        ///Op60位移下限
        /// </summary>
        [Display(Name = "Op60位移下限")]
        [MaxLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        [Editable(true)]
        public string Op60DisplacementLower { get; set; }

        /// <summary>
        ///Op60位移上限
        /// </summary>
        [Display(Name = "Op60位移上限")]
        [MaxLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        [Editable(true)]
        public string Op60DisplacementUpper { get; set; }

        /// <summary>
        ///Op60节拍
        /// </summary>
        [Display(Name = "Op60节拍")]
        [DisplayFormat(DataFormatString = "20,6")]
        [Column(TypeName = "decimal")]
        public decimal? Op60Beat { get; set; }

        /// <summary>
        ///Op70加工结果
        /// </summary>
        [Display(Name = "Op70加工结果")]
        [MaxLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        [Editable(true)]
        public string Op70Result { get; set; }

        /// <summary>
        ///Op70加工时间
        /// </summary>
        [Display(Name = "Op70加工时间")]
        [Column(TypeName = "datetime")]
        [Editable(true)]
        public DateTime? Op70Time { get; set; }

        /// <summary>
        ///Op70Beat
        /// </summary>
        [Display(Name = "Op70Beat")]
        [DisplayFormat(DataFormatString = "20,6")]
        [Column(TypeName = "decimal")]
        public decimal? Op70Beat { get; set; }

        /// <summary>
        ///Op80加工结果
        /// </summary>
        [Display(Name = "Op80加工结果")]
        [MaxLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        [Editable(true)]
        public string Op80Result { get; set; }

        /// <summary>
        ///Op80加工时间
        /// </summary>
        [Display(Name = "Op80加工时间")]
        [Column(TypeName = "datetime")]
        [Editable(true)]
        public DateTime? Op80Time { get; set; }

        /// <summary>
        ///Op80节拍
        /// </summary>
        [Display(Name = "Op80节拍")]
        [DisplayFormat(DataFormatString = "20,6")]
        [Column(TypeName = "decimal")]
        public decimal? Op80Beat { get; set; }

        /// <summary>
        ///Op90加工结果
        /// </summary>
        [Display(Name = "Op90加工结果")]
        [MaxLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        [Editable(true)]
        public string Op90Result { get; set; }

        /// <summary>
        ///Op90加工时间
        /// </summary>
        [Display(Name = "Op90加工时间")]
        [Column(TypeName = "datetime")]
        [Editable(true)]
        public DateTime? Op90Time { get; set; }

        /// <summary>
        ///Op90相机图片文件
        /// </summary>
        [Display(Name = "Op90相机图片文件")]
        [MaxLength(90)]
        [Column(TypeName = "nvarchar(90)")]
        [Editable(true)]
        public string Op90IV3File { get; set; }

        /// <summary>
        ///Op90Beat
        /// </summary>
        [Display(Name = "Op90Beat")]
        [DisplayFormat(DataFormatString = "20,6")]
        [Column(TypeName = "decimal")]
        public decimal? Op90Beat { get; set; }

        /// <summary>
        ///Op100加工结果
        /// </summary>
        [Display(Name = "Op100加工结果")]
        [MaxLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        [Editable(true)]
        public string Op100Result { get; set; }

        /// <summary>
        ///Op100加工时间
        /// </summary>
        [Display(Name = "Op100加工时间")]
        [Column(TypeName = "datetime")]
        [Editable(true)]
        public DateTime? Op100Time { get; set; }

        /// <summary>
        ///Op100节拍
        /// </summary>
        [Display(Name = "Op100节拍")]
        [DisplayFormat(DataFormatString = "20,6")]
        [Column(TypeName = "decimal")]
        public decimal? Op100Beat { get; set; }

        /// <summary>
        ///Op110加工结果
        /// </summary>
        [Display(Name = "Op110加工结果")]
        [MaxLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        [Editable(true)]
        public string Op110Result { get; set; }

        /// <summary>
        ///Op110加工时间
        /// </summary>
        [Display(Name = "Op110加工时间")]
        [Column(TypeName = "datetime")]
        [Editable(true)]
        public DateTime? Op110Time { get; set; }

        /// <summary>
        ///Op110相机图片文件
        /// </summary>
        [Display(Name = "Op110相机图片文件")]
        [MaxLength(90)]
        [Column(TypeName = "nvarchar(90)")]
        [Editable(true)]
        public string Op110IV3File { get; set; }

        /// <summary>
        ///Op110节拍
        /// </summary>
        [Display(Name = "Op110节拍")]
        [DisplayFormat(DataFormatString = "20,6")]
        [Column(TypeName = "decimal")]
        public decimal? Op110Beat { get; set; }

        /// <summary>
        ///Op120加工结果
        /// </summary>
        [Display(Name = "Op120加工结果")]
        [MaxLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        [Editable(true)]
        public string Op120Result { get; set; }

        /// <summary>
        ///Op120加工时间
        /// </summary>
        [Display(Name = "Op120加工时间")]
        [Column(TypeName = "datetime")]
        [Editable(true)]
        public DateTime? Op120Time { get; set; }

        /// <summary>
        ///Op120扭矩数据
        /// </summary>
        [Display(Name = "Op120扭矩数据")]
        [MaxLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        [Editable(true)]
        public string Op120Torque { get; set; }

        /// <summary>
        ///Op120扭矩文件
        /// </summary>
        [Display(Name = "Op120扭矩文件")]
        [MaxLength(90)]
        [Column(TypeName = "nvarchar(90)")]
        [Editable(true)]
        public string Op120TorqueFile { get; set; }

        /// <summary>
        ///Op120扭矩下限
        /// </summary>
        [Display(Name = "Op120扭矩下限")]
        [MaxLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        [Editable(true)]
        public string Op120TorqueLower { get; set; }

        /// <summary>
        ///Op120扭矩上限
        /// </summary>
        [Display(Name = "Op120扭矩上限")]
        [MaxLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        [Editable(true)]
        public string Op120TorqueUpper { get; set; }

        /// <summary>
        ///Op120节拍
        /// </summary>
        [Display(Name = "Op120节拍")]
        [DisplayFormat(DataFormatString = "20,6")]
        [Column(TypeName = "decimal")]
        public decimal? Op120Beat { get; set; }

        /// <summary>
        ///Op130加工结果
        /// </summary>
        [Display(Name = "Op130加工结果")]
        [MaxLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        [Editable(true)]
        public string Op130Result { get; set; }

        /// <summary>
        ///Op130加工时间
        /// </summary>
        [Display(Name = "Op130加工时间")]
        [Column(TypeName = "datetime")]
        [Editable(true)]
        public DateTime? Op130Time { get; set; }

        /// <summary>
        ///Op130节拍
        /// </summary>
        [Display(Name = "Op130节拍")]
        [DisplayFormat(DataFormatString = "20,6")]
        [Column(TypeName = "decimal")]
        public decimal? Op130Beat { get; set; }

        /// <summary>
        ///创建ID
        /// </summary>
        [Display(Name = "创建ID")]
        [Column(TypeName = "int")]
        [Editable(true)]
        public int? CreateID { get; set; }

        /// <summary>
        ///创建人
        /// </summary>
        [Display(Name = "创建人")]
        [MaxLength(30)]
        [Column(TypeName = "nvarchar(30)")]
        [Editable(true)]
        public string Creator { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [Column(TypeName = "datetime")]
        [Editable(true)]
        [Required(AllowEmptyStrings = false)]
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///修改ID
        /// </summary>
        [Display(Name = "修改ID")]
        [Column(TypeName = "int")]
        [Editable(true)]
        public int? ModifyID { get; set; }

        /// <summary>
        ///修改人
        /// </summary>
        [Display(Name = "修改人")]
        [MaxLength(30)]
        [Column(TypeName = "nvarchar(30)")]
        [Editable(true)]
        public string Modifier { get; set; }

        /// <summary>
        ///修改时间
        /// </summary>
        [Display(Name = "修改时间")]
        [Column(TypeName = "datetime")]
        [Editable(true)]
        public DateTime? ModifyDate { get; set; }
    }
}
