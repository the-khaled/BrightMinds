using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.Dtos.VideoDtos
{
     public class VideoDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]

        public int SectionId { get; set; }
      //  [Required]
        public IFormFile Video {  get; set; }
        [Required]
        public decimal Duration { get; set; }
        public string? Description { get; set; }
       // [Required]
        public IFormFile CoverImage { get; set; }
    }
}
