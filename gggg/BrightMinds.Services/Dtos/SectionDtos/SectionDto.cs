using BrightMinds.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.Dtos.SectionDtos
{
     public class SectionDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public int Order { get; set; }
       // [Required]
    
    
    }
}
