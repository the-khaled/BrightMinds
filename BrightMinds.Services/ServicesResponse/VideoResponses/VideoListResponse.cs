using BrightMinds.Services.Dtos.VideoDtos;
using BrightMinds.Services.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.ServicesResponse.VideoResponses
{
    public class VideoListResponse:GenericResponse<IReadOnlyList<VideoDetailsDto>>
    {
    }
}
