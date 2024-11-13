using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations
{
    public class RecoveryPasswordDAL : IRecoveryPasswordDAL
    {
        private readonly PlatformDbContext context;

        public RecoveryPasswordDAL(PlatformDbContext context)
        {
            this.context = context;
        }

        public async Task Create(PasswordCodes user)
        {
           await context.PasswordCodes.AddAsync(user);

           await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var passcode = await Get(id);

           if (passcode != null)
           { 
               context.PasswordCodes.Remove(passcode);
           }

           await context.SaveChangesAsync();
        }

        public async Task Delete(Guid guid)
        {
            var p = await context.PasswordCodes
                .FirstOrDefaultAsync(x => x.VerificationCode == guid);

            if (p != null)
            {
                context.PasswordCodes.Remove(p);
                await context.SaveChangesAsync();
            }
        }

        public async Task Update(PasswordCodes user)
        { 
            context.PasswordCodes.Update(user);

            await context.SaveChangesAsync();
        }

        public async Task<PasswordCodes?> Get(int id)
        {
            return await context
                .PasswordCodes
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PasswordCodes?> Get(string email)
        {
            return await context
                .PasswordCodes
                .FirstOrDefaultAsync(x => x.User.AppUser.Email == email);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await context
                .Users
                .FirstOrDefaultAsync(x => x.AppUser.Email == email);
        }

        public async Task DeleteAll(int userId)
        {
            var codes = context.PasswordCodes.Where(x => x.User.Id == userId);

            context.PasswordCodes
                .RemoveRange(codes);

            await context.SaveChangesAsync();
        }

        public async Task<string> GetEmailByCode(Guid? code)
        {
            if (code==null)
            {
                return string.Empty;
            }

            var email = await context.PasswordCodes
                .FirstOrDefaultAsync(x => x.VerificationCode == code);

            email.User = await context.Users.FirstOrDefaultAsync(x => x.Id == email.Id) ?? null;

            if (email == null)
            {
                return string.Empty;
            }

            return email.User.AppUser.Email;
        }

        public async Task<User?> GetUserByCode(Guid? code)
        {
            if (code == null)
                return null;

            var codes = await context.PasswordCodes.FirstOrDefaultAsync(x => x.VerificationCode == code);

            var user = codes.User;

            if (user != null)
            {
                user.AppUser = await context.AppUsers.FirstOrDefaultAsync(x => x.Id == user.AppUserId);
            }

            return user;
        }

        public async Task UpdatePassword(User user)
        {
            context.Users.Update(user);

            await context.SaveChangesAsync();
        }
    }
}
