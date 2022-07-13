using System.Threading.Tasks;
using AspDotNetLab2.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace AspDotNetLab2.Middlewares
{
    public class TimerMiddleware
    {
        private readonly RequestDelegate _next;

        public TimerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, TimerServiceInterface timerService)
        {
            if (context.Request.Path.Value.ToLower() == "/services/timer")
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync($"<h3>Поточні дата і час:</h3> {timerService?.GetCurrentDate()}");
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
    public static class TimerMiddlewareExtension
    {
        public static IApplicationBuilder UseTimer
        (this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TimerMiddleware>();
        }
    }
}
