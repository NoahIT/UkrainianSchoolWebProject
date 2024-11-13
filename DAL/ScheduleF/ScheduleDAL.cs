using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Schedule;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Tls;

namespace DAL.ScheduleF
{
    public class ScheduleDAL : IScheduleDAL
    {
        private readonly PlatformDbContext context;

        public ScheduleDAL(PlatformDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Models.Schedule>> GetAllSchedules()
        {
            var schedules = await context.Schedules.ToListAsync();

            return schedules;
        }

        public async Task<List<Models.Schedule>> GetSchedulesBySubjectsId(List<int> subjectsId)
        {
            return await context.Schedules.Where(x => subjectsId.Contains(x.Teacher.SubjectId)).ToListAsync();
        }
    }
}
