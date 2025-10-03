
using BrightMinds.Infrastructure.DataAccess;
using BrightMinds.Core.Models;
using BrightMinds.Services.ApplicationServicesConfigrations;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;
using Blendd.Api.Extensions;

namespace BrightMinds.Api.Extensions
{
    public static class UpdateDatabaseExtension
    {
        public async static Task<WebApplication>UpdateDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            await ApplyMigration.ApplyMigrationsAsync(scope);
            var context = scope.ServiceProvider.GetRequiredService<BrightMindsContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await app.SeedUsersAndRoles(userManager, roleManager);
               await app.SeedAppData(context);
            return app;
        }
    }
}
