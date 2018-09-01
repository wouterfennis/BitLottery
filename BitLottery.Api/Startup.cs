using BitLottery.Business;
using BitLottery.Business.RandomGenerator;
using BitLottery.Business.RandomWrapper;
using BitLottery.Database;
using BitLottery.Models;
using BitLottery.RandomService;
using BitLottery.RandomService.HttpClientWrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

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

            // Business
            services.AddTransient<ILottery, Lottery>();
            services.AddTransient<IRandomGenerator, RandomGenerator>();
            services.AddTransient<IRandomWrapper, RandomWrapper>();

            // Repositories
            services.AddTransient<IRepository<Draw, int>, DrawRepository>();
            services.AddTransient<IRepository<Ballot, int>, BallotRepository>();

            // Services
            services.AddTransient<IRandomService, RandomDotOrgService>();
            services.AddTransient<IHttpClientWrapper, HttpClientWrapper>();

            services.AddDbContext<BitLotteryContext>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();
        }
    }
}
