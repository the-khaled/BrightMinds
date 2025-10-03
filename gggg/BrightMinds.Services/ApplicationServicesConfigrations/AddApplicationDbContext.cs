

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrightMinds.Infrastructure.DataAccess;

namespace BrightMinds.Services.ApplicationServicesConfigrations
{
     public static class ApplicationDbContext
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, string? ConnectionString, string? RedisConnection)
        {
            services.AddDbContext<BrightMindsContext>(option =>
          option.UseSqlServer(ConnectionString));


            //services.AddSingleton<IConnectionMultiplexer>(
            //    s =>
            //    {
            //        var connection = ConfigurationOptions.Parse(RedisConnection);
            //        return ConnectionMultiplexer.Connect(connection);
            //    }
            //);
            return services;
        }
    }
}
