using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace QuickService_AdminAPI
{
    public class Program
    {
        //public static void Main(string[] args)
        //{
        //    var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        //    try
        //    {
        //        logger.Debug("init main");
        //        CreateHostBuilder(args).Build().Run();
        //    }
        //    catch (Exception exception)
        //    {
        //        //NLog: catch setup errors
        //        logger.Error(exception, "Stopped program because of exception");
        //        throw;
        //    }
        //    finally
        //    {
        //        // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
        //        LogManager.Shutdown();
        //    }
        //}

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog(); // NLog: Setup NLog for Dependency injection
        }

        public static async Task Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            var host = CreateWebHostBuilder(args).Build();
            //using var scope = host.Services.CreateScope();
            //var services = scope.ServiceProvider;
            try
            {
                logger.Debug("init main function");

                //using var scope = host.Services.CreateScope();
                //var services = scope.ServiceProvider;

                //var context = services.GetRequiredService<AppDbContext>();
                //await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error in init");
                //throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }

            await host.RunAsync();
            //CreateHostBuilder(args).Build().Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Information);
                })
                .UseNLog();
    }
}