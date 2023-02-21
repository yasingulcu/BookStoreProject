using BookStorePatika.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace BookStorePatika.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;

        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var watch = Stopwatch.StartNew();
            try
            {
                string message = "[Request] HTTP " + httpContext.Request.Method + " - " + httpContext.Request.Path;
                _loggerService.Write(message);

                await _next.Invoke(httpContext);


                //message = "[Request] HTTP " +
                //    httpContext.Request.Method + " - " + httpContext.Request.Path +
                //    " responded " + httpContext.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms ";



            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(httpContext, ex, watch);
            }
        }

        private Task HandleException(HttpContext httpContext, Exception ex, Stopwatch watch)
        {
            string message = "[Error] HTTP" + httpContext.Request.Method + " - " + httpContext.Response.StatusCode + " Error Message " + ex.Message +
                " in " + watch.Elapsed.TotalMilliseconds;
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);

            return httpContext.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
