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
    /// 权限校验服务
    /// </summary>
    public interface IAuthService : ITransient
    {
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        ValidCodeOutPut GetCaptchaInfo();

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <returns></returns>
        Task<SysUserAc> GetLoginUser();

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input">登录参数</param>
        /// <returns>Token信息</returns>
        Task<LoginOutPut> Login(LoginInput input);

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        Task LoginOut();
    }
}