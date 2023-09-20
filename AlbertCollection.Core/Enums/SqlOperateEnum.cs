using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbertCollection.Core.Enums
{
    public enum SqlOperateEnum
    {
        /// <summary>
        /// 读取不操作数据库
        /// </summary>
        [Description("读取不操作数据库")]
        None = 0,
        /// <summary>
        /// 插入数据
        /// </summary>
        [Description("插入数据")]
        Insert = 1,
        /// <summary>
        /// 更新数据
        /// </summary>
        [Description("更新数据")]
        Update = 2
    }
}
