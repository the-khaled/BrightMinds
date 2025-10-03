using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.Dtos.CourseDtos
{
     public class CourseDto
    {
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public string InstructorId { get; set; }
        //[Required]

        public IFormFile Image { get; set; }
    }
}
