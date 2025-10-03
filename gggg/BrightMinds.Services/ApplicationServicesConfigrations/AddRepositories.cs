
using BrightMinds.Core.Interfaces;
using BrightMinds.Infrastructure.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.ApplicationServicesConfigrations
{
    public static class AddRepositories
    {
        public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
