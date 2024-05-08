using MarketPlace.WebUI.Middlewares.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace MarketPlace.WebUI.Middlewares
{
    public class TimingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public TimingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;   
            _logger = loggerFactory.CreateLogger<TimingMiddleware>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var start = DateTime.UtcNow;
                await _next(context);
                _logger.LogInformation($"Request: {context.Request.Path} Timing: {(DateTime.UtcNow - start).TotalMilliseconds}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception has occured");

                switch (ex)
                {
                    case ValidationException _:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                await CreateExceptionResponseAsync(context, ex);

            }
        }

        private static Task CreateExceptionResponseAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(new ErrorDetail()
            {

                StatusCode = context.Response.StatusCode,
                Message = ex.Message

            }.ToString());
        }
    }
}
