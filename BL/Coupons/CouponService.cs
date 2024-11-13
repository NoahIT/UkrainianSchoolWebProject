using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Coupons;
using DAL.Models;

namespace BL.Coupons
{
    public class CouponService : ICouponService
    {
        private readonly ICouponDAL couponDAL;

        public CouponService(ICouponDAL couponDal)
        {
            couponDAL = couponDal;
        }

        public async Task<bool> ApplyCoupon(string couponCode, int userId)
        {
            if (await couponDAL.ApplyCoupon(couponCode, userId))
            {
                await couponDAL.DeactivateAllActiveCoupons(userId, couponCode);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Coupon> CreateCouponForUser(int userId, decimal discountPercentage)
        {
            return await couponDAL.CreateCouponForUser(userId, discountPercentage);
        }

        public async Task<Coupon?> FindCoupon(string couponCode, int userId)
        {
            return await couponDAL.FindCoupon(couponCode, userId);
        }

        public async Task<Coupon?> FindActiveCoupon(int userId)
        {
            return await couponDAL.FindActiveCoupon(userId);
        }

        public async Task DeleteActiveAndUsedCoupon(int userId)
        {
            await couponDAL.DeleteActiveAndUsedCoupon(userId);
        }
    }
}
