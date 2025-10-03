
using BrightMinds.Core.Interfaces;
using BrightMinds.Core.Models;
using BrightMinds.Services.IServices;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.Services
{
    public class CouponService:ICouponService
    {
        private readonly IUnitOfWork _UnitOfWork;

        public CouponService(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public async Task<IEnumerable<Coupon>> GenerateCoupons(int courseId, int count, decimal value)
        {
            return await _UnitOfWork.CouponRepository.GenerateCouponsAsync(courseId, count, value);
        }

        public async Task<bool> ValidateCoupon(string code)
        {
            var coupon = await _UnitOfWork.CouponRepository.GetByCodeAsync(code);
            if (coupon == null || coupon.IsUsed || coupon.Course == null) 
                return false;

            return true;
        }
        public async Task UseCoupon(string code, int userId)
        {
            var coupon = await _UnitOfWork.CouponRepository.GetByCodeAsync(code);
            if (coupon == null || coupon.IsUsed)
                throw new Exception("coupon already used Before");

            coupon.IsUsed = true;
            coupon.UserId = userId;
            _UnitOfWork.CouponRepository.Update(coupon);
            var result=await _UnitOfWork.CompleteAsync();
            if (result <= 0)
                throw new Exception("Failed to update the coupon status.");
        }
    }
}
    