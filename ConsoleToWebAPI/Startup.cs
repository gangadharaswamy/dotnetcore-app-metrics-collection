using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus;

namespace ConsoleToWebAPI
{
    class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(options => { options.AllowSynchronousIO = true; });
            services.AddMetrics();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseHttpMetrics();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                /*MapGet("/",async context =>
                {
                   await context.Response.WriteAsync("Hello - Web API app");
                });*/
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Testing - .NET Core Web API app!!!");
                });
                //object p1 = endpoints.MapMetrics();
                endpoints.MapMetrics();
            });
        }
    }
}
