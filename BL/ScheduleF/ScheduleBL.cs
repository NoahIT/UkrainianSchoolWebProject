using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Orders;
using BL.Services;
using DAL.Models;
using DAL.Orders;
using DAL.Schedule;

namespace BL.ScheduleF
{
    public class ScheduleBL : IScheduleBL
    {
        private readonly IScheduleDAL schedule;
        private readonly IOrderDAL orderDal;

        public ScheduleBL(IScheduleDAL schedule, IOrderDAL orderDal)
        {
            this.schedule = schedule;
            this.orderDal = orderDal;
        }

        public async Task<List<Schedule>> GetAllSchedules()
        {
            return await schedule.GetAllSchedules();
        }

        public async Task<List<Schedule>?> GetScheduleByUserId(int userId)
        {
            var approvedOrders = await orderDal.GetApprovedOrders(userId);

            if (approvedOrders != null)
            {
                var subjectsId = await orderDal.GetSubjectsIdsByApprovedOrdersIds(approvedOrders.Select(x => x.Id).ToList());

                var schedules = await schedule.GetSchedulesBySubjectsId(subjectsId);

                return schedules;
            }

            return new List<Schedule>();
        }
    }
}
