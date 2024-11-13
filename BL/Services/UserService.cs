using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Auth;
using BL.Helpers;
using BL.Helpers.Interfaces;
using BL.Models;
using DAL.Interfaces;
using DAL.Models;
using DAL.Orders;
using DAL.UserManager;
using Newtonsoft.Json;
using ZstdSharp.Unsafe;

namespace BL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDAL userDAL;
        private readonly IDbSessionDAL dbSession;
        private readonly IOrderDAL orderDAL;
        private readonly IEncrypt encrypt;
        private readonly IWebCookie web;
        private readonly ILessonsDAL lessons;

        public UserService(IUserDAL userDal, IDbSessionDAL dbSession, IOrderDAL orderDal, IEncrypt encrypt, IWebCookie web, ILessonsDAL lessons)
        {
            userDAL = userDal;
            this.dbSession = dbSession;
            orderDAL = orderDal;
            this.encrypt = encrypt;
            this.web = web;
            this.lessons = lessons;
        }

        public async Task DeleteAccount(User user)
        { 
            await userDAL.Delete(user);
        }

        public async Task RestrictAccess(int userId)
        {
            var dbModel = await dbSession.Get(userId);


            if (dbModel != null)
            {
                var sessionData = JsonConvert.DeserializeObject<AccessModel>(dbModel.SessionData);

                if (sessionData != null)
                {
                    sessionData.IsSubscription = false;
                }

                dbModel.SessionData = JsonConvert.SerializeObject(sessionData);

                await dbSession.Update(dbModel);
            }
        }

        public async Task<User?> GetAccount(string sessionId)
        {
            var s = await dbSession.Get(HelpersM.StringToGuidDef(sessionId) ?? Guid.Empty);

            return s?.User;
        }

        public async Task<string> GetClassNumberByApprovedOrders(int userId)
        {
            var orders = await orderDAL.GetOrdersByUserId(userId);

            if (orders == null)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();

            foreach (var order in orders)
            {
                sb.Append(order.GradeClass);
            }

            return sb.ToString();
        }

        public async Task UpdateProfileInformation(string? fullName, string? login, string? password,string? phone, int userId)
        {
            var user = await userDAL.GetUser(userId);

            if (user != null)
            {
                if (fullName != null)
                {
                    var names = fullName.Split(' ');

                    if (names.Length >= 1)
                    {
                        user.FirstName = names[0];
                    }

                    if (names.Length > 1)
                    {
                        user.LastName = names[1];
                    }
                }

                if (login != null)
                {
                    user.AppUser.Login = login;
                }

                if (password != null)
                {
                    user.AppUser.Password = encrypt.HashPassword(password, user.AppUser.Salt);
                    user.AppUser.FirstPassword = null;
                }

                if (phone != null)
                {
                    user.AppUser.Phone = phone;
                }

                await userDAL.Update(user);
            }
        }

        private async Task<DbSessionModel?> GetDbModel()
        {
            var sessionId = web.Get(AuthConstants.SessionCookieName);

            return await dbSession.Get(HelpersM.StringToGuidDef(sessionId));
        }

        public async Task<AccessModel> GetAccessModel()
        {
            var dbModel = await GetDbModel();

            var accessModel = new AccessModel()
            {
                IsLoggedIn = false,
                IsAdmin = false,
                IsStudent = false,
                IsSubscription = false,
                IsTeacher = false
            };

            if (dbModel == null)
                return accessModel;

            var orders = await orderDAL.GetOrdersByUserId((int)dbModel.UserId);

            if (orders != null)
            {
                accessModel.IsStudent = true;

                if(orders.Count > 0)
                    accessModel.IsSubscription = true;
            }

            var sessionData = JsonConvert.DeserializeObject<AccessModel>(dbModel.SessionData);

            if (sessionData.IsTeacher)
            {
                accessModel.IsTeacher = true;
            }

            if (sessionData.IsAdmin)
            {
                accessModel.IsAdmin = true;
            }


            accessModel.IsLoggedIn = true;

            return accessModel;
        }

        public async Task<User?> GetUser()
        {
            return (await GetDbModel())?.User;
        }


        public async Task<int?> GetUserId()
        {
            var sessionId = web.Get(AuthConstants.SessionCookieName);
            var id = await dbSession.GetUserId(HelpersM.StringToGuidDef(sessionId));

            return id;
        }

        public async Task<string> GetFullNameAndUserName(int userId)
        {
            var user = await userDAL.GetUserNameAndFullname(userId);

            if (user == null)
            {
                return "default";
            }

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(user.FirstName + " ");
            stringBuilder.Append(user.LastName + " ");
            stringBuilder.Append($"({user.AppUser.Login})");

            return stringBuilder.ToString();
        }

        public async Task<HelloUserModel?> GetHelloUserModel()
        {
            var dbModel = await GetDbModel();

            if (dbModel==null)
                return null;
            

            var helloModel = new HelloUserModel();

            var approvedOrders = await orderDAL.GetApprovedOrders(dbModel.User.Id);

            helloModel.ApprovedOrders = approvedOrders;
            helloModel.Login = dbModel.User.AppUser.Login;

            return helloModel;
        }

        public async Task<(bool? IsValidSubscription, string? Login)> GetHelloUserLoginAndSubsValid()
        {
            var dbModel = await GetDbModel();


            if (dbModel==null)
            {
                return (false, "");
            }


            string login = dbModel.User.AppUser.Login ?? "";

            bool isValidSubscription = await orderDAL.IsValidSubscriptions(dbModel.User.Id);

            return (isValidSubscription, login);
        }

        public async Task<List<Lessons>?> GetLessonsByValidSubscription()
        {
            var userId = await GetUserId();

            if (userId == null)
            {
                return null;
            }

            var approvedOrders = await orderDAL.GetApprovedOrders((int)userId);

            if (approvedOrders == null)
            {
                return null;
            }

            List<Lessons> lessons = new ();

            foreach (var item in approvedOrders)
            {
                foreach (var item1 in item.Subjects)
                {
                    lessons.AddRange(await orderDAL.GetAllLessonsBySubjectId(item1.Subject.Id));
                }
            }

            return lessons.OrderBy(x=>x.DateOfLesson.Date).ToList();
        }

        public async Task<bool> IsSubscription(string subject)
        {
            var userId = await GetUserId();

            if (userId == null) 
                return false;

            var orders = await orderDAL.GetApprovedOrders((int)userId);

            if (orders == null)
                return false;

            foreach (var item in orders)
            {
                foreach (var subjectf in item.Subjects)
                {
                    if (subjectf.Subject.AbbrSubject == subject)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public async Task<List<Lessons>?> GetAllLessonsByAbbr(int? klass, string abbr, int PageSize, int pageIndex)
        {
            if (klass == null)
            {
                return await lessons.GetAllLessonsByAbbr(abbr, PageSize, pageIndex);    
            }

            return await lessons.GetAllLessonsByAbbr((int)klass,abbr, PageSize, pageIndex);
        }

    }
}
