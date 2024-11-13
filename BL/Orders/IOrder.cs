using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BL.Orders
{
    public interface IOrder
    {
        Task<int?> CreateOrder(string GradeClass, int userId, int subscriptionId, List<int> subjectsId);

        Task UpdateOrderStatus(int? orderId, StatusCode code);

        Task<Order?> GetOrder(int orderId);

        Task DeleteOrder(int orderId);

        Task<List<int>?> GetApprovedOrdersSubjectIds(int userId);

        Task<IEnumerable<Order>> GetAllOrders();

    }
}
