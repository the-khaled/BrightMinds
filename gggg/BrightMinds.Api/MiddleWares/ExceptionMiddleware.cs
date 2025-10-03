﻿

using BrightMinds.Api.Errors;
using System.Net;
using System.Text.Json;

namespace BrightMinds.Api.MiddleWares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger,IHostEnvironment environment) 
        {
           _next = next;
           _logger = logger;
           _environment = environment;
        }
        public async Task InvokeAsync(HttpContext context){ 
        
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex) { 
            _logger.LogError(ex,ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode =(int) HttpStatusCode.InternalServerError;

                var errorresopnse = _environment.IsDevelopment() ?
                 new ApiExceptionErrorResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                 : new ApiExceptionErrorResponse((int)HttpStatusCode.InternalServerError);
                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var json=JsonSerializer.Serialize(errorresopnse,options);

                await context.Response.WriteAsync(json);
            }
        
        }

    }
}
