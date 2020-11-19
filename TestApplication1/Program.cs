using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Serilog;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TestApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((hostContext, logging) =>
                {
                    var logConfig =
                        new LoggerConfiguration()
                            .MinimumLevel.Verbose()
                            .Enrich.FromLogContext()
                            .WriteTo.Map(
                                keySelector: e => e.Level,
                                configure: (level, wt) => wt.File($"logs\\test-app-{level}.log"));

                    logging.AddSerilog(logConfig.CreateLogger());
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
    }
}
