using AutoMapper;
using BrightMinds.Core.Models;
using BrightMinds.Services.Dtos.FeedbackDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.Helpers
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping from Entity to DTO
            CreateMap<Feedback, FeedbackDto>()
                .ForMember(dest => dest.Userid, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.CourseId));

            // Mapping from DTO to Entity
            CreateMap<FeedbackDto, Feedback>();
        }
    }
}
