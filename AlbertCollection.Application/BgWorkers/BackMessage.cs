using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbertCollection.Application.BgWorkers
{
    /// <summary>
    /// 所有日志
    /// </summary>
    public class BackMessage : BackgroundService
    {
        private readonly ILogger<BackMessage> _logger;

        /// <summary>
        /// 日志队列
        /// </summary>
        public static ConcurrentQueue<string> DebugPageMessages { get; set; } = new();

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="logger"></param>
        public BackMessage(ILogger<BackMessage> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 添加消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logLevel"></param>
        public static void AddMessage(string message, LogLevel logLevel)
        {
            DebugPageMessages.Enqueue(message.FormatOutput(LogLevel.Information));
        }

        /// <summary>
        /// 后台任务
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (DebugPageMessages.Count > 12)
                {
                    DebugPageMessages.TryDequeue(out _);
                }
                await Task.Delay(200, stoppingToken); // 延迟1秒，检查取消标记
            }
        }
    }
}
