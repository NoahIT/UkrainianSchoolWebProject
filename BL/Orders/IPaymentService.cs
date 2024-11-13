using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Models;
using DAL.Models;

namespace BL.Orders
{
    public interface IPaymentService
    {
        Task<ServiceUrlModel?> ChangeOrderStatus(WayforpayResponse response);
        Task<PaymentModel?> CreatePaymentModel(int orderId);

        Task SavePaymentInHistory(WayforpayResponse payment);
    }
}
