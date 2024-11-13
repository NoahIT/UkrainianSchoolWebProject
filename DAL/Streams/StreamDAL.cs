using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Streams
{
    public class StreamDAL : IStreamDAL
    {
        private readonly PlatformDbContext context;

        public StreamDAL(PlatformDbContext context)
        {
            this.context = context;
        }

        public async Task<StreamInformation?> GetStreamBySubjectId(int subjectId)
        {
            return await context.StreamInformations.FirstOrDefaultAsync(x => x.SubjectId == subjectId);
        }
    }
}
