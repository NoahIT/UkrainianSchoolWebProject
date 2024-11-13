using BL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Helpers.Interfaces;
using BL.Models;
using Newtonsoft.Json;
using Guid = System.Guid;

namespace BL.Auth
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IDbSession dbSession;
        private readonly IWebCookie web;

        public CurrentUser(IDbSession dbSession, IWebCookie web)
        {
            this.dbSession = dbSession;
            this.web = web;
        }

        public async Task<AccessModel> IsLoggedIn()
        {
            var model = new AccessModel();

            var value = web.Get(AuthConstants.SessionCookieName);

            if (string.IsNullOrEmpty(value))
            {
                web.AddSecure(AuthConstants.SessionCookieName,Guid.NewGuid().ToString(),5);
                return model;
            }


            if (Guid.TryParse(value, out Guid x))
            {
                var sessionModel = await dbSession.GetSession(x);

                if (sessionModel != null)
                {
                    var access = JsonConvert.DeserializeObject<AccessModel?>(sessionModel.SessionData);

                    return access ?? model;
                }
            }

            return model;
        }

    }
}
