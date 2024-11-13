using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IRecoveryPasswordDAL
    {
        Task Create(PasswordCodes user);
        Task Delete(int id);
        Task Delete(Guid guid);
        Task Update(PasswordCodes user);
        Task<PasswordCodes?> Get(int id);
        Task<PasswordCodes?> Get(string email);

        Task<User?> GetByEmail(string email);

        Task DeleteAll(int userId);

        Task<string> GetEmailByCode(Guid? code);

        Task<User?> GetUserByCode(Guid? code);

        Task UpdatePassword(User user);
    }
}
