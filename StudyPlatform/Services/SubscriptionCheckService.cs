using BL.Orders;
using DAL.Models;

namespace StudyPlatform.Services
{
    public class SubscriptionCheckService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<SubscriptionCheckService> _logger;
        private readonly TimeSpan _delay = TimeSpan.FromDays(1);

        public SubscriptionCheckService(IServiceScopeFactory scopeFactory, ILogger<SubscriptionCheckService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var orderService = scope.ServiceProvider.GetRequiredService<IOrder>();

                        await CheckSubscriptions(orderService);
                    }

                    _logger.LogInformation("Подписки успешно проверены.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка при проверке подписок.");
                }

                await Task.Delay(_delay, stoppingToken);
            }
        }

        private async Task CheckSubscriptions(IOrder orderService)
        {
            var orders = await orderService.GetAllOrders();

            foreach (var order in orders)
            {
                if (order.End < DateTime.Now && order.Status != StatusCode.Expired)
                {
                    await orderService.UpdateOrderStatus(order.Id, StatusCode.Expired); 
                }
            }
        }
    }
}
