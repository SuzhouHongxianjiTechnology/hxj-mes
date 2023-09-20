using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbertCollection.Core.Hsl
{
    public static class HslBaseHelper
    {
        /// <summary>
        /// 启动之前调用，激活字符串不可以写在配置文件中
        /// </summary>
        /// <returns></returns>
        public static bool HslAuthorization()
        {
            return HslCommunication.Authorization.SetAuthorizationCode("66634f11-68dc-45d5-9638-fccb9f6b8fed");
        }
    }
}
