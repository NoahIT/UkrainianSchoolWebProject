using Microsoft.EntityFrameworkCore;
using StreamServer.Database.Models;

namespace StreamServer.Database.Services
{
    public class StreamService : IStreamService
    {
        private readonly StreamDbContext context;

        public StreamService(StreamDbContext context)
        {
            this.context = context;
        }

        public async Task StreamStarted(StreamInformation stream)
        {
            var sDb = await context.StreamInformations.FirstOrDefaultAsync(x => x.AbbrName == stream.AbbrName);
            if (sDb != null)
            {
                sDb.Identifier=stream.Identifier;
                sDb.IsLive=stream.IsLive;
                sDb.StreamPath=stream.StreamPath;
                sDb.Modified = DateTime.UtcNow;
            }

            if (sDb == null)
            {
                throw new Exception("Такой стрим не был найден");
            }

            await context.SaveChangesAsync();
        }

        public async Task StreamEnded(StreamInformation stream)
        {
            var sDb = await context.StreamInformations.FirstOrDefaultAsync(x => x.AbbrName == stream.AbbrName);

            if (sDb == null)
            {
                throw new Exception("Такой стрим не был найден");
            }

            sDb.IsLive = stream.IsLive;

            await context.SaveChangesAsync();
        }

        public async Task<StreamInformation?> GetStream(int subjectId)
        {
            return await context.StreamInformations
                .Where(x => x.SubjectId == subjectId)
                .Select(x => new StreamInformation
                {
                    Password = x.Password,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ValidateTeacherCreditials(int subjectId, string password)
        {
            var streamCreditials = await GetStream(subjectId);

            if (streamCreditials == null)
            {
                return false;
            }

            if (password == streamCreditials.Password)
            {
                return true;
            }

            return false;
        }
    }
}
