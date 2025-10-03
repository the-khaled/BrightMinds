using BrightMinds.Services.Dtos.CategoryDtos;
using BrightMinds.Services.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.ServicesResponse.CategoryResponses
{
     public class CategoryListResponse:GenericResponse<IReadOnlyList<CategoryDetailsDto>>
    {
    }
}
