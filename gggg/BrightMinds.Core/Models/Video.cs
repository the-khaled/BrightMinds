using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BrightMinds.Core.Models
{
     public class Video:BaseEntity
    {
        public string Name {  get; set; }   
        public int SectionId {  get; set; } 
        public string VideoUrl { get; set; }
        public string CoverUrl { get; set; }    
        public decimal Duration {  get; set; }  
        public string? Description { get; set; }
        public DateOnly CreatedDate { get; set; }
        public DateOnly UpdatedDate { get; set; }
        public Section Section { get; set; }    



    }
}
