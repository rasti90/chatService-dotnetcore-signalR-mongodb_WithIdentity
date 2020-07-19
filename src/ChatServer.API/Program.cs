using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ChatServer.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging => {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder => {
                    // webBuilder.UseSentry(options =>
                    // {
                    //     options.Debug = true;
                    //     options.MaxRequestBodySize = RequestSize.Always;
                    //     options.Dsn = "Your Sentry Dsn Address";
                    //     options.BeforeSend = @event =>
                    //     {
                    //         // Never report server names
                    //         @event.ServerName = null;
                    //         return @event;
                    //     };
                    // });
                    webBuilder.UseStartup<Startup>();
                    //.UseUrls ("https://localhost:6001");
                });
        }
    }
}
