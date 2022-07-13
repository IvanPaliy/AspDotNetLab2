using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using AspDotNetLab2.Services.Interfaces;
using AspDotNetLab2.Services;
using AspDotNetLab2.Middlewares;

namespace AspDotNetLab2
{
    public class Startup
    {
        private static IServiceCollection _services;
        public void ConfigureServices(IServiceCollection services)
        {
            _services = services;
            services.AddTransient<TimerServiceInterface, TimerService>();
            services.AddScoped<RandomServiceInterface, RandomService>();
            services.AddSingleton<GeneralCounterServiceInterface, GeneralCounterService>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseGeneralCounter();
            app.UseTimer();
            app.UseRandom();
            app.Map("/services", services =>
            {
                services.Map("/list", List);
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var sb = new StringBuilder();
                    sb.Append("<h1>Лабораторна робота №2</h1>");
                    sb.Append("<ul>");
                    sb.Append("<li><a href=\"/services/list\">All Services</a></li>");
                    sb.Append("<li><a href=\"/services/timer\">Date and Time</a></li>");
                    sb.Append("<li><a href=\"/services/random\">Random count</a></li>");
                    sb.Append("<li><a href=\"/services/general-counter\">Number of requests</a></li>");
                    sb.Append("</ul>");
                    context.Response.ContentType = "text/html;charset=utf-8";
                    await context.Response.WriteAsync(sb.ToString());
                });
            });
        }
        public static void List(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var sb = new StringBuilder();
                sb.Append("<h1>All services</h1>");
                sb.Append("<table border=\"1\">");

                sb.Append("<tr><th>#</th><th>Type</th><th>Lifetime</th><th>Realisation</th></tr>");
                for (var i = 0; i < _services.Count; i++)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td>{i + 1}</td>");
                    sb.Append($"<td>{_services[i].ServiceType.FullName}</td>");
                    sb.Append($"<td>{_services[i].Lifetime}</td>");
                    sb.Append($"<td>{_services[i].ImplementationType?.FullName}</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                context.Response.ContentType = "text/html;charset=utf-8";
                await context.Response.WriteAsync(sb.ToString());
            });
        }
    }
}