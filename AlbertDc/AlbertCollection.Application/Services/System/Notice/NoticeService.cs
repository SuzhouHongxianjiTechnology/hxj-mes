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
    /// <inheritdoc cref="INoticeService"/>
    /// </summary>
    public class NoticeService : INoticeService
    {
        /// <inheritdoc/>
        public virtual async Task LoginOut(string userId, List<VerificatInfo> verificatInfos, string message)
        {
            //客户端ID列表
            var clientIds = new List<string>();
            //遍历token列表获取客户端ID列表
            verificatInfos.ForEach(it =>
            {
                clientIds.AddRange(it.ClientIds);
            });
            //获取signalr实例
            var signalr = App.GetService<IHubContext<TGHub, ITGHub>>();
            //发送其他客户端登录消息
            await signalr.Clients.Users(clientIds).LoginOut(message);
        }
    }
}