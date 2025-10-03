using BrightMinds.Services.Dtos.VideoDtos;
using BrightMinds.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BrightMinds.Api.Controllers
{
    [Authorize]
    public class VideoController : ApiBaseController
    {
        private readonly IVideoService _videoService;

        public VideoController(IVideoService videoService)
        {
            _videoService = videoService;
        }
        // GET: api/<VideoController>
        [HttpGet]
        public async Task<ActionResult> GetBySectionId([FromQuery]int sectionId)
        {
            var result = await _videoService.GetByIdAsync(sectionId);
            if (result.Success)
                return Ok(result);
            return NotFound(result);
        }

        // GET api/<VideoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await _videoService.GetByIdAsync(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        // POST api/<VideoController>
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] VideoDto videoDto)
        {
            var result=await _videoService.CreateAsync(videoDto); 
            if(result.Success) 
                return Ok(result);
            return BadRequest(result);
        }

        // PUT api/<VideoController>/5
        [HttpPut]
        public async Task<ActionResult> Put( [FromForm] VideoDto videoDto)
        {
            var result = await _videoService.UpdateAsync(videoDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        // DELETE api/<VideoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _videoService.DeleteAsync(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
