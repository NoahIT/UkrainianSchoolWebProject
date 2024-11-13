using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BL.ScheduleF
{
    public interface IScheduleBL
    {
        Task<List<Schedule>> GetAllSchedules();

        Task<List<Schedule>?> GetScheduleByUserId(int userId);
    }
}
