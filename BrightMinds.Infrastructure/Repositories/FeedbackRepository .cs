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
    public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
    {
        private readonly BrightMindsContext _context;
        public FeedbackRepository(BrightMindsContext context) : base(context)
        {

            _context = context;
        }
        public async Task<IEnumerable<Feedback>> GetFeedbacksByCourseIdAsync(int courseId)
        {
            return await _context.Feedbacks.Where(f => f.CourseId == courseId).ToListAsync();
        }
        public async Task<IEnumerable<Feedback>> GetRatingFeedbacksByCourseIdAsync(int courseId)
        {
            return await _context.Feedbacks.Where(f => f.CourseId == courseId ).ToListAsync();
        }

       
    }
}
