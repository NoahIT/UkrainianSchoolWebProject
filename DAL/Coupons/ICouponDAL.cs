using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Coupons
{
    public interface ICouponDAL
    {
        Task<Coupon> CreateCouponForUser(int userId, decimal discountPercentage);

        Task<bool> ApplyCoupon(string couponCode, int userId);

        Task DeactivateAllActiveCoupons(int userId, string couponCode);

        Task<Coupon?> FindCoupon(string couponCode, int userId);
        Task<Coupon?> FindActiveCoupon(int userId);
        Task DeleteActiveAndUsedCoupon(int userId);
    }
}
