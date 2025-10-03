using BrightMinds.Core.Interfaces;
using BrightMinds.Core.Models;
using BrightMinds.Services.Dtos.CategoryDtos;
using BrightMinds.Services.IServices;
using BrightMinds.Services.ServicesResponse.CategoryResponses;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CategoryResponse> CreateAsync(CategoryDto categoryDto)
        {
            var response=new CategoryResponse();
            try
            {
                await _unitOfWork.CateogryRepository.Add(categoryDto.Adapt<Category>());
                await _unitOfWork.CompleteAsync();
                response.Success = true;
                response.Message = "Created successfuly";
                response.StatusCode = (int)HttpStatusCode.Created;

            }
            catch (Exception ex) {
                response.Success = false;
                response.Message = "an error occured while adding category";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

            }
            return response;
        }

        public async Task<CategoryResponse> DeleteAsync(int id)
        {
            var response = new CategoryResponse();
            var category=await _unitOfWork.CateogryRepository.GetAsync(id);
            if (category == null) {
                response.Success = false;
                response.Message = "Category not found";
                response.StatusCode=(int)HttpStatusCode.NotFound;
                return response;
            }
            try
            {
                _unitOfWork.CateogryRepository.Delete(category);
                await _unitOfWork.CompleteAsync();
                response.Success = true;
                response.Message = "Deleted successfully";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            catch (Exception ex) {
                response.Success = false;
                response.Message = "an error occured while deleting category";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

            }
            return response;
        }

        public async Task<CategoryListResponse> GetAllAsync()
        {
            var response =new CategoryListResponse();
           var categories= await _unitOfWork.CateogryRepository.GetAllAsync();
           var categoriesDto=categories.Adapt<IReadOnlyList<CategoryDetailsDto>>();
            response.Success = true;
            response.StatusCode=(int)(HttpStatusCode.OK);
            response.Message = "success";
            response.Data = categoriesDto;
            return response;
        }

        public async Task<CategoryResponse> GetByIdAsync(int id)
        {
            var response = new CategoryResponse();
            var category= await _unitOfWork.CateogryRepository.GetAsync(id);
            if(category ==null)
            {
                response.Success = false;
                response.Message = "Category not found";
                response.StatusCode= (int)HttpStatusCode.NotFound;
            }    
            else
            {
                response.Success = true;
                response.Message = "success";
                response.Data = category.Adapt<CategoryDetailsDto>();
                response.StatusCode = (int)(HttpStatusCode.OK);
            }
            return response;
        }

        public async Task<CategoryResponse> UpdateAsync(int id, CategoryDto categoryDto)
        {
            var response = new CategoryResponse();
            var category = await _unitOfWork.CateogryRepository.GetAsync(id);
            if (category == null)
            {
                response.Success = false;
                response.Message = "Category not found";
                response.StatusCode = (int)HttpStatusCode.NotFound;
                return response;
            }
            try
            {
                _unitOfWork.CateogryRepository.Update(category);
                await _unitOfWork.CompleteAsync();
                response.Success = true;
                response.Message = "Updated successfully";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "an error occured while Updating category";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

            }
            return response;
        }
    }
}
