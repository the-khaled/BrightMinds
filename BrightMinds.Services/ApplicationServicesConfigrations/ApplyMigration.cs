

using BrightMinds.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.ApplicationServicesConfigrations
{
    public class ApplyMigration
    {
        public static async Task ApplyMigrationsAsync(IServiceScope scope)
        {
            var service = scope.ServiceProvider;
            var LoggerFactory = service.GetRequiredService<ILoggerFactory>();

            try
            {
                var dbcontext = service.GetService<BrightMindsContext>();
                await dbcontext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<ApplyMigration>();
                logger.LogError(ex, "An error occured during apply migration");
            }
        }

    }
}
