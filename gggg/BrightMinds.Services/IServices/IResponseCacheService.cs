using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.IServices
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cachKey,object response,TimeSpan timeSpan);
        Task<string> GetCacheResponseAsync(string cachKey);

    }
}
