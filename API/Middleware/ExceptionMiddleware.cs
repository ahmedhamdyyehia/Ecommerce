using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _Next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger
        ,IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _Next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _Next(context);

            }
            catch(Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                context.Response.ContentType="application/json";
                context.Response.StatusCode=(int) HttpStatusCode.InternalServerError;
                var response=_env.IsDevelopment()
                ? new ApiException((int) HttpStatusCode.InternalServerError,ex.Message
                ,ex.StackTrace.ToString())
                : new ApiException((int) HttpStatusCode.InternalServerError );
                var option=new JsonSerializerOptions {PropertyNamingPolicy=JsonNamingPolicy.CamelCase};

                var json=JsonSerializer.Serialize(response,option);
                await context.Response.WriteAsync(json);
                
            }
        }
    }
}