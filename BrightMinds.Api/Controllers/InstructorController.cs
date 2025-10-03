using BrightMinds.Services.Dtos.InstructorDtos;
using BrightMinds.Services.IServices;
using BrightMinds.Services.SpecificationParams;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BrightMinds.Api.Controllers
{
   
    public class InstructorController :ApiBaseController
    {
        private readonly IInstructorService _instructorService;

        public InstructorController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery]InstructorSpecParams specParams)
        {
            var result=await _instructorService.GetAllWithSpecAsync(specParams);
            return Ok(result);
        }

        // GET api/<InstructorController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var result= await _instructorService.GetAsync(id);
            if(result.Success)
                return Ok(result);
           return BadRequest(result);   
        }

        // POST api/<InstructorController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] InstructorDto instructorDto)
        {
            var result=await _instructorService.CreateAsync(instructorDto);  
            if(result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        // PUT api/<InstructorController>/5
        [HttpPut("")]
        public async  Task<ActionResult> Put( [FromBody] InstructorDto instructorDto)
        {
          var result=await _instructorService.UpdateAsync(instructorDto);
            if(result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        // DELETE api/<InstructorController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result=await _instructorService.DeleteAsync(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
