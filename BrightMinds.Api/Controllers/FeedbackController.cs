using BrightMinds.Core.Models;
using BrightMinds.Services.Dtos.FeedbackDtos;
using BrightMinds.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrightMinds.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {


        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost]
        public async Task<ActionResult<FeedbackDto>> AddFeedback([FromBody] FeedbackDto feedbackDto)
        {
            if (ModelState.IsValid)
            {
                var feedback = await _feedbackService.AddFeedbackAsync(feedbackDto);
                return Ok(feedback);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<ActionResult<FeedbackDto>> UpdateFeedback([FromBody] FeedbackDto feedbackDto)
        {
            var feedback = await _feedbackService.UpdateFeedbackAsync(feedbackDto);
            return Ok(feedback);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            await _feedbackService.DeleteFeedbackAsync(id);
            return Ok();
        }

        [HttpGet("{courseId}")]
        public async Task<ActionResult<IEnumerable<FeedbackDto>>> GetFeedbacksByCourse(int courseId)
        {
            var feedbacks = await _feedbackService.GetFeedbacksByCourseIdAsync(courseId);
            return Ok(feedbacks);
        }

        [HttpGet("Rate/{courseId}")]
        public async Task<IActionResult> GetRatingFeedbacksByCourseId(int courseId)
        {
            var feedbacks = await _feedbackService.GetRatingFeedbacksByCourseIdAsync(courseId);
            return Ok(feedbacks);

            /*      private readonly IFeedbackService _feedbackService;

                  public FeedbackController(IFeedbackService feedbackService)
                  {
                      _feedbackService = feedbackService;
                  }

                  [HttpPost("AddFeedback")]
                  public async Task<IActionResult> AddFeedback(string userId, int courseId, string content, int rating)
                  {
                      await _feedbackService.AddFeedbackAsync(userId, courseId, content, rating);
                      return Ok(new { message = "Feedback added successfully" });
                  }

                  [HttpGet("GetFeedbacksByCourseId/{courseId}")]
                  public async Task<IActionResult> GetFeedbacksByCourseId(int courseId)
                  {
                      var feedbacks = await _feedbackService.GetFeedbacksByCourseIdAsync(courseId);
                      return Ok(feedbacks);
                  }

                  [HttpGet("course/{courseId}/five-stars")]
                  public async Task<IActionResult> GetRatingFeedbacksByCourseId(int courseId)
                  {

                      var feedbacks = await _feedbackService.GetRatingFeedbacksByCourseIdAsync(courseId);
                      return Ok(feedbacks);
                  }*/
        }
    }
}
