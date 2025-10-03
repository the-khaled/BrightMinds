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
     public class InstructorRepository:GenericRepository<Instructor>,IInstructorRepository
    {
        private readonly BrightMindsContext _context;

        public InstructorRepository(BrightMindsContext context):base(context)
        {
            _context = context;
        }
        public async Task<Instructor>GetByUserId(string userId)
            {
            return await _context.Instructors.Where(i => i.UserId == userId).FirstOrDefaultAsync();
        }
    }
}
