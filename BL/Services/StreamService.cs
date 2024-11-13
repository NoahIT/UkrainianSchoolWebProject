using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Streams;

namespace BL.Services
{
    public class StreamService : IStreamService
    {
        private readonly IStreamDAL streamDAL;

        public StreamService(IStreamDAL streamDal)
        {
            streamDAL = streamDal;
        }

        public async Task<StreamInformation?> GetStreamBySubjectId(int subjectId)
        {
            return await streamDAL.GetStreamBySubjectId(subjectId);
        }
    }
}
