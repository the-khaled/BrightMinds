using BrightMinds.Infrastructure.Migrations;
using BrightMinds.Services.Dtos.InstructorDtos;
using BrightMinds.Services.ServicesResponse.InstructorResponses;
using BrightMinds.Services.SpecificationParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.IServices
{
    public interface IInstructorService
    {
        public Task<InstructorResponse> CreateAsync(InstructorDto instructorDto);
        public Task<InstructorResponse> UpdateAsync(InstructorDto instructorDto);
        public Task<InstructorResponse> DeleteAsync(string userId);
        public Task<InstructorResponse> GetAsync(string userId);
        public Task<InstructorListResponse> GetAllWithSpecAsync(InstructorSpecParams specParams); 
       

    }
}
