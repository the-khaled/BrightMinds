using BrightMinds.Core.Interfaces;
using BrightMinds.Core.Models;
using BrightMinds.Core.Specifications;
using BrightMinds.Services.Dtos.VideoDtos;
using BrightMinds.Services.Helpers;
using BrightMinds.Services.IServices;
using BrightMinds.Services.ServicesResponse.VideoResponses;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.Services
{
     public class VideoService : IVideoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VideoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<VideoResponse> CreateAsync(VideoDto videoDto)
        {
         
            var response = new VideoResponse();
            if(videoDto.CoverImage==null||videoDto.Video==null)
            {
                response.Message = "ImageCover and Video is Required";
                response.StatusCode= (int)HttpStatusCode.BadRequest;
                response.Success = false;
                return response;    
            }
            var video = videoDto.Adapt<Video>();
            video.CreatedDate=DateOnly.FromDateTime(DateTime.Now);
            video.UpdatedDate=DateOnly.FromDateTime(DateTime.Now);
            video.VideoUrl = await DocumentSetting.UploadFile(videoDto.Video, "VideosContent");
            video.CoverUrl = await DocumentSetting.UploadFile(videoDto.CoverImage, "VideoImage");
            try
            {
                await _unitOfWork.VideoRepository.Add(video);
                await _unitOfWork.CompleteAsync();
                response.Success = true;
                response.Message = "Added Successfully";
                response.StatusCode = (int)HttpStatusCode.OK;
            }
            catch (Exception ex) {
                response.Success = false;
                response.Message = "an error occured while adding Video";
                response.StatusCode = (int)HttpStatusCode.Created;
            }
            return response;            
        }

        public async Task<VideoResponse> DeleteAsync(int id)
        {
            var response =new VideoResponse();  
            var video=await _unitOfWork.VideoRepository.GetAsync(id);
            if (video == null)
            {
                response.Success = false;
                response.Message = "Video not found";
                response.StatusCode = (int)HttpStatusCode.NotFound;
                return response;
            }
                try
                {
                    _unitOfWork.VideoRepository.Delete(video);
                    await _unitOfWork.CompleteAsync();
                    await DocumentSetting.DeleteFile(video.CoverUrl, "VideoImages");
                    await DocumentSetting.DeleteFile(video.VideoUrl, "VideoImages");
                    response.Success = true;
                    response.Message = "Deleted successfully";
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = "an error occure while deleting video";
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
            
                return response;
            
        }

        public async Task<VideoResponse> GetByIdAsync(int id)
        {
            var response =new VideoResponse();
            var video = await _unitOfWork.VideoRepository.GetAsync(id);
            if (video == null)
            {
                response.Success = false;
                response.Message = "Video not found";
                response.StatusCode= (int)HttpStatusCode.NotFound;  
                return response;
            }
            var videoDto=video.Adapt<VideoDetailsDto>();
            response.Success= true;
            response.Message = "success";
            response.StatusCode =(int)HttpStatusCode.OK;
            return response;    
        }

        public async Task<VideoListResponse> GetBySectionIdAsync(int sectionId)
        {
            var response = new VideoListResponse();   
            var spec= new VideoSpecifications(sectionId);
            var videos=await _unitOfWork.VideoRepository.GetWithSpecAsync(spec);
            var vidoesDto=videos.Adapt<IReadOnlyList< VideoDetailsDto>>();
            response.Success= true;
            response.Message = "success";
            response.StatusCode =(int)HttpStatusCode.OK;
            response.Data=vidoesDto;
            return response;
        }

        public async Task<VideoResponse> UpdateAsync(VideoDto videoDto)
        {
            var response = new VideoResponse();
            var video = await _unitOfWork.VideoRepository.GetAsync(videoDto.Id);
            if (video == null)
            {
                response.Success = false;
                response.Message = "Video not found";
                response.StatusCode = (int)HttpStatusCode.NotFound;
                return response;
            }
                video.UpdatedDate = DateOnly.FromDateTime(DateTime.Now);
                 if(videoDto.CoverImage != null)
                video.VideoUrl = await DocumentSetting.UploadFile(videoDto.Video, "VideosContent");
                if (videoDto.Video != null)
                    video.CoverUrl = await DocumentSetting.UploadFile(videoDto.CoverImage, "VideoImage");
                try
                {
                     _unitOfWork.VideoRepository.Update(video);
                    await _unitOfWork.CompleteAsync();
                    response.Success = true;
                    response.Message = "Updated Successfully";
                    response.StatusCode = (int)HttpStatusCode.NoContent;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = "an error occured while Updating Video";
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
            return response;
        }
    }
}
