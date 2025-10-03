
using BrightMinds.Core.Models;
using BrightMinds.Services.Dtos.CartCdos;
using BrightMinds.Services.Dtos.CourseDtos;
using BrightMinds.Services.Dtos.IdentityDtos;
using BrightMinds.Services.Dtos.InstructorDtos;
using BrightMinds.Services.Dtos.VideoDtos;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.ApplicationServicesConfigrations
{
    public static class AddProfiles
    {

        public static IServiceCollection AddApplicationProfiles(this IServiceCollection services, IConfiguration configuration)
        {
            var baseUrl = configuration["ApiBaseUrl"];
            // services.AddAutoMapper(m => m.AddProfile(new StoryProfile(configuration)));
            TypeAdapterConfig<AppUser, UserDto>
                 .NewConfig().Map(dest => dest.DisplayName, src => $"{src.FirstName} {src.LastName}")
                 .Map(dest => dest.Mobile, src => src.PhoneNumber)
                .Map(dest => dest.ImageCover, src => $"{baseUrl}/files/UsersImages/{src.ImageCover}");


            TypeAdapterConfig<Instructor, InstructorDetailsDto>
                .NewConfig().Map(dest => dest.DisplayName, src => $"{src.User.FirstName} {src.User.LastName}")
                .Map(dest => dest.Email, src => src.User.Email)
                .Map(dest => dest.ImageCover, src =>$"{baseUrl}/files/UsersImages/{src.User.ImageCover}")
                .Map(dest => dest.Mobile, src => src.User.PhoneNumber);

            TypeAdapterConfig<Course, CourseWithSectionsDto>
                .NewConfig().Map(dest => dest.InstructorName, src=>
                $"{ src.Instructor.User.FirstName} {src.Instructor.User.LastName}")
                .Map(dest => dest.PictureUrl, src => $"{baseUrl}/files/CourseImages/{src.PictureUrl}");

            TypeAdapterConfig<Video,VideoDetailsDto>
                .NewConfig()
                .Map(dest=>dest.SectionName,src=>src.Section.Name)
                .Map(dest => dest.VideoUrl, src => $"{baseUrl}/files/VideosContent/{src.VideoUrl}")
                .Map(dest => dest.CoverUrl, src => $"{baseUrl}/files/VideoImages/{src.CoverUrl}");

            TypeAdapterConfig<CartItem, CartItemWithCourseDto>
            .NewConfig()
            .Map(dest => dest.CourseName, src => src.Course.Name)
            .Map(dest => dest.Price, src => src.Price);
           




            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly()); 
       
            return services;
        }
    }
}
