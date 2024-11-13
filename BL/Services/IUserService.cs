using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Models;
using DAL.Models;

namespace BL.Services
{
    public interface IUserService
    {
       Task DeleteAccount(User user);
       Task RestrictAccess(int userId);

       Task<User?> GetAccount(string sessionId);
       Task<string> GetClassNumberByApprovedOrders(int userId);

       Task UpdateProfileInformation(string? fullName, string? login, string? password,string? phone, int userId);

       Task<AccessModel> GetAccessModel();

       Task<User?> GetUser();

       Task<HelloUserModel?> GetHelloUserModel();

       Task<(bool? IsValidSubscription, string? Login)> GetHelloUserLoginAndSubsValid();
       Task<List<Lessons>?> GetLessonsByValidSubscription();
       Task<bool> IsSubscription(string subject);
       Task<List<Lessons>?> GetAllLessonsByAbbr(int? klass, string abbr, int PageSize, int pageIndex);
       Task<int?> GetUserId();
       Task<string> GetFullNameAndUserName(int userId);
    }
}
