using BrightMinds.Services.Dtos.CategoryDtos;
using BrightMinds.Services.ServicesResponse.CategoryResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.IServices
{
     public interface ICategoryService
    {
        public Task<CategoryResponse>GetByIdAsync(int id);
        public Task<CategoryResponse> CreateAsync(CategoryDto categoryDto);
        public Task<CategoryResponse> UpdateAsync(int id,CategoryDto categoryDto);
        public Task<CategoryResponse> DeleteAsync(int id);
        public Task<CategoryListResponse>GetAllAsync();
    }
}
