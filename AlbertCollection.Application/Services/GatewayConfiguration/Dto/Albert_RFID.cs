using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbertCollection.Application.Services.GatewayConfiguration.Dto
{
    public class Albert_RFID
    {
        /// <summary>
        ///RFID主键
        /// </summary>
        public int RFIDPkInt { get; set; }

        /// <summary>
        ///RFID编号
        /// </summary>
        public int RFID { get; set; }
        /// <summary>
        ///RFID 是否被占用，0 未被，1 被占用
        /// </summary>
        public int RFIDIsUse { get; set; }
        
        /// <summary>
        ///产品码
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        ///壳体码
        /// </summary>
        public string ShellCode { get; set; }

        /// <summary>
        ///Op10节拍
        /// </summary>
        public string OP10Beat { get; set; }

        /// <summary>
        /// Op10结果
        /// </summary>
        public string Op10Result { get; set; }
        /// <summary>
        /// Op10结果
        /// </summary>
        public DateTime Op10Time { get; set; }
        /// <summary>
        ///Op150节拍
        /// </summary>
        public string OP150Beat { get; set; }

        /// <summary>
        /// Op150结果
        /// </summary>
        public string Op150Result { get; set; }
        /// <summary>
        /// Op150结果
        /// </summary>
        public DateTime Op150Time { get; set; }
        /// <summary>
        ///Op160节拍
        /// </summary>
        public string OP160Beat { get; set; }

        /// <summary>
        /// Op160结果
        /// </summary>
        public string Op160Result { get; set; }
        /// <summary>
        /// Op160结果
        /// </summary>
        public DateTime Op160Time { get; set; }

        /// <summary>
        ///钢球在位(Y/N)
        /// </summary>
        public string SteelBall { get; set; }

        /// <summary>
        ///堵帽在位(Y/N)
        /// </summary>
        public string PlugCap { get; set; }

        /// <summary>
        ///螺帽在位(Y/N)
        /// </summary>
        public string Nut { get; set; }

        /// <summary>
        ///弹簧在位(Y/N)
        /// </summary>
        public string Spring { get; set; }

        /// <summary>
        ///轴承在位(Y/N)
        /// </summary>
        public string Bearing { get; set; }

        /// <summary>
        ///壳体在位(Y/N)
        /// </summary>
        public string Case { get; set; }
    }
}
