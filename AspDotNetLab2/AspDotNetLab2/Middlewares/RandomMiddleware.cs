using System.Text;
using System.Threading.Tasks;
using AspDotNetLab2.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AspDotNetLab2.Middlewares
{
    public class RandomMiddleware
    {
        private readonly RequestDelegate _next;
        public RandomMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, RandomServiceInterface firstRandomService)
        {
            {
                if (context.Request.Path.Value.ToLower() == "/services/random")
                {
                    var sb = new StringBuilder();
                    sb.Append("<h3>Випадкове число:</h3>");
                    sb.Append("<p> 1 звернення до сервісу:");
                    sb.Append($" {firstRandomService?.RandomValue} </p>");
                    sb.Append("<p> 2 звернення до сервісу:");
                    var secondRandomService =
                   context.RequestServices.GetService<RandomServiceInterface>();
                    sb.Append($" {secondRandomService?.RandomValue} </p>");
                    context.Response.ContentType = "text/html;charset=utf-8";
                    await context.Response.WriteAsync(sb.ToString());
                }
                else
                {
                    if (_next != null)
                    {
                        await _next.Invoke(context);
                    }
                }
            }
        }
    }
    public static class RandomMiddlewareExtension
    {
        public static IApplicationBuilder UseRandom
        (this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RandomMiddleware>();
        }
    }
}
