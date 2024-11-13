using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Auth;
using BL.Helpers.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BL.Helpers
{
    public class WebCookie : IWebCookie
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WebCookie(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor)); ;
        }


        public void AddSecure(string cookieName, string value, int days = 0)
        {
            CookieOptions options = new CookieOptions()
            {
                Path = "/",
                Secure = true,
                HttpOnly = true
            };

            if (days>0)
            {
                options.Expires = DateTimeOffset.UtcNow.AddDays(days);
            }

            _httpContextAccessor.HttpContext?.Response.Cookies.Append(cookieName, value, options);
        }

        public void Add(string cookieName, string value, int days = 0)
        {
            CookieOptions options = new CookieOptions()
            {
                Path = "/"
            };

            if (days > 0)
            {
                options.Expires = DateTimeOffset.UtcNow.AddDays(days);
            }

            _httpContextAccessor.HttpContext?.Response.Cookies.Append(cookieName,value, options);
        }

        public void Delete(string cookieName)
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete(cookieName);
        }

        public string? Get(string cookieName)
        {
            var cookieValue = _httpContextAccessor.HttpContext?.Request?.Cookies[cookieName];

            return cookieValue;
        }

        public string GetOrUpdateSession()
        {
            var session = Get(AuthConstants.SessionCookieName);

            if (session == null)
            {
                AddSecure(AuthConstants.SessionCookieName, Guid.NewGuid().ToString(), 10);
                session = Get(AuthConstants.SessionCookieName);
            }

            return session;
        }
    }
}
