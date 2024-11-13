using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Coupons
{
    public class CouponDAL : ICouponDAL
    {
        private readonly PlatformDbContext context;

        public CouponDAL(PlatformDbContext context)
        {
            this.context = context;
        }

        private string GenerateCouponCode()
        {
            return Guid.NewGuid().ToString().Substring(0, 8);
        }

        public async Task<Coupon> CreateCouponForUser(int userId, decimal discountPercentage)
        {
            var coupon = new Coupon
            {
                Code = GenerateCouponCode(),
                DiscountPercentage = discountPercentage,
                IsUsed = false,
                UserId = userId,
            };

            await context.Coupons.AddAsync(coupon);
            await context.SaveChangesAsync();

            return coupon;
        }

        public async Task<bool> ApplyCoupon(string couponCode, int userId)
        {
            var coupon = await context.Coupons
                .FirstOrDefaultAsync(c => c.Code == couponCode && c.UserId == userId);

            if (coupon == null)
            {
                return false; 
            }

            if (coupon.IsUsed)
            {
                return false; 
            }

            coupon.IsUsed = true;
            coupon.IsActive = true;
            await context.SaveChangesAsync();

            return true;
        }

        public async Task DeactivateAllActiveCoupons(int userId, string couponCode)
        {
            var coupons = await context.Coupons
                .Where(x => x.UserId == userId && x.Code != couponCode)
                .ToListAsync();

            foreach (var coupon in coupons)
            {
                coupon.IsActive = false;
                coupon.IsUsed = false;
            }

            await context.SaveChangesAsync();
        }

        public async Task<Coupon?> FindCoupon(string couponCode, int userId)
        {
            return await context.Coupons
                .FirstOrDefaultAsync(c => c.Code == couponCode && c.UserId == userId);
        }

        public async Task<Coupon?> FindActiveCoupon(int userId)
        {
            return await context.Coupons
                .FirstOrDefaultAsync(x => x.IsActive && x.UserId == userId);
        }

        public async Task DeleteActiveAndUsedCoupon(int userId)
        {
            var couponsToDelete = await context.Coupons
                .Where(c => c.UserId == userId && (c.IsActive && c.IsUsed))
                .ToListAsync();

            context.Coupons.RemoveRange(couponsToDelete);

            await context.SaveChangesAsync();
        }
    }
}
