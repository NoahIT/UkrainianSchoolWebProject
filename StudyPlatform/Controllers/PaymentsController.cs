using BL.Coupons;
using BL.Models;
using BL.Orders;
using DAL.Subscriptions.Payments;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudyPlatform.ViewModels;

namespace StudyPlatform.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly IOrder orderBL;
        private readonly ISignatureGenerator gen;
        private readonly IPaymentService paymentService;

        public PaymentsController(IOrder order, ISignatureGenerator gen, IPaymentService paymentService)
        {
            this.orderBL = order;
            this.gen = gen;
            this.paymentService = paymentService;
            
        }

        [HttpGet]
        [Route("/payment-redirect")]
        public IActionResult RedirectPayment(int? orderId)
        {
            if (orderId != null)
            {
                return View("PaymentRedirect", orderId);
            }

            return View("Error404");
        }

        [HttpPost]
        [Route("payment-details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PaymentPage(int orderId)
        {
            var m = await paymentService.CreatePaymentModel(orderId);

            if (m == null)
            {
                return Redirect("/selectPlan");
            }

            PaymentViewModel model = new PaymentViewModel();

            if (!m.EqualsNextPayEnd())
            {
                model = new PaymentViewModel
                {
                    Order = m.Order,
                    Signature = m.Signature,
                    OrderDate = m.OrderDate,
                    Productname = m.Productname,
                    OrderId = m.OrderId,
                    Price = m.Price,
                    Start = m.Start,
                    NextPay =  m.NextPay,
                    End = m.End
                };
            }
            else
            {
                model = new PaymentViewModel
                {
                    Order = m.Order,
                    Signature = m.Signature,
                    OrderDate = m.OrderDate,
                    Productname = m.Productname,
                    OrderId = m.OrderId,
                    Price = m.Price,
                    Start = m.Start,
                    NextPay = null,
                    End = m.End
                };
            }


            return View("Payments", model);
        }



        [HttpPost]
        [Route("payments/responseFromApi")]
        public async Task<IActionResult> ResponsePost()
        {
            WayforpayResponse? response;

            using (var reader = new StreamReader(Request.Body))
            {
                var body = await reader.ReadToEndAsync();
                response = JsonConvert.DeserializeObject<WayforpayResponse>(body);
            }

            if (response == null)
            {
                Console.WriteLine("Invalid response. Payment Controller response is null");
                return BadRequest("Invalid response");
            }

            //var generatedSignature = gen.GenerateSignature(response.OrderReference, response.TransactionStatus, response.ProcessingDate.ToString());

            //if (response.MerchantSignature != generatedSignature)
            //{
            //    return Unauthorized("Invalid signature");
            //}


            var confirmation = await paymentService.ChangeOrderStatus(response);

            await paymentService.SavePaymentInHistory(response);

            if (confirmation != null)
            {
                Console.WriteLine(response.TransactionStatus + " " + response.OrderReference + " was successfully changed PaymentController");
                return Ok(confirmation);
            }

            Console.WriteLine(response.TransactionStatus + " " + response.OrderReference + " was not changed PaymentController");
            return BadRequest("Some problems..");
        }

        [HttpPost]
        [Route("payments/success")]
        public async Task<IActionResult> Success()
        {
            return View("PaymentSuccess");
        }

        [HttpPost]
        [Route("payments/pending")]
        public async Task<IActionResult> Pending()
        {
            return View("Pending");
        }

        [HttpPost]
        [Route("payments/failed")]
        public async Task<IActionResult> Failed()
        {
            return View("Failed");
        }
    }
}
