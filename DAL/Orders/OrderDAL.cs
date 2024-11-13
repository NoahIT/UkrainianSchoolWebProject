using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Orders
{
    public class OrderDAL : IOrderDAL
    {
        private readonly PlatformDbContext context;

        public OrderDAL(PlatformDbContext context)
        {
            this.context = context;
        }


        public async Task<int> CreateOrder(Order order)
        {
              var entry = await context.Orders.AddAsync(order);



              await context.SaveChangesAsync();

              return entry.Entity.Id;
        }

        public async Task UpdateOrderStatus(Order order)
        {
            var o = await GetOrder(order.Id);


            if (o!=null)
            {
                o.Status = order.Status;

                context.Orders.Update(o);

                await context.SaveChangesAsync();
            }
        }

        public async Task<Order?> GetOrder(int orderId)
        {
            return await context.Orders.FirstOrDefaultAsync(x => x.Id == orderId) ?? null;
        }

        public async Task DeleteOrder(int orderId)
        {
            var order = await GetOrder(orderId);

            if (order!=null)
            {
                context.Orders.Remove(order);
            }
        }

        public async Task<List<Order>?> GetOrdersByUserId(int userId)
        {
            return await context.Orders.Where(x => x.UserId == userId &&
                                                   x.Status == StatusCode.Approved &&
                                                   x.End.Date >= DateTime.UtcNow.Date)
                .ToListAsync();
        }

        public async Task<List<Order>?> GetApprovedOrders(int userId)
        {
            List<Order>? orders = await context
                .Orders
                .Where(x=>x.Status == StatusCode.Approved && x.UserId == userId)
                .ToListAsync();

            return orders;
        }

        public async Task<List<int>?> GetApprovedOrdersSubjectIds(int userId)
        {
            return await context.Orders
                .Where(x => x.Status == StatusCode.Approved && x.UserId == userId)
                .SelectMany(s=>s.Subjects.Select(x=>x.SubjectId)).ToListAsync();
        }

        public async Task<bool> IsValidSubscriptions(int userId)
        {
            var c = await context.Orders.CountAsync(x =>
                x.Status == StatusCode.Approved && x.User.Id == userId && x.End.Date > DateTime.UtcNow.Date);

            return c > 0;
        }

        public async Task<List<int>> GetSubjectsIdsByApprovedOrdersIds(List<int> ordersId)
        {
            var subjectIds = await context.OrderSubjects
                .Where(x => ordersId.Contains(x.OrderId))
                .Select(x => x.SubjectId)
                .ToListAsync();

            return subjectIds;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await context.Orders.ToListAsync();
        }

        public async Task<List<Lessons>> GetAllLessonsBySubjectId(int subjectId)
        {
            return await context.Lessons.Where(x => x.Teacher.SubjectId == subjectId).ToListAsync();
        }
    }
}
