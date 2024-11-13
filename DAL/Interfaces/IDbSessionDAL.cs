using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IDbSessionDAL
    {
        Task<DbSessionModel?> Get(Guid? sessionId);
        Task<DbSessionModel?> Get(int userId);
        Task Update(DbSessionModel model);


        Task Update(Guid dbSessionID, string sessionData, int userId);

        Task Extend(Guid dbSessionID);

        Task Create(DbSessionModel model);

        Task Lock(Guid sessionId);
        Task<string?> GetSessionData(int? userid);
        Task Delete(Guid sessionId);
        Task<int?> GetUserId(Guid? sessionId);
    }
}
