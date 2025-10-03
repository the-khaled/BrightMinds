using BrightMinds.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Core.Interfaces
{
    public interface IFeedbackRepository : IGenericRepository<Feedback>
    {

        Task<IEnumerable<Feedback>> GetFeedbacksByCourseIdAsync(int courseId);
        Task<IEnumerable<Feedback>> GetRatingFeedbacksByCourseIdAsync(int courseId );
    }
}
    

