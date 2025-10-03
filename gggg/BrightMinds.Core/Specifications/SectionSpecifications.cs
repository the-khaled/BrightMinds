using BrightMinds.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Core.Specifications
{
     public class SectionSpecifications:BaseSpecification<Section>
    {
        public SectionSpecifications(int courseId):base(s=>s.CourseId==courseId)
        {
           // Includes.Add(s => s.Videos);
            AddOrderBy(s => s.Order);
        }


    }
}
