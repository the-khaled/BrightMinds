using BrightMinds.Core.ISpecificationParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.SpecificationParams
{
     public class InstructorSpecParams:IInstructorSpecParams
    {
        private const int MaxSize = 10;
        private int pagesize = 5;
        public int PageIndex { set; get; } = 1;
        public int PageSize
        {
            get { return pagesize; }
            set { pagesize = value > MaxSize ? MaxSize : value; }
        }



        public string? SearchName { get; set; }
    }
}
