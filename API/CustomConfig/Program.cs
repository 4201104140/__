using CustomConfig.CustomProvider;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomConfig
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var options = host.Services.GetRequiredService<IOptions<WidgetOptions>>().Value;

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((_, configuration) =>
                    {
                        //configuration.Sources.Clear();
                        //configuration.AddEntityConfiguration(reloadOnChange: true);
                    });
                    webBuilder.ConfigureServices((context, services) =>
                        services.Configure<WidgetOptions>(
                            context.Configuration.GetSection("WidgetOptions")));
                    webBuilder.UseStartup<Startup>();
                });
    }
}
