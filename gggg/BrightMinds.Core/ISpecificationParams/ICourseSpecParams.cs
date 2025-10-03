using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Core.ISpecificationParams
{
    public interface ICourseSpecParams
    {
        //private const int MaxSize = 10;
        public int PageIndex { set; get; }
        public int PageSize { set; get; }

        //  public string? OrderBy { get; set; }
        public string? SearchName { get; set; }
        public string? InstructorId { get; set; }   
        public int? CategoryId { get; set; }
    }
}
