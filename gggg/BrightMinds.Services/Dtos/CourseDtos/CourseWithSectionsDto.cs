using BrightMinds.Services.Dtos.SectionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.Dtos.CourseDtos
{
     public class CourseWithSectionsDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string InstructorName { get; set; }
        public string PictureUrl { get; set; }
        public DateOnly CreatedDate { get; set; }
        public DateOnly UpdatedDate { get; set; }
       // public IReadOnlyList<SectionDto> Sections {  get; set; }    
    }
}
