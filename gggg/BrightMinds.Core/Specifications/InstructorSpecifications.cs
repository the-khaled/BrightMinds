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
     public class InstructorSpecifications:BaseSpecification<Instructor>
    {
        public InstructorSpecifications(IInstructorSpecParams specParams)
          : base(BuildCriteria(specParams))

        {
            Includes.Add(i => i.User);

            ApplyPagination(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

        }
        public InstructorSpecifications(string userId):base(b=>b.UserId==userId)
        {
            Includes.Add(i => i.User);
        }
        
        public InstructorSpecifications(IInstructorSpecParams specParams,bool isCountQuery)
          : base(BuildCriteria(specParams))
        {
          
        }

        private static Expression<Func<Instructor, bool>> BuildCriteria(IInstructorSpecParams specParams)
        {
            return u =>
                string.IsNullOrEmpty(specParams.SearchName) ||
                (u.User.FirstName + u.User.LastName).ToLower().Contains(specParams.SearchName.ToLower());
        }



    }
}
