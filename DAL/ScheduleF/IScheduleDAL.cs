using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Schedule
{
    public interface IScheduleDAL
    {
       Task<List<Models.Schedule>> GetAllSchedules();
       Task<List<Models.Schedule>> GetSchedulesBySubjectsId(List<int> subjectsId);
    }
}
