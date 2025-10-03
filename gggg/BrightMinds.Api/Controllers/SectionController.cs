using BrightMinds.Services.Dtos.SectionDtos;
using BrightMinds.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BrightMinds.Api.Controllers
{
    [Authorize]
    public class SectionController : ApiBaseController
    {
        private readonly ISectionService _sectionService;

        public SectionController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }
        // GET: api/<SectionController>
        [HttpGet("{courseId}")]
        public async Task<ActionResult> GetByCourseId(int courseId)
        {
            var result=await _sectionService.GetByCourseId(courseId);
            if (result.Success)
                return Ok(result);
            return NotFound(result);
        }

        // GET api/<SectionController>/5
        [HttpGet]
        public async Task<ActionResult> GetById([FromQuery] int id)
        {
            var result=await _sectionService.GetAsync(id);
            if (result.Success)
                return Ok(result);
            return NotFound(result);
        }

        // POST api/<SectionController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]SectionDto sectionDto)
        {
            var result=await _sectionService.CreateAsync(sectionDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        // PUT api/<SectionController>/5
        [HttpPut]
        public async Task<ActionResult>  Put( [FromBody]SectionDto sectionDto)
        {
            var result=await _sectionService.UpdateAsync(sectionDto); 
            if(result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        // DELETE api/<SectionController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result=await _sectionService.DeleteAsync(id);
            if(result.Success)
               return Ok(result);
            return BadRequest(result);
        }
    }
}
