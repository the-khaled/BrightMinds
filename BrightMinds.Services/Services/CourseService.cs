using Azure;
using BrightMinds.Core.Interfaces;
using BrightMinds.Core.Models;
using BrightMinds.Core.Specifications;
using BrightMinds.Services.Dtos.CourseDtos;
using BrightMinds.Services.Helpers;
using BrightMinds.Services.IServices;
using BrightMinds.Services.ServicesResponse.CourseResponses;
using BrightMinds.Services.SpecificationParams;
using BrightMinds.Services.Dtos;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.Services
{
     public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CourseResponse> CreateAsync(CourseDto courseDto)
        {
            var response=new CourseResponse();
            var course = courseDto.Adapt<Course>();
            if (courseDto.Image == null)
            {
                response.Message = "ImageCover is required";
                response.Success = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                return response;    
            }
                course.CreatedDate = DateOnly.FromDateTime(DateTime.Now);
                course.UpdatedDate = DateOnly.FromDateTime(DateTime.Now);
                try
                {
                    course.PictureUrl = await DocumentSetting.UploadFile(courseDto.Image, "CourseImages");
                    await _unitOfWork.CourseRepository.Add(course);
                    await _unitOfWork.CompleteAsync();
                    response.Success = true;
                    response.Message = "Created successfully";
                    response.StatusCode = (int)HttpStatusCode.OK;
                }
                catch (Exception ex)
                {
                    response.Message = "an error occured while adding course";
                    response.Success = false;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
            return response;
        }

        public async Task<CourseResponse> DeleteAsync(int id)
        {
            var response = new CourseResponse();
            var spec = new CourseSpecifications(id);
            var course=await _unitOfWork.CourseRepository.GetWithSpecAsync(spec);
            if (course == null) {
                response.Message = "Course not found";
                response.Success = false;
                response.StatusCode = (int)HttpStatusCode.NotFound;
                return response;    
            }
                await using var transaction = await _unitOfWork.BeginTransactionAsync(); ;

                try
                {
                    _unitOfWork.SectionRepository.DeleteRange(course.Sections);
                    await _unitOfWork.CompleteAsync();

                    course.Sections = null;
                    _unitOfWork.CourseRepository.Delete(course);
                    await _unitOfWork.CompleteAsync();
                    await _unitOfWork.CommitAsync();

                }
                catch (Exception e)
                {
                    await _unitOfWork.RollBackAsync();
                    response.Success = false;
                    response.Message = "an error occured while deleting course";
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;

                }

            return response;

        }

        public async Task<CourseListResponse> GetAllAsync(CourseSpecParams specParams)
        {
            var response=new CourseListResponse();
            var spec = new CourseSpecifications(specParams);
            var courses = await _unitOfWork.CourseRepository.GetAllWithSpecAsync(spec);
            var coursesToReturn =courses.Adapt<IReadOnlyList< CourseWithSectionsDto>>();
            var specToCount = new CourseSpecifications(specParams,true);
            var count= await _unitOfWork.CourseRepository.GetCountWithSpecAsync(specToCount);    
            var pagination=new
                Pagination(specParams.PageSize,specParams.PageIndex,count,coursesToReturn);
            response.Success = true;
            response.Message = "success";
            response.StatusCode = (int)HttpStatusCode.OK;
            response.Data=pagination;

            return response;
        }

        public async Task<CourseResponse> GetAsync(int id)
        {
            var response=new CourseResponse();
            var spec=new CourseSpecifications(id);
            var course = await _unitOfWork.CourseRepository.GetAllWithSpecAsync(spec);
            if (course==null)
            {
                response.Success = false;
                response.Message = "course not found";
                response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else
            {
                var returnedCourse=course.Adapt<CourseWithSectionsDto>();   
                response.Success = true;
                response.Message = "success";
                response.StatusCode = (int)HttpStatusCode.OK;
            }
            return response;
        }

        public async Task<CourseResponse> UpdateAsync(CourseDto courseDto)
        {
            var response = new CourseResponse();
            var course = await _unitOfWork.CourseRepository.GetAsync(courseDto.Id);
            if (course is null)
            {
                response.Message = "course not found";
                response.Success = false;
                response.StatusCode = (int)HttpStatusCode.NotFound;
                return response;    
            }
                course.UpdatedDate = DateOnly.FromDateTime(DateTime.Now);
                course.Description = courseDto.Description;
                course.Name = courseDto.Name;
                if (courseDto.Image != null)
                {
                    course.PictureUrl = await DocumentSetting.UploadFile(courseDto.Image, "CourseImages");
                }
                try
                {
                    _unitOfWork.CourseRepository.Update(course);
                    await _unitOfWork.CompleteAsync();
                    response.Success = true;
                    response.Message = "Updated successfully";
                    response.StatusCode = (int)HttpStatusCode.NoContent;
                }
                catch (Exception ex)
                {
                    response.Message = "an error occured while updating course";
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Success = false;
                }
                return response;
        }
    }
}
