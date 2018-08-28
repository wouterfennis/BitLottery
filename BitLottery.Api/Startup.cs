using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BitLottery.Business;
using BitLottery.Business.RandomGenerator;
using BitLottery.Database;
using BitLottery.Models;
using BitLottery.RandomService;
using BitLottery.RandomService.HttpClientWrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BitLottery.Api
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
            services.AddMvc();
            services.AddTransient<ILottery, Lottery>();
            services.AddTransient<IRandomGenerator, RandomGenerator>();
            services.AddTransient<IRepository<Draw, int>, DrawRepository>();
            services.AddTransient<IRandomService, RandomDotOrgService>();
            services.AddTransient<IHttpClientWrapper, HttpClientWrapper>();

            services.AddDbContext<BitLotteryContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
