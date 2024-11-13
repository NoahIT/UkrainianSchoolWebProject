using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Exceptions;
using BL.Helpers;
using BL.Helpers.Interfaces;
using BL.Models;
using DAL.Interfaces;
using DAL.Models;
using DAL.Orders;
using DAL.UserManager;
using Newtonsoft.Json;

namespace BL.Auth
{
    public class Auth : IAuth
    {
        private readonly IAuthDAL authDal;
        private readonly IEncrypt encrypt;
        private readonly IDbSession dbSession;
        private readonly IWebCookie webCookie;
        private readonly IOrderDAL orderDal;
        private readonly IUserDAL userDal;

        private readonly IDbSessionDAL dbDal;

        public Auth(IAuthDAL authDal, IEncrypt encrypt, IDbSession dbSession, IWebCookie webCookie, IDbSessionDAL dbDal, IOrderDAL orderDal, IUserDAL userDal)
        {
            this.authDal = authDal;
            this.encrypt = encrypt;
            this.dbSession = dbSession;
            this.webCookie = webCookie;
            this.dbDal = dbDal;
            this.orderDal = orderDal;
            this.userDal = userDal;
        }

        public async Task<int> CreateUser(User model)
        {
            model.AppUser.Salt = Guid.NewGuid().ToString();
            model.AppUser.Password = encrypt.HashPassword(model.AppUser.Password, model.AppUser.Salt);

            int id = await authDal.CreateUser(model);

            await Login(id);
            return id;
        }

        public async Task<int> CreateFirstUser(User model)
        {
            model.AppUser.Salt = Guid.NewGuid().ToString();
            model.AppUser.FirstPassword = model.AppUser.FirstPassword;
            model.AppUser.Login = model.AppUser.Email.Split('@')[0];

            int id = await authDal.CreateUser(model);

            return id;
        }

        private async Task CreateCookieAndCreateDbSession(int userId)
        {
            var sId = Guid.NewGuid();


            var order = await orderDal.GetOrdersByUserId(userId);

            var accessModel = await GetAccessModel(order);
            

            await dbDal.Create(new DbSessionModel()
            {
                DbSessionId = sId,
                UserId = userId,
                SessionData = JsonConvert.SerializeObject(accessModel)
            });

            webCookie.AddSecure(AuthConstants.SessionCookieName,sId.ToString(),15);
        }

        private async Task<AccessModel> GetAccessModel(List<Order>? order)
        {
            var userId = order?.First().UserId ?? 0;

            AccessModel accessModel = new AccessModel()
            {
                IsLoggedIn = true,
                IsAdmin = false,
                IsStudent = true,
                IsSubscription = false,
                IsTeacher = false
            };

            if (order == null)
            {
                return accessModel;
            }

            foreach (var o in order)
            {
                if (o.Status == StatusCode.Approved && o.End.Date >= DateTime.UtcNow.Date)
                {
                    accessModel.IsSubscription = true;
                    break;
                }
            }
            
            if (await userDal.IsTeacher(userId))
            {
                accessModel.IsStudent = false;
                accessModel.IsTeacher = true;
            }
            

            return accessModel;
        }

        public async Task Login(int id)
        {
            var session = webCookie.Get(AuthConstants.SessionCookieName);


            // достаем из Dbsession айдисесси по айди юзера
            // сетим в куки сессию из базы

            var dbModel = await dbDal.Get(id);

            if (dbModel == null)
            {
               await CreateCookieAndCreateDbSession(id);
            }
            else
            {
                webCookie.AddSecure(AuthConstants.SessionCookieName,dbModel.DbSessionId.ToString(), 15);

                var order = await orderDal.GetOrdersByUserId(id);

                var s = await GetAccessModel(order);

                dbModel.SessionData = JsonConvert.SerializeObject(s);

                await dbDal.Update(dbModel);
            }
        }

        public async Task<int> Authenticate(string email, string password)
        {
            var (passwordDb, salt, id, firstPassword) = await authDal.GetUserPassword(email);

            if (firstPassword != null)
            {
                if (firstPassword == password)
                {
                    await Login(id);

                    return id;
                }
            }
            else
            {
               if (passwordDb == encrypt.HashPassword(password, salt))
               {
                   await Login(id);

                   return id;
               }
            }


            return 0;
        }

        public async Task ValidateEmail(string email)
        {
            var user = await authDal.GetEmail(email);
            if (user != null)
                throw new DuplicateEmailException();
        }

        public void ValidatePassword(string password)
        {
            if (password.Length < 5)
                throw new EasyPasswordException();
        }

        public async Task<int> RegisterNoAuth(User user)
        {
            int id = 0;

            using (var scope = HelpersM.CreateTransactionScope())
            {
                await ValidateEmail(user.AppUser.Email);
                id = await CreateFirstUser(user);

                scope.Complete();
            }

            return id;
        }

        public async Task<int> Register(User user)
        {
            ValidatePassword(user.AppUser.Password);

            int id = 0;

            var session = webCookie.GetOrUpdateSession();


            using (var scope = HelpersM.CreateTransactionScope())
            {
                await dbSession.Lock(HelpersM.StringToGuidDef(session) ?? Guid.Empty);
                await ValidateEmail(user.AppUser.Email);
                id = await CreateUser(user);

                scope.Complete();
            }

            return id;
        }

        public async Task Logout()
        {
            var session = webCookie.GetOrUpdateSession();

            await dbSession.Delete(HelpersM.StringToGuidDef(session) ?? Guid.NewGuid());

            webCookie.Delete(AuthConstants.SessionCookieName);
        }
    }
}
