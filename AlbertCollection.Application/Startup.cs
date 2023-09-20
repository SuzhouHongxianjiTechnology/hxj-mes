#region copyright
//------------------------------------------------------------------------------
//  此代码版权声明为全文件覆盖，如有原作者特别声明，会在下方手动补充
//  此代码版权（除特别声明外的代码）归作者本人AlbertZhao所有
//  源代码使用协议遵循本仓库的开源协议及附加协议
//  Gitee源代码仓库：https://gitee.com/AlbertZhao/AlbertCollection



//------------------------------------------------------------------------------
#endregion

using AlbertCollection.Application.BgWorkers;
using AlbertCollection.Core.Hsl;
using Furion.Logging.Extensions;

namespace AlbertCollection.Application
{
    /// <summary>
    /// AppStartup启动类
    /// </summary>
    public class Startup : AppStartup
    {
        /// <summary>
        /// 配置
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            //事件总线
            services.AddEventBus();
            services.AddSingleton<ApplicationCacheService>();
            // 任务队列
            services.AddTaskQueue();
            // 开启后台任务
            services.AddHostedService<BackMessage>();
            // 激活
            var isActive = HslBaseHelper.HslAuthorization();
            if(isActive)
            {
                "Hsl 被激活".LogInformation();
            }
        }
    }
}