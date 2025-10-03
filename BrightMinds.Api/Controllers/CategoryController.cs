using BrightMinds.Core.Models;
using BrightMinds.Services.Dtos.CategoryDtos;
using BrightMinds.Services.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BrightMinds.Api.Controllers
{
    public class CategoryController : ApiBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
          var result=await _categoryService.GetAllAsync();
            if(result.Success)
                return Ok(result);
            return BadRequest();
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var result=await _categoryService.GetByIdAsync(id);
            if(result.Success)  
                return Ok(result);
            return NotFound(result);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDto categoryDto)
        {
            var result = await _categoryService.CreateAsync(categoryDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDto categoryDto)
        {
            var result = await _categoryService.UpdateAsync(id, categoryDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result=await _categoryService.DeleteAsync(id);
            if(result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
