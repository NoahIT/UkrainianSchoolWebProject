using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.UserManager
{
    public interface IUserDAL
    {
        Task<User?> GetUser(int userId);

        Task Delete(User user);

        Task<bool> IsTeacher(int userId);

        Task Update(User user);
        Task<User?> GetUserNameAndFullname(int userId);
    }
}
