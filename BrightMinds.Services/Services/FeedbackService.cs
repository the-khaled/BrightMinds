using AutoMapper;
using BrightMinds.Core.Interfaces;
using BrightMinds.Core.Models;
using BrightMinds.Infrastructure.DataAccess;
using BrightMinds.Services.Dtos.FeedbackDtos;
using BrightMinds.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;
/*        private readonly IFeedbackRepository _feedbackRepository;
*/        private readonly IMapper _mapper;
        public FeedbackService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
/*            _feedbackRepository = feedbackRepository;
*/            _mapper = mapper;
        }

        public async Task<FeedbackDto> AddFeedbackAsync(FeedbackDto feedbackDto)
        {
            try
            {
                var feedback = _mapper.Map<Feedback>(feedbackDto); 

                if (feedback == null)
                {
                    throw new Exception("The feedback object is null");
                }
                if (string.IsNullOrEmpty(feedbackDto.Content))
                {
                    throw new ArgumentException("Content is required and cannot be empty.");
                }
                feedback.UserId = feedbackDto.Userid;
                feedback.CourseId = feedbackDto.CourseId;

                await _unitOfWork.FeedbackRepository.Add(feedback); 
                await _unitOfWork.CompleteAsync(); 
                return _mapper.Map<FeedbackDto>(feedback); 
            }
            catch (Exception ex)
            {                    
                Console.WriteLine("Error occurred during saving changes: " + ex.Message);
                throw;
            }
        }
       
        
        public async Task<FeedbackDto> UpdateFeedbackAsync(FeedbackDto feedbackDto)
        {
            if (feedbackDto == null)
                throw new ArgumentNullException(nameof(feedbackDto));

            var feedback = await _unitOfWork.FeedbackRepository.GetAsync(feedbackDto.Id);
            if (feedback == null)
                throw new Exception("Feedback not found.");

            _mapper.Map(feedbackDto, feedback);
            _unitOfWork.FeedbackRepository.Update(feedback);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<FeedbackDto>(feedback);
        }

        public async Task<IEnumerable<FeedbackDto>> GetFeedbacksByCourseIdAsync(int courseId)
        {
            var feedbacks = await _unitOfWork.FeedbackRepository.GetFeedbacksByCourseIdAsync(courseId);
            return _mapper.Map<IEnumerable<FeedbackDto>>(feedbacks); 
        }
        public async Task DeleteFeedbackAsync(int id)
        {
            var feedback = await _unitOfWork.FeedbackRepository.GetAsync(id);
            if (feedback == null)
                throw new Exception("Feedback not found.");

            _unitOfWork.FeedbackRepository.Delete(feedback);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<FeedbackDto>> GetRatingFeedbacksByCourseIdAsync(int courseId)
        {
            var feedbacks = await _unitOfWork.FeedbackRepository.GetRatingFeedbacksByCourseIdAsync(courseId);
            return _mapper.Map<IEnumerable<FeedbackDto>>(feedbacks); 
        }

 /*       Task<IEnumerable<Feedback>> IFeedbackService.GetFeedbacksByCourseIdAsync(int courseId)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Feedback>> IFeedbackService.GetRatingFeedbacksByCourseIdAsync(int courseId, int Rating)
        {
            throw new NotImplementedException();
        }*/
    }
}
