using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace OnlineFileStorage
{
    public class GlobalExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e, _logger);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception e, ILogger<GlobalExceptionHandler> logger)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new
            { error = "We're already working on it. Please try again later" });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
