using System.Threading.Tasks;
using AspDotNetLab2.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;


namespace AspDotNetLab2.Middlewares
{
    public class GeneralCounterMiddleware
    {
        private readonly RequestDelegate _next;
        private GeneralCounterServiceInterface _generalCounterService;
        public GeneralCounterMiddleware(RequestDelegate next, GeneralCounterServiceInterface generalCounterService)
        {
            _next = next;
            _generalCounterService = generalCounterService;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.Value.ToLower() == "/services/general-counter")
            {
                _generalCounterService.IncrementRequests();
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync($"<h3>Кількість запитів:</h3> {_generalCounterService?.RequestsValue}");
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
    public static class GeneralCounterMiddlewareExtension
    {
        public static IApplicationBuilder UseGeneralCounter
        (this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GeneralCounterMiddleware>();
        }
    }
}
