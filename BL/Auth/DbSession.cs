using BL.Helpers.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BL.Models;
using Newtonsoft.Json;

namespace BL.Auth
{
    public class DbSession : IDbSession
    {
        private readonly IDbSessionDAL sessionDAL;
        private readonly IWebCookie webCookie;

        private DbSessionModel? sessionModel = null;
        private Dictionary<string, object>? SessionData = null;

        public DbSession(IDbSessionDAL sessionDAL, IWebCookie webCookie)
        {
            this.sessionDAL = sessionDAL;
            this.webCookie = webCookie;
        }

        private void CreateSessionCookie(Guid sessionid)
        {
            webCookie.Delete(AuthConstants.SessionCookieName);
            webCookie.AddSecure(AuthConstants.SessionCookieName, sessionid.ToString());
        }

        private async Task<DbSessionModel> CreateSession()
        {
            var data = new DbSessionModel()
            {
                DbSessionId = Guid.NewGuid(),
                Created = DateTime.UtcNow,
                LastAccessed = DateTime.UtcNow
            };
            await sessionDAL.Create(data);
            return data;
        }

        public async Task<DbSessionModel?> GetSession(Guid sessionId)
        {
            return await sessionDAL.Get(sessionId);
        }

        public async Task Lock(Guid sessionId)
        {
            await sessionDAL.Lock(sessionId);
        }

        public async Task Create(Guid session, int userId)
        {
            var data = new DbSessionModel()
            {
                DbSessionId = session,
                Created = DateTime.UtcNow,
                LastAccessed = DateTime.UtcNow,
                UserId = userId
            };

            await sessionDAL.Create(data);
        }

        public async Task CreateOrUpdate(Guid session, int userId)
        {
            var sessionData = JsonConvert.SerializeObject(new AccessModel()
            {
                IsAdmin = false,
                IsLoggedIn = true,
                IsStudent = true,
                IsSubscription = false,
                IsTeacher = false
            });

            var s = await sessionDAL.Get(session);

            if (s == null)
            {
                await sessionDAL.Create(new DbSessionModel()
                {
                    DbSessionId = session,
                    UserId = userId,
                    SessionData = sessionData
                });
            }
            else
            {
                await sessionDAL.Update(session, sessionData, userId);
            }
        }

        public async Task Delete(Guid session)
        {
            await sessionDAL.Delete(session);
        }
    }
}
