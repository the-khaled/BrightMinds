using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Core.Models
{
     public class Course:BaseEntity
    {
        public string Name { get; set; }   
        public string Description { get; set; } 
        public string InstructorId { get; set; }
        public int CategoryId { get; set; }
        public string PictureUrl { get; set; }  
        public DateOnly CreatedDate { get; set; }  
        public DateOnly UpdatedDate { get; set; }   
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public Instructor Instructor { get; set; }  
        public ICollection<Section> Sections { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }

    }
}
