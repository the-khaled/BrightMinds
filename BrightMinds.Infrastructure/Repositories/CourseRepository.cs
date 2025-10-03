using BrightMinds.Core.Interfaces;
using BrightMinds.Core.Models;
using BrightMinds.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Infrastructure.Repositories
{
     public class CourseRepository:GenericRepository<Course>,ICourseRepository
    {
        private readonly BrightMindsContext _context;
        public CourseRepository(BrightMindsContext context):base(context)
        {
            _context = context;
        }
        public async Task<Course> GetByIdAsync(int coursid)
        {
            return await _context.Courses.FirstOrDefaultAsync(u => u.Id == coursid);
        }
    }
}
