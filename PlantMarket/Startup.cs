using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlantMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.SpaServices.AngularCli;

namespace PlantMarket
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private const string AngularClientName = "PlantMarketClient";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PlantMarketContext>(options =>
                options.UseSqlServer(Configuration
                    .GetSection("ConnectionStrings")
                        .GetValue<string>("DefaultDbConnection")));

            services.RegisterDependency();

            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                });

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();


            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = $"{AngularClientName}/dist";
            });


            /*services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlantMarket", Version = "v1" });
            });*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c =>c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlantMarket v1"));
            }

            app.UseHttpsRedirection();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints
                .MapDefaultControllerRoute();
            })
            .UseSpa(spa =>
            {
                spa.Options.SourcePath = $"{AngularClientName}";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
