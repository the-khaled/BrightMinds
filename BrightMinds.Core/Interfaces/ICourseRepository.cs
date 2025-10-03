using BrightMinds.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Core.Interfaces
{
     public interface ICourseRepository:IGenericRepository<Course>
    {
        Task<Course> GetByIdAsync(int coursid);
        void Update(Course course);
    }
}
