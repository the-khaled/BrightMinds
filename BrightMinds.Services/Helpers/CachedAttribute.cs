
using BrightMinds.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLifeInSeconds;

        public CachedAttribute(int timeToLifeInSeconds)
        {
            _timeToLifeInSeconds = timeToLifeInSeconds;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var responsecache = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            var cachKey= GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cachResponse = await responsecache.GetCacheResponseAsync(cachKey);
            if(!string.IsNullOrEmpty(cachResponse))
            {
                var contentResult = new ContentResult()
                {
                    Content = cachResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentResult;
                return;
            }
            var ExcutedEndpointContext=await next.Invoke();
            if( ExcutedEndpointContext.Result is OkObjectResult okObjectResult ) 
            { 
                await responsecache.CacheResponseAsync(cachKey, okObjectResult.Value,TimeSpan.FromSeconds(_timeToLifeInSeconds));
            }
        }

        private  string GenerateCacheKeyFromRequest(HttpRequest request)
        {
          var keyBuilder= new StringBuilder();
            keyBuilder.Append(request.Path);
            foreach(var (key,Value)in  request.Query.OrderBy(o=>o.Key)) {
                keyBuilder.Append($"|{key}-{Value}");
            
            }
            return keyBuilder.ToString();
        }
    }
}
