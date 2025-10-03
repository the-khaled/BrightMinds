using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.Dtos.FeedbackDtos
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Content field is required.")]
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Userid { get; set; } 
        public int CourseId { get; set; }
    }
}
