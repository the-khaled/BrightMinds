

using Blendd.Api.Extensions;
using BrightMinds.Api.Extensions;
using BrightMinds.Api.MiddleWares;
using BrightMinds.Core.Interfaces;
using BrightMinds.Core.Models;
using BrightMinds.Infrastructure.DataAccess;
using BrightMinds.Infrastructure.Repositories;
using BrightMinds.Services.ApplicationServicesConfigrations;
using BrightMinds.Services.IServices;
using BrightMinds.Services.Services;
using Library.Services.IServices;
using Library.Services.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;

namespace BrightMinds.Api
{
    public class Program
    {
        public static  async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<ICouponService, CouponService>();
            builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            builder.Services.AddScoped<IFeedbackService, FeedbackService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddValidationErrorResponseHelper();
            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 100000000; // 100 MB
            });
            builder.Services.AddApplicationDbContext(builder.Configuration.GetConnectionString("DefaultConnection")
                , builder.Configuration.GetConnectionString("Redis"));

            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
            })
                      .AddEntityFrameworkStores<BrightMindsContext>()
                      .AddDefaultTokenProviders();
            builder.Services.AddApplicationServices(builder.Configuration);

            builder.Services.AddCors(
              option => {
                  option.AddPolicy("MyPolicy", options => {
                      options.AllowAnyHeader().
                       AllowAnyMethod()
                     .AllowAnyOrigin();
                  });
              });



            var app = builder.Build();
            await app.UpdateDatabase();
           // await app.SeedUsersAndRoles();
            app.UseMiddleware<ExceptionMiddleware>();
            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //  }
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseCors("MyPolicy");
            app.UseHttpsRedirection();
            /*app.UseAuthentication();
            app.UseAuthorization();*/   ///////////
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
