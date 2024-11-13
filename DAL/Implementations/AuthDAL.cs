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
    public class AuthDAL : IAuthDAL
    {
        private readonly PlatformDbContext _context;

        public AuthDAL(PlatformDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateUser(User model)
        {
            if (await _context.AppUsers.FirstOrDefaultAsync(x=>x.Id == model.AppUserId) == null)
            {
                _context.AppUsers.Add(model.AppUser);
            }

            _context.Users.Add(model);

            await _context.SaveChangesAsync();

            return model.Id;
        }

        public async Task<(string? Password, string Salt, int Id, string? FirstPassword)> GetUserPassword(string email)
        {

            try
            {
                var app = await _context.AppUsers.FirstOrDefaultAsync(x => x.Email == email);

                if (app == null)
                {
                    throw new Exception();
                }

                return (app.Password, app.Salt, app.Id, app.FirstPassword);
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }

        public async Task<string?> GetEmail(string email)
        {
            return (await _context.AppUsers.FirstOrDefaultAsync(x => x.Email == email))?.Email;
        }
    }
}
