using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BL.Coupons;
using BL.Email;
using BL.Models;
using BL.Services;
using DAL.Models;
using DAL.Subscriptions.Payments;

namespace BL.Orders
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentDAL paymentDAL;
        private readonly IOrder orderService;
        private readonly ISignatureGenerator gen;
        private readonly INotificationService notificationService;
        private readonly IUserService userService;
        private readonly ICouponService couponService;

        public PaymentService(IPaymentDAL paymentDal, IOrder order, ISignatureGenerator gen, INotificationService notificationService, ICouponService couponService, IUserService userService)
        {
            this.paymentDAL = paymentDal;
            this.orderService = order;
            this.gen = gen;
            this.notificationService = notificationService;
            this.couponService = couponService;
            this.userService = userService;
        }

        public async Task<ServiceUrlModel?> ChangeOrderStatus(WayforpayResponse response)
        {
            if (int.TryParse(response.OrderReference, out int orderId))
            {
                var order = await orderService.GetOrder(orderId);

                if (order == null)
                {
                    Console.WriteLine($"PaymentService gets null(not found order) Order id: {response.OrderReference}");

                    return null;
                }

                try
                {
                    if (Enum.TryParse<StatusCode>(response.TransactionStatus, out StatusCode status))
                    {
                        bool isFirstSuccessfulPayment = order.Status != StatusCode.Approved
                                                        && status == StatusCode.Approved
                                                        && order.Start.Date == DateTime.UtcNow.Date;

                        if (response.TransactionStatus != "Refunded") // ПОСЛЕ ТЕСТОВОГО РЕЖИМА ЭТО НУЖНО УБРАТЬ  УБРАТЬ УБРАТЬ УБРАТЬ УБРАТЬ
                        {
                            await orderService.UpdateOrderStatus(order.Id, status);

                            switch (status)
                            {
                                case StatusCode.Approved:
                                    if (isFirstSuccessfulPayment)
                                    {
                                        await notificationService.SendEmailNoReply(new MailMessageCustom(
                                            "Поздравляем!!! С оформлением подписки",
                                            $"Для подключения к нашей системе, используйте свою почту и данный первый пароль(измените его в при входе): {order.User.AppUser.FirstPassword}",
                                            order.User.AppUser.Email));
                                    }
                                    else
                                    {
                                        await notificationService.SendEmailNoReply(new MailMessageCustom(
                                            "Ваша подписка была успешно продленна!",
                                            $"Спасибо!!!!!!!!!!!!!!!!!!",
                                            order.User.AppUser.Email));
                                    }
                                    break;
                                case StatusCode.Declined:
                                case StatusCode.Expired:
                                case StatusCode.Refunded:
                                case StatusCode.Voided:
                                    if ((order.Start.Date == DateTime.UtcNow.Date && order.User.CreatedAt.Date == DateTime.UtcNow.Date) 
                                        || (order.Start.Date.AddHours(4) == DateTime.UtcNow.Date.AddHours(4) 
                                         && order.User.CreatedAt.Date.AddHours(4) == DateTime.UtcNow.Date.AddHours(4))
                                        )
                                    {
                                        await userService.DeleteAccount(order.User);
                                    }
                                    else
                                    {
                                        await userService.RestrictAccess(order.User.Id);

                                        await notificationService.SendEmailNoReply(new MailMessageCustom(
                                            "Ваша подписка была выключена!",
                                            $"Из за того что вы не оплатили подписку, подписка была выключена",
                                            order.User.AppUser.Email));
                                    }
                                    break;
                            }


                            Console.WriteLine($"PaymentService: changed order status tp {response.TransactionStatus}. Order id: {response.OrderReference}");
                        }
                    }

                    var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
                    var signature = gen.GenerateSignature(response.OrderReference, "accept", time);

                    var confirmation = new ServiceUrlModel()
                    {
                        orderReference = response.OrderReference,
                        status = "accept",
                        time = time,
                        signature = signature
                    };

                    return confirmation;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine($"PaymentService: try catch block {response.TransactionStatus}. Order id: {response.OrderReference}");

                    return null;
                }

            }
            Console.WriteLine($"PaymentService gets null in converting into int value. Order id: {response.OrderReference}");


            return null;
        }

        public async Task<PaymentModel?> CreatePaymentModel(int orderId)
        {
            var order = await orderService.GetOrder(orderId);

            if (order == null)
            {
                return null;
            }

            var coupon = await couponService.FindActiveCoupon(order.UserId);
            string price = string.Empty;
            string not = string.Empty;

            if (coupon == null)
            {
                price = order.SubscriptionType.Price.ToString("F2").Replace(",", ".");
            }
            else
            {
                price = ((order.SubscriptionType.Price / 100) * (100 - coupon.DiscountPercentage)).ToString("F2").Replace(",", ".");
                not = $"-{coupon.DiscountPercentage}% discount";
            }

            var orderDate = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds().ToString();
            var productName = $"Subscription for {order.SubscriptionType.Months} month(s), Number of lessons: {order.SubscriptionType.LessonsCount}. {not}";

            var orderIdS = order.Id.ToString();

            var signature = gen.GenerateSignature(
                "uniqum_school",
                "www.uniqum.school",
                orderIdS,
                orderDate,
                price,
                "UAH",
                productName,
                "1",
                price 
            );

            await couponService.DeleteActiveAndUsedCoupon(order.UserId);

            return new PaymentModel()
            {
                Order = order,
                Signature = signature,
                OrderDate = orderDate,
                Productname = productName,
                OrderId = orderIdS,
                Price = price,
                Start = order.Start.ToString("dd.MM.yyyy"),
                NextPay = order.Start.AddMonths(1).ToString("dd.MM.yyyy"),
                End = order.End.ToString("dd.MM.yyyy")
            };
        }

        public async Task SavePaymentInHistory(WayforpayResponse payment)
        {
            var paymentHistory = new PaymentsHistory()
            {
                MerchantAccount = payment.MerchantAccount,
                OrderReference = payment.OrderReference,
                MerchantSignature = payment.MerchantSignature,
                Amount = payment.Amount,
                Currency = payment.Currency,
                AuthCode = payment.AuthCode,
                Email = payment.Email,
                Phone = payment.Phone,
                CreatedDate = payment.CreatedDate,
                ProcessingDate = payment.ProcessingDate,
                CardPan = payment.CardPan,
                CardType = payment.CardType,
                IssuerBankCountry = payment.IssuerBankCountry,
                IssuerBankName = payment.IssuerBankName,
                RecToken = payment.RecToken,
                TransactionStatus = payment.TransactionStatus,
                Reason = payment.Reason,
                ReasonCode = payment.ReasonCode,
                Fee = payment.Fee,
                PaymentSystem = payment.PaymentSystem,
                AcquirerBankName = payment.AcquirerBankName,
                CardProduct = payment.CardProduct,
                ClientName = payment.ClientName
            };

            await paymentDAL.SavePaymentHistory(paymentHistory);
        }
    }
}
