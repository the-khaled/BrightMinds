using BrightMinds.Services.Dtos.SectionDtos;
using BrightMinds.Services.ServicesResponse.SectionResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.IServices
{
     public interface ISectionService
    {
        public Task<SectionResponse> CreateAsync(SectionDto sectionDto);
        public Task<SectionResponse> UpdateAsync(SectionDto sectionDto);
        public Task<SectionResponse> DeleteAsync(int sectionId);    
        public Task<SectionResponse> GetAsync(int sectionId);   
        public Task<SectionListResponse>GetByCourseId(int courseId);    

    }
}
