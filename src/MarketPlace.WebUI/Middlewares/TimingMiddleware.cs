using MarketPlace.WebUI.Middlewares.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;

namespace MarketPlace.WebUI.Middlewares
{
    public class TimingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TimingMiddleware> _logger;
        public TimingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;   
            _logger = loggerFactory.CreateLogger<TimingMiddleware>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            await _next(context);

            stopwatch.Stop();
            _logger.LogInformation("Request {RequestMethod} {RequestPath} completed in {ElapsedMilliseconds} ms",context.Request?.Method,
                context.Request?.Path.Value,stopwatch.ElapsedMilliseconds);
        }
      
    }
}
