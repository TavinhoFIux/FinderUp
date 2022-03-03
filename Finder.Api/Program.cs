using Finder.Repository.DAO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finder.Api
{
   public class Program
    {
        public static void Main(string[] args)
        {            
            var host = CreateHostBuilder(args).Build();

            IConfiguration configs = host.Services.GetService<IConfiguration>();

            if (args.Contains("seed"))
            {
                DatabaseBootstrap dateRepository = new DatabaseBootstrap(configs.GetConnectionString("Master"));
                dateRepository.Setup().Wait();
            }

            host.Run(); 
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
