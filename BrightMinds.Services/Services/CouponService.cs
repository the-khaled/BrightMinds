using BrightMinds.Core.Interfaces;
using Library.Core.Interfaces;
using BrightMinds.Core.Models;
using BrightMinds.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Core.Models;
using Library.Services.IServices;

namespace Library.Services.Services
{
    public class CouponService:ICouponService
    {
        private readonly IUnitOfWork _UnitOfWork;

        public CouponService(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public async Task<IEnumerable<Coupon>> GenerateCoupons(int count, decimal value)
        {
            return await _UnitOfWork.CouponRepository.GenerateCouponsAsync(count, value);
        }

        public async Task<bool> ValidateCoupon(string code)
        {
            var coupon = await _UnitOfWork.CouponRepository.GetByCodeAsync(code);
            if (coupon == null || coupon.IsUsed) 
                return false;

            return true;
        }

        public async Task UseCoupon(string code, string userId, int courseId)
        {
            var coupon = await _UnitOfWork.CouponRepository.GetByCodeAsync(code);
            if (coupon == null || coupon.IsUsed)
                throw new Exception("Coupon already used Before");

            var user = await _UnitOfWork.UserRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found.");

            var course = await _UnitOfWork.CourseRepository.GetByIdAsync(courseId);
            if (course == null)
                throw new Exception("Course not found.");

            coupon.IsUsed = true;

            var couponUsage = new CouponUsage
            {
                CouponId = coupon.Id,
                UserId = userId,
                CourseId = courseId
            };

            user.WaletBalance += coupon.Value;

            _UnitOfWork.CouponRepository.Update(coupon);
            _UnitOfWork.CouponUsageRepository.Add(couponUsage); // استخدام CouponUsageRepository
            _UnitOfWork.UserRepository.Update(user);
            var result = await _UnitOfWork.CompleteAsync();
            if (result <= 0)
                throw new Exception("Failed to update the coupon status.");
        }


    }
}
    