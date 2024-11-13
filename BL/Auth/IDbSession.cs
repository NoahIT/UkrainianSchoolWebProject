using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Org.BouncyCastle.Bcpg;

namespace BL.Auth
{
    public interface IDbSession
    {
        Task<DbSessionModel?> GetSession(Guid sessionId);
        Task Lock(Guid sessionId);

        Task Create(Guid session, int userId);
        Task CreateOrUpdate(Guid session, int userId);

        Task Delete(Guid session);
    }
}
