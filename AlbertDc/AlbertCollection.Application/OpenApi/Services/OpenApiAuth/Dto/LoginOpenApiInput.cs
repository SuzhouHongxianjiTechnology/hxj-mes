#region copyright
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
    /// 登录输入参数
    /// </summary>
    public class LoginOpenApiInput
    {
        /// <summary>
        /// 账号
        ///</summary>
        [Required(ErrorMessage = "账号不能为空"), MinLength(3, ErrorMessage = "账号不能少于4个字符")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        ///</summary>
        [Required(ErrorMessage = "密码不能为空"), MinLength(3, ErrorMessage = "密码不能少于3个字符")]
        public string Password { get; set; }
    }
}