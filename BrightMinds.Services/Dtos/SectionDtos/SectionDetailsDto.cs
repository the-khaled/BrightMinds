using BrightMinds.Services.Dtos.VideoDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.Dtos.SectionDtos
{
    public class SectionDetailsDto
    {
        public string Name { get; set; }
        public int CourseId { get; set; }
        public string? Description { get; set; }
        public int Order { get; set; }
        public DateOnly CreatedDate { get; set; }
        public DateOnly UpdatedDate { get; set; }
       
        //public IReadOnlyList<VideoDetailsDto>Videos { get; set; }
    }
}
