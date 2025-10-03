using BrightMinds.Core.Interfaces;
using Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Interfaces
{
    public interface ICouponRepository: IGenericRepository<Coupon>
    { 
        Task<Coupon> GetByCodeAsync(string code);   
        Task<bool> IsCouponUsedAsync(string code);
        Task<IEnumerable<Coupon>> GenerateCouponsAsync(int count, decimal value);
    }
}
