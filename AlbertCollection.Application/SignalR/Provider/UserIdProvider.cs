#region copyright
//------------------------------------------------------------------------------
//  此代码版权声明为全文件覆盖，如有原作者特别声明，会在下方手动补充
//  此代码版权（除特别声明外的代码）归作者本人AlbertZhao所有
//  源代码使用协议遵循本仓库的开源协议及附加协议
//  Gitee源代码仓库：https://gitee.com/AlbertZhao/AlbertCollection



//------------------------------------------------------------------------------
#endregion

using Microsoft.AspNetCore.Http.Connections.Features;

namespace AlbertCollection.Application
{
    /// <summary>
    /// 用户ID提供器
    /// </summary>
    public class UserIdProvider : IUserIdProvider
    {
        /// <inheritdoc/>
        public string GetUserId(HubConnectionContext connection)
        {
            var feature = connection.Features.Get<IHttpContextFeature>();
            var UserId = feature.HttpContext.Request.Headers[ClaimConst.UserId].FirstOrDefault()?.ToLong();

            if (UserId > 0)
            {
                return $"{UserId}{TGHub.TG_TrackingCircuitHandlerid}{YitIdHelper.NextId()}";//返回用户ID
            }

            return connection.ConnectionId;
        }
    }
}