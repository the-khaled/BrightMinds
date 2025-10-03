using BrightMinds.Core.SpecificationParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.SpecificationParams
{
    public class UserSpecParams : IUserSpecParams
    {
        private const int MaxSize = 15;
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
