using BrightMinds.Core.Models;
using BrightMinds.Services.Dtos.CourseDtos;
using BrightMinds.Services.IServices;
using BrightMinds.Services.SpecificationParams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BrightMinds.Api.Controllers
{
    [Authorize]
    public class CourseController : ApiBaseController
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        // GET: api/<CourseController>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Get([FromQuery]CourseSpecParams specParams)
        {
            var result = await _courseService.GetAllAsync(specParams);
            if(result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        // GET api/<CourseController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]

        public async Task<ActionResult> Get(int id)
        {
            var result=await _courseService.GetAsync(id);
            if(result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        // POST api/<CourseController>
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] CourseDto course)
        {
            var result=await _courseService.CreateAsync(course);  
            if(result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        // PUT api/<CourseController>/5
        [HttpPut]
        public async Task<ActionResult>  Put( [FromForm] CourseDto courseDto)
        {
            var result = await _courseService.UpdateAsync(courseDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);


        }

        // DELETE api/<CourseController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _courseService.DeleteAsync(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
