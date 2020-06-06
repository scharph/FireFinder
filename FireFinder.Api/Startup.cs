using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FireFinder.Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FireFinder
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
            services.AddResponseCaching().AddControllers().AddNewtonsoftJson();

            services.AddSingleton<IDataProvider>(new OOELVDataProvider());
            services.AddSingleton<IDistrictProvider>(new DistrictProvider());

            services.AddCors(builder =>
                builder.AddDefaultPolicy(policy =>
                    policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    
                ));
            
            //services.AddSpaStaticFiles(configuration =>
            //{
            //    configuration.RootPath = "FireFinderUI/dist";
            //});

            services.AddOpenApiDocument(
                settings =>
                    settings.PostProcess =
                        d => d.Info.Title = "FireFinder API"
                );
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
            app.UseCors();
            app.UseResponseCaching();

            app.UseStaticFiles();
 
            app.UseHttpsRedirection();
 
            //app.UseSpa(spa =>
            //{
            //    spa.Options.SourcePath = "FireFinderUI";
 
            //    if (env.IsDevelopment())
            //    {
            //        spa.UseAngularCliServer(npmScript: "start");
            //    }
            //});

            app.Use(async (context, next) =>
            {
                context.Response.GetTypedHeaders().CacheControl =
                    new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromSeconds(60)
                    };
                context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
                    new string[] { "Accept-Encoding" };

                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3(settings => settings.Path = "/api");
            app.UseReDoc(settings => settings.Path = "/redoc");

        }
    }
}
