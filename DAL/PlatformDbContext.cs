using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class PlatformDbContext : DbContext
    {
        public PlatformDbContext(DbContextOptions<PlatformDbContext> options)
            :base(options) { }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DbSessionModel> DbSessions { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUserAppRole> AppUserAppRole { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }
        public DbSet<UserGroup> Groups { get; set; }
        public DbSet<PasswordCodes> PasswordCodes { get; set; }
        public DbSet<SubscriptionType> SubscriptionTypes { get; set; }
        public DbSet<Models.Schedule> Schedules { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PaymentToken> PaymentTokens { get; set; }
        public DbSet<OrderSubject> OrderSubjects { get; set; }
        public DbSet<Lessons> Lessons { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Videos> Videos { get; set; }
        public DbSet<StreamInformation> StreamInformations { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<PaymentsHistory> PaymentsHistories { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
