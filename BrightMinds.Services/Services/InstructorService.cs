using BrightMinds.Core.Interfaces;
using BrightMinds.Core.Models;
using BrightMinds.Core.Specifications;
using BrightMinds.Services.Dtos.InstructorDtos;
using BrightMinds.Services.IServices;
using BrightMinds.Services.ServicesResponse.InstructorResponses;
using BrightMinds.Services.SpecificationParams;
using BrightMinds.Services.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.Services
{
     public class InstructorService : IInstructorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InstructorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<InstructorResponse> CreateAsync(InstructorDto instructorDto)
        {
            var response = new InstructorResponse();
            try
            {
                var instructor = instructorDto.Adapt<Instructor>();
                await _unitOfWork.InstructorRepository.Add(instructor);
                await _unitOfWork.CompleteAsync();
                response.Success = true;
                response.Message = "success";
                response.StatusCode = (int)HttpStatusCode.Created;
            }
            catch (Exception ex) {
                response.Success = false;
                response.Message = "an error occured while creating instructor";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            return response;
        }

        public async Task<InstructorResponse> DeleteAsync(string userId)
        {
            var response= new InstructorResponse();
            var instructor = await _unitOfWork.InstructorRepository.GetByUserId(userId);
            if (instructor == null)
            {
                response.Success = false;
                response.Message = "Instructor not found";
                response.StatusCode = (int)HttpStatusCode.NotFound;
                return response;    
            }
                try
                {
                    _unitOfWork.InstructorRepository.Delete(instructor);
                    await _unitOfWork.CompleteAsync();
                    response.Success = true;
                    response.Message = "Deleted successfully";
                    response.StatusCode = (int)HttpStatusCode.NoContent;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = "an error occured while deleting Instructor";
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
            return response;
        }

        public async Task<InstructorListResponse> GetAllWithSpecAsync(InstructorSpecParams specParams)
        {
            var response = new InstructorListResponse();
            var spec = new InstructorSpecifications(specParams);
            var instructors = await _unitOfWork.InstructorRepository.GetAllWithSpecAsync(spec);
            var instructorsToReturn = instructors.Adapt<IReadOnlyList<InstructorDetailsDto>>();
            var specToCount = new InstructorSpecifications(specParams, true);
            var count=await _unitOfWork.InstructorRepository.GetCountWithSpecAsync(specToCount);
            var pagination = new
               Pagination(specParams.PageSize, specParams.PageIndex, count, instructorsToReturn);
            response.Success = true;
            response.Message = "success";
            response.StatusCode = (int)HttpStatusCode.OK;
            response.Data = pagination;

            return response;
        
        }

     
        public async Task<InstructorResponse> GetAsync(string id)
        {
         var response =new InstructorResponse();
         var spec=new InstructorSpecifications(id);
         var instructor = await _unitOfWork.InstructorRepository.GetWithSpecAsync(spec);
            if (instructor == null)
            {
                response.Success = false;
                response.Message = "Instructor not found";
                response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else
            {
                response.Success = true;
                response.Message = "success";
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Data = instructor.Adapt<InstructorDetailsDto>();
            }
            return response;
        }

        public async Task<InstructorResponse> UpdateAsync(InstructorDto instructorDto)
        {
            var response = new InstructorResponse();
            var instructor = await _unitOfWork.InstructorRepository.GetByUserId(instructorDto.UserId);
          
            try
            {
                //_context.Entry(trackedEntity.Entity).State = EntityState.Detached;
                _unitOfWork.InstructorRepository.Update(instructorDto.Adapt<Instructor>());
                await _unitOfWork.CompleteAsync();
                response.Success = true;
                response.Message = "updated successfully";
                response.StatusCode = (int)HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "an error occured while Updating Instructor";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            return response;    
        }
    }
}
