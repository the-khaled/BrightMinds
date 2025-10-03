using BrightMinds.Core.Interfaces;
using BrightMinds.Services.IServices;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrightMinds.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpPost("GenerateCoupons")]
        public async Task<IActionResult> GenerateCoupons(int courseId, int count, decimal value)
        {
            var coupons = await _couponService.GenerateCoupons(courseId, count, value);
            return Ok(coupons);
        }

        [HttpPost("UseCoupon/{code}")]
        public async Task<IActionResult> UseCoupon(string code, int userId)
        {
            try
            {
                await _couponService.UseCoupon(code, userId);
                return Ok("Coupon used successfully...");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ValidateCoupon/{code}")]
        public async Task<IActionResult> ValidateCoupon(string code)
        {
            var isValid = await _couponService.ValidateCoupon(code);
            return isValid ? Ok("Coupon is valid.") : BadRequest("Invalid or expired coupon.");
        }
    }
}
