using BrightMinds.Core.Interfaces;
using BrightMinds.Core.Models;
using BrightMinds.Core.Specifications;
using BrightMinds.Services.Dtos.SectionDtos;
using BrightMinds.Services.IServices;
using BrightMinds.Services.ServicesResponse.SectionResponses;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.Services
{
    public class SectionService : ISectionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SectionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<SectionResponse> CreateAsync(SectionDto sectionDto)
        {
            var response = new SectionResponse();
            try
            {
                var section = sectionDto.Adapt<Section>();
                section.CreatedDate = DateOnly.FromDateTime(DateTime.Now);
                section.UpdatedDate = DateOnly.FromDateTime(DateTime.Now);
                await _unitOfWork.SectionRepository.Add(section);
                await _unitOfWork.CompleteAsync();
                response.Success = true;
                response.Message = "Created successfully";
                response.StatusCode = (int)HttpStatusCode.Created;
            }
            catch (Exception ex) { 
            response.Success=false;
            response.Message = "an error occured while adding section";
            response.StatusCode= (int)HttpStatusCode.InternalServerError;   
            }
            return response;    
        }

        public async Task<SectionResponse> DeleteAsync(int sectionId)
        {
            var response= new SectionResponse();    
            var section =await _unitOfWork.SectionRepository.GetAsync(sectionId); ;
            if (section == null)
            {
                response.Success = false;
                response.Message = "Section not found";
                response.StatusCode = (int)HttpStatusCode.NotFound;
                return response;    
            }
                try
                {
                    _unitOfWork.SectionRepository.Delete(section);
                    await _unitOfWork.CompleteAsync();
                    response.Success = true;
                    response.Message = "Deleted successfully";
                    response.StatusCode = (int)HttpStatusCode.NoContent;
                }
                catch (Exception ex)
                {
                    response.Success = !false;
                    response.Message = "an error occured while Deleting Section";
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
                return response;    
        }

        public async Task<SectionResponse> GetAsync(int sectionId)
        {
            var response=new SectionResponse(); 
            var section=await _unitOfWork.SectionRepository.GetAsync(sectionId);
            if (section == null)
            {
                response.Success = false;
                response.Message = "section not found";
                response.StatusCode= (int)HttpStatusCode.NotFound;  
                return response;
            }
           
                var sectionDto=section.Adapt<SectionDetailsDto>();
                response.Success= true;
                response.Message = "success";
                response.StatusCode= (int)HttpStatusCode.OK;
                response.Data=sectionDto;
            return response;
        }

        public async Task<SectionListResponse> GetByCourseId(int courseId)
        {
           var response = new SectionListResponse();
            var spec = new SectionSpecifications(courseId);
            var sections=await _unitOfWork.SectionRepository.GetAllWithSpecAsync(spec);
            var sectionsToReturn = sections.Adapt<IReadOnlyList<SectionDetailsDto>>();
            response.Success = true;
            response.Message = "success";
            response.StatusCode = (int)HttpStatusCode.OK;   
            return response;    
        }

        public async Task<SectionResponse> UpdateAsync(SectionDto sectionDto)
        {
            var response = new SectionResponse();
            var section=await _unitOfWork.SectionRepository.GetAsync(sectionDto.Id);
            if (section == null)
            {
                response.Success = false;
                response.Message = "section not found";
                response.StatusCode = (int)HttpStatusCode.NotFound; 
                return response;
            }
         
                section.Description = sectionDto.Description;
                section.Name = sectionDto.Name; 
                section.Order = sectionDto.Order;
                section.UpdatedDate=DateOnly.FromDateTime(DateTime.Now);
                try
                {
                    _unitOfWork.SectionRepository.Update(section);
                    await _unitOfWork.CompleteAsync();
                    response.Success = true;
                    response.Message = "Updated successfully";
                    response.StatusCode = (int)HttpStatusCode.NoContent;
                }
                catch (Exception ex) { 
                response.Success = false;
                response.Message = "an error occured while updating section";
                response.StatusCode=(int)HttpStatusCode.InternalServerError; 
                }
            return response;
        }
    }
}
