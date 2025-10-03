using BrightMinds.Api.Controllers;
using BrightMinds.Core.Models;
using Library.Core.Interfaces;
using Library.Services.IServices;
using Library.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
/*    [Authorize]
*/    public class CouponController : ApiBaseController
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateCoupons(int courseId, int count, decimal value)
        {
            var coupons = await _couponService.GenerateCoupons(count, value);
            return Ok(coupons);
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> UseCoupon(string code, string userId, int courseId)
        {
            try
            {
              var TheUser = User.Claims
                 .FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                await _couponService.UseCoupon(code, userId, courseId);
                return Ok("Coupon used successfully...");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> ValidateCoupon(string code)
        {
            var isValid = await _couponService.ValidateCoupon(code);
            return isValid ? Ok("Coupon is valid.") : BadRequest("Invalid or expired coupon.");
        }
    }
}
/*var userId = User.Claims.
FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;*/