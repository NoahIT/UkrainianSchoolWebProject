using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Orders
{
    public interface IOrderDAL
    {
        Task<int> CreateOrder(Order order);

        Task UpdateOrderStatus(Order order);

        Task<Order?> GetOrder(int orderId);

        Task DeleteOrder(int orderId);

        Task<List<Order>?> GetOrdersByUserId(int userId);

        Task<List<Order>?> GetApprovedOrders(int userId);
        Task<List<int>?> GetApprovedOrdersSubjectIds(int userId);
        Task<List<Lessons>> GetAllLessonsBySubjectId(int subjectId);
        Task<bool> IsValidSubscriptions(int userId);
        Task<List<int>> GetSubjectsIdsByApprovedOrdersIds(List<int> ordersId);

        Task<IEnumerable<Order>> GetAllOrders();
    }
}
