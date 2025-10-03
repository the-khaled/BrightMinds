using BrightMinds.Core.ISpecificationParams;
using BrightMinds.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Core.Specifications
{
     public class CourseSpecifications:BaseSpecification<Course>
    {
        public CourseSpecifications(ICourseSpecParams specParams)
          : base(BuildCriteria(specParams))
        {
            Includes.Add(c => c.Instructor);
            Includes.Add(c => c.Instructor.User);
            ApplyPagination(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

        }

     



        public CourseSpecifications(int id):base(c=>c.Id==id)
        {
            Includes.Add(c => c.Instructor);
            Includes.Add(c => c.Sections);
        }
        public CourseSpecifications(ICourseSpecParams specParams,bool isCountQuery)
          : base(BuildCriteria(specParams))
        {
           
        }

        private static Expression<Func<Course, bool>> BuildCriteria(ICourseSpecParams specParams)
        {
            return c =>
                (string.IsNullOrEmpty(specParams.SearchName)
            || c.Name.ToLower().Contains(specParams.SearchName.ToLower())
            &&specParams.CategoryId.HasValue||c.CategoryId==specParams.CategoryId
            && string.IsNullOrEmpty(specParams.SearchName)||c.InstructorId==specParams.InstructorId);
        }


    }
}
