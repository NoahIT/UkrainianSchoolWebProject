using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BL.Coupons
{
    public interface ICouponService
    {
        Task<bool> ApplyCoupon(string couponCode, int userId);
        Task<Coupon> CreateCouponForUser(int userId, decimal discountPercentage);

        Task<Coupon?> FindCoupon(string couponCode, int userId);

        Task<Coupon?> FindActiveCoupon(int userId);
        Task DeleteActiveAndUsedCoupon(int orderUserId);
    }
}
