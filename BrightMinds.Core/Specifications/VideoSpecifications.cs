using BrightMinds.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Core.Specifications
{
     public class VideoSpecifications:BaseSpecification<Video>
    {
        public VideoSpecifications(int sectionId) :base(v=>v.SectionId==sectionId)   
        { 
          Includes.Add(v=>v.Section);
            AddOrderBy(c=>c.UpdatedDate);
        }

    }
}
