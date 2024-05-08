using MarketPlace.WebUI.Middlewares;

namespace MarketPlace.WebUI.Extentions
{
    public static class MiddlewareExtentions
    {
        public static IApplicationBuilder UseTiming(this IApplicationBuilder app) => app.UseMiddleware<TimingMiddleware>();

    }
}
