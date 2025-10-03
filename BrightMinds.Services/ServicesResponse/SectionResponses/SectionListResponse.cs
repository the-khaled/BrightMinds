using BrightMinds.Services.Dtos.SectionDtos;
using BrightMinds.Services.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.ServicesResponse.SectionResponses
{
    public class SectionListResponse:GenericResponse<IReadOnlyList<SectionDetailsDto>>
    {
    }
}
