﻿#region copyright
//------------------------------------------------------------------------------
//  此代码版权声明为全文件覆盖，如有原作者特别声明，会在下方手动补充
//  此代码版权（除特别声明外的代码）归作者本人AlbertZhao所有
//  源代码使用协议遵循本仓库的开源协议及附加协议
//  Gitee源代码仓库：https://gitee.com/AlbertZhao/AlbertCollection



//------------------------------------------------------------------------------
#endregion

namespace AlbertCollection.Application
{
    /// <summary>
    /// 登录事件参数
    /// </summary>
    public class LoginEvent
    {
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime DateTime = DateTime.UtcNow;

        /// <summary>
        /// 登录设备
        /// </summary>
        public AuthDeviceTypeEnum Device { get; set; }

        /// <summary>
        /// 过期时间(分)
        /// </summary>
        public int Expire { get; set; }

        /// <summary>
        /// Ip地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public SysUserAc SysUser { get; set; }

        /// <summary>
        /// 验证Id
        /// </summary>
        public long VerificatId { get; set; }
    }
}