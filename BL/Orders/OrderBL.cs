using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Subscriptions;
using DAL.Models;
using DAL.Orders;
using DAL.Subjects;
using DAL.UserManager;

namespace BL.Orders
{
    public class OrderBL : IOrder
    {
        private readonly IOrderDAL order;
        private readonly IUserDAL userDAL;
        private readonly ISubscription sub;
        private readonly ISubjectDAL subjectsDAL;

        public OrderBL(IOrderDAL order, IUserDAL userDal, ISubscription sub, ISubjectDAL subjectsDal)
        {
            this.order = order;
            userDAL = userDal;
            this.sub = sub;
            subjectsDAL = subjectsDal;
        }

        public async Task<int?> CreateOrder(string gradeClass, int userId, int subscriptionId, List<int> subjectsId)
        {
            var user = await userDAL.GetUser(userId);
            var subscription = await sub.GetSubType(subscriptionId);

            if (Enum.TryParse<GradeClass>(gradeClass, out GradeClass x))
            {
                if (user != null && subscription != null)
                {
                    var orderModel = new Order()
                    {
                        GradeClass = x,
                        UserId = userId,
                        SubscriptionTypeId = subscriptionId,
                        User = user,
                        SubscriptionType = subscription,
                        CreatedAt = DateTime.UtcNow,
                        Modified = DateTime.UtcNow,
                        isValid = false,
                        Status = StatusCode.Started,
                        Start = DateTime.UtcNow,
                        End = DateTime.UtcNow.AddMonths(subscription.Months),
                    };

                    var orderId = await order.CreateOrder(orderModel);
                    await subjectsDAL.CreateOrderSubjects(orderId, subjectsId);

                    return orderId;
                }
            }

            return null;
    }

        public async Task UpdateOrderStatus(int? orderId, StatusCode code)
        {
            if (orderId != null)
            {
                await order.UpdateOrderStatus(new Order()
                {
                    Id = (int)orderId,
                    Status = code
                });
            }
        }

        public async Task<Order?> GetOrder(int orderId)
        {
            return await order.GetOrder(orderId);
        }

        public async Task DeleteOrder(int orderId)
        {
            await order.DeleteOrder(orderId);
        }

        public async Task<List<int>?> GetApprovedOrdersSubjectIds(int userId)
        {
            return await order.GetApprovedOrdersSubjectIds(userId);
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await order.GetAllOrders();
        }
    }
}
