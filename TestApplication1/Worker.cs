using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TestApplication1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly Random _random;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _random = new Random();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested == false)
            {
                var randomLevel = GetRandomLogLevel();
                _logger.Log(randomLevel, "Random {level} worker message at {time}", randomLevel, DateTimeOffset.Now);
                await Task.Delay(99, stoppingToken);
            }
        }

        private LogLevel GetRandomLogLevel() =>
            (LogLevel)_random.Next(0, 6);
    }
}
