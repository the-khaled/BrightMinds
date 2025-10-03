using BrightMinds.Services.Dtos.VideoDtos;
using BrightMinds.Services.ServicesResponse.VideoResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.IServices
{
     public interface IVideoService
    {
        public Task<VideoListResponse>GetBySectionIdAsync(int sectionId);
        public Task<VideoResponse> GetByIdAsync(int id);
        public Task<VideoResponse> CreateAsync(VideoDto videoDto);
        public Task<VideoResponse> UpdateAsync(VideoDto videoDto);
        public Task<VideoResponse> DeleteAsync(int id);
    }
}
