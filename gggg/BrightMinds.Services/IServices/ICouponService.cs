using BrightMinds.Core.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.IServices
{
    public interface ICouponService
    {
        public  Task<IEnumerable<Coupon>> GenerateCoupons(int courseId, int count, decimal value);
        public  Task<bool> ValidateCoupon(string code);
        public  Task UseCoupon(string code, int userId);


    }
}
