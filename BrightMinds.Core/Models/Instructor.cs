using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Core.Models
{
    public class Instructor:BaseEntity
    {
       
        public string UserId { get; set; } 
        public string? Qualifications { get; set; }
       public string? JobTitle {  get; set; }    
        public AppUser User { get; set; }   
        public ICollection<Course> Courses { get; set; }   

    }
}
