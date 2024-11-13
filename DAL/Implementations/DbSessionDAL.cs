using DAL.Models;
using DAL;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;

public class DbSessionDAL : IDbSessionDAL
{
    private readonly PlatformDbContext _context;

    public DbSessionDAL(PlatformDbContext context)
    {
        _context = context;
    }

    public async Task Create(DbSessionModel model)
    {
        _context.DbSessions.Add(model);
        await _context.SaveChangesAsync();
    }

    public async Task<DbSessionModel?> Get(Guid? sessionId)
    {
        if (sessionId == null)
        {
            return null;
        }

        return await _context.DbSessions
            .Where(s => s.DbSessionId == sessionId)
            .FirstOrDefaultAsync();
    }

    public async Task<DbSessionModel?> Get(int userId)
    {
        return await _context.DbSessions
            .Where(s => s.UserId == userId)
            .FirstOrDefaultAsync();
    }

    public async Task Update(DbSessionModel model)
    {
       var m = await Get(model.DbSessionId);

       if (m != null)
       {
           model.LastAccessed = DateTime.UtcNow;
           
           _context.DbSessions.Update(model);
       }
    }

    public async Task Lock(Guid sessionId)
    {
        var session = await _context.DbSessions
            .Where(s => s.DbSessionId == sessionId)
            .FirstOrDefaultAsync();

        if (session != null)
        {
            // Simulating lock by fetching with an exclusive lock
            _context.Entry(session).State = EntityState.Modified;
        }
    }

    public async Task Update(Guid dbSessionID, string sessionData, int userId)
    {
        var session = await _context.DbSessions
            .Where(s => s.UserId == userId)
            .FirstOrDefaultAsync();

        if (session != null)
        {
            session.SessionData = sessionData;
            session.DbSessionId = dbSessionID;
            await _context.SaveChangesAsync();
        }
    }

    public async Task Extend(Guid dbSessionID)
    {
        var session = await _context.DbSessions
            .Where(s => s.DbSessionId == dbSessionID)
            .FirstOrDefaultAsync();

        if (session != null)
        {
            session.LastAccessed = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<string?> GetSessionData(int? userid)
    {
        if (userid == null)
            return null;

        return await _context.DbSessions
            .Where(s => s.UserId == userid && s.SessionData != "{}" && s.SessionData != "{}")
            .Select(s => s.SessionData)
            .FirstOrDefaultAsync();
    }

    public async Task Delete(Guid sessionId)
    {
        var session = await _context.DbSessions
            .Where(s => s.DbSessionId == sessionId)
            .FirstOrDefaultAsync();

        if (session != null)
        {
            _context.DbSessions.Remove(session);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int?> GetUserId(Guid? sessionId)
    {
        if (sessionId == null)
        {
            return null;
        }

        var session = await _context.DbSessions
            .Where(x => x.DbSessionId == sessionId)
            .Select(x => x.UserId)
            .FirstOrDefaultAsync();

        return session;
    }
}
