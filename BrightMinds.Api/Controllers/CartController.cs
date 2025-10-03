using BrightMinds.Api.Response;
using BrightMinds.Core.Models;
using BrightMinds.Services.Dtos;

using BrightMinds.Services.Dtos.CartCdos;
using BrightMinds.Services.IServices;
using BrightMinds.Services.ServiceResponse.Cart;
using BrightMinds.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BrightMinds.Api.Controllers
{
    [Authorize]
    public class CartController : ApiBaseController
    {
        private readonly ICartService _cartService;
        private readonly UserManager<AppUser> _userManager;

        public CartController(ICartService cartService,
            UserManager<AppUser> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
        }

        // GET: api/<CartController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var userId = await _userManager.Users.Where(u => u.Email == email).Select(u => u.Id).FirstOrDefaultAsync();
            var result = await _cartService.GetCartItemsAsync(userId);
            if (result.Success)
                return Ok(new {StatusCode=result.StatusCode,Message=result.Message,Data=result.Data });
            else return NotFound(new ApiResponse(result.StatusCode, result.Message));
        }

        // GET api/<CartController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CartController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CartItemDto cartItem)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var userId = await _userManager.Users.Where(u => u.Email == email).Select(u => u.Id).FirstOrDefaultAsync();
            var result = await _cartService.AddToCartAsync(cartItem, userId);
            if (result.Success)
                return Ok(new ApiResponse(result.StatusCode, result.Message));
            else return BadRequest(new ApiResponse(result.StatusCode));
        }

        // PUT api/<CartController>/5
        //[HttpPut("{id}")]
        //public async Task<ActionResult> Put(int id, [FromBody] int quantity)
        //{
        //  var result=await _cartService.UpdateCartItemQuantity(id, quantity);
        //    if (result.Success)
        //        return Ok(new ApiResponse(result.StatusCode, result.Message));
        //    else return BadRequest(new ApiResponse(result.StatusCode));
        //}

        // DELETE api/<CartController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _cartService.DeleteCartItemAsync(id);
            if (result.Success)
                return Ok(new ApiResponse(result.StatusCode, result.Message));
            else return BadRequest(new ApiResponse(result.StatusCode));

        }
        [HttpDelete("clear/{id}")]
        public async Task<ActionResult> Clear(int id)
        {
            var result = await _cartService.ClearCartAsync(id);
            if (result.Success)
                return Ok(new ApiResponse(result.StatusCode, result.Message));
            else return BadRequest(new ApiResponse(result.StatusCode));

        }

    }
}
