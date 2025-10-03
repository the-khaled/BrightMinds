using BrightMinds.Services.Dtos.CourseDtos;
using BrightMinds.Services.ServicesResponse.CourseResponses;
using BrightMinds.Services.SpecificationParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.IServices
{
     public interface ICourseService
    {
        public Task<CourseResponse> CreateAsync(CourseDto courseDto);
        public Task<CourseResponse> UpdateAsync(CourseDto courseDto);
        public Task<CourseResponse> DeleteAsync(int id);
        public Task<CourseResponse> GetAsync(int id);
        public Task<CourseListResponse> GetAllAsync(CourseSpecParams specParams);
        //public Task<CourseListResponse>GetByCategoryId (int categoryId);    


    }
}
