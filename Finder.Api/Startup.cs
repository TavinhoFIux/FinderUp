using Finder.Repository.FeedStockCatchRepository;
using Finder.Repository.FeedStockRepository;
using Finder.Repository.UserRepository;
using Finder.Service.FeedStockService;
using Finder.Service.FeesStockCatchService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
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
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IFeedStockService, FeedStockService>();
            services.AddTransient<IFeedStockRepository, FeedStockRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IFeedStockRepository, FeedStockRepository>();
            services.AddTransient<IFeedStockCatchService, FeedStockCatchService>();
            services.AddTransient<IFeedStockCatchRepository, FeedStockCatchRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
