using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PublicGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .ConfigureAppConfiguration((host, cfg) => {
                     cfg
                         .SetBasePath(host.HostingEnvironment.ContentRootPath)
                         .AddJsonFile("appsettings.json", true, true)
                         .AddJsonFile("ocelot.json")
                         //.AddJsonFile($"appsettings.{host.HostingEnvironment.EnvironmentName}.json", true, true)                       
                         .AddEnvironmentVariables();
                 })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
