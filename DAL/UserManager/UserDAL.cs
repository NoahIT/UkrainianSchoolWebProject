using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.UserManager
{
    public class UserDAL : IUserDAL
    {
        private readonly PlatformDbContext context;

        public UserDAL(PlatformDbContext context)
        {
            this.context = context;
        }

        public async Task<User?> GetUser(int userId)
        {
            return await context.Users.FirstOrDefaultAsync(x=>x.Id==userId);
        }

        public async Task Delete(User user)
        {
            var u = await GetUser(user.Id);

            if (u != null)
            {
                context.Users.Remove(u);
                context.AppUsers.Remove(u.AppUser);
            }

            await context.SaveChangesAsync();
        }

        public async Task<bool> IsTeacher(int userId)
        {
            var t = await context.Teachers.FirstOrDefaultAsync(x => x.UserId == userId);

            return t != null;
        }

        public async Task Update(User user)
        {
            context.Users.Update(user);

            await context.SaveChangesAsync();
        }

        public async Task<User?> GetUserNameAndFullname(int userId)
        {
            return await context.Users.Select(x=>new User()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                AppUser = new AppUser()
                {
                    Login = x.AppUser.Login
                }
            }).FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}
