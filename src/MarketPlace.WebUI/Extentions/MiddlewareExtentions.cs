using MarketPlace.WebUI.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace MarketPlace.WebUI.Extentions
{
    public static class MiddlewareExtentions
    {
        public static IApplicationBuilder UseTiming(this IApplicationBuilder app) => app.UseMiddleware<TimingMiddleware>();
        public static IApplicationBuilder UseCustomExeptionHandling(this IApplicationBuilder app) =>
          app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
