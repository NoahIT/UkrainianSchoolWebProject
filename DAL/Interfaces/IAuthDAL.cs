using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IAuthDAL
    {
        Task<int> CreateUser(User model);

        Task<(string? Password, string Salt, int Id, string? FirstPassword)> GetUserPassword(string email);

        Task<string?> GetEmail(string email);
    }
}
