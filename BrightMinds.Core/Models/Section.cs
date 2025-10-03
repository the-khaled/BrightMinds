using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Core.Models
{
    public class Section:BaseEntity
    {
        public string Name { get; set; }    
        public int CourseId {  get; set; }  
        public string? Description {  get; set; }
        public int Order {  get; set; }
    
        public DateOnly CreatedDate { get; set; }
        public DateOnly UpdatedDate { get; set; }
        public Course Course { get; set; }
        public ICollection<Video> Videos { get; set; }  
    }
}
