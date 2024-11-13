using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BL.Auth
{
    public interface IAuth
    {
        Task<int> CreateUser(User model);
        Task<int> Authenticate(string email, string password);
        Task ValidateEmail(string email);
        Task<int> Register(User user);
        Task<int> RegisterNoAuth(User user);
        Task<int> CreateFirstUser(User model);
        Task Logout();
    }
}
