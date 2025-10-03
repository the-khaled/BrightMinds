using BrightMinds.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Core.Interfaces
{
     public interface IInstructorRepository:IGenericRepository<Instructor>
    {
        public  Task<Instructor> GetByUserId(string userId);

    }
}
