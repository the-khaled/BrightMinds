using BrightMinds.Core.Models;
using BrightMinds.Services.Dtos.FeedbackDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.IServices
{
    public interface IFeedbackService
    {
        Task<FeedbackDto> AddFeedbackAsync(FeedbackDto feedbackDto);
        Task<FeedbackDto> UpdateFeedbackAsync(FeedbackDto feedbackDto);
        Task DeleteFeedbackAsync(int id);
        Task<IEnumerable<FeedbackDto>> GetFeedbacksByCourseIdAsync(int courseId);
        Task<IEnumerable<FeedbackDto>> GetRatingFeedbacksByCourseIdAsync(int courseId);

        /*  Task AddFeedbackAsync(string userId, int courseId, string content, int rating);

         */
    }
}
