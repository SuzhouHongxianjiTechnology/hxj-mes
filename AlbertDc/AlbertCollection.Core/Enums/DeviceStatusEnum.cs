using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbertCollection.Core
{
    public enum DeviceStatusEnum
    {
        /// <summary>
        /// 在线
        /// </summary>
        [Description("在线")]
        OnLine = 0,
        /// <summary>
        /// 离线
        /// </summary>
        [Description("离线")]
        OffLine = 1,
        /// <summary>
        /// 断开
        /// </summary>
        [Description("断开")]
        DisConnect = 2,
    }
}
