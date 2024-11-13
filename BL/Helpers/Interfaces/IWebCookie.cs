using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Helpers.Interfaces
{
    public interface IWebCookie
    {
        void AddSecure(string cookieName, string value, int days = 0);
        void Add(string cookieName, string value, int days = 0);
        void Delete(string cookieName);
        string? Get(string cookieName);
        string GetOrUpdateSession();
    }
}
