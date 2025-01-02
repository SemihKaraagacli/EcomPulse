using EcomPulse.Repository.CreditCardRepository;
using EcomPulse.Service.CreditCardService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EcomPulse.Service.HostedService
{
    public class ExpirationDateCheckerService : BackgroundService
    {
        private readonly ILogger<ExpirationDateCheckerService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        public ExpirationDateCheckerService(IServiceScopeFactory scopeFactory, ILogger<ExpirationDateCheckerService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken) //A back service was created to delete expired credit cards after checking every 1 day.
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var creditCards = scope.ServiceProvider.GetRequiredService<ICreditCardRespository>();
                        var expiredCards = await creditCards.GetExpiredCardsAsync(stoppingToken);
                        if (expiredCards.Any())
                        {
                            _logger.LogInformation($"Found {expiredCards.Count} expired cards. Deleting...");

                            await creditCards.DeleteExpiredCardsAsync(expiredCards, stoppingToken);

                            _logger.LogInformation($"{expiredCards.Count} expired cards deleted successfully.");
                        }
                        else
                        {
                            _logger.LogInformation("No expired cards found during this cycle.");
                        }
                    }
                    await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Error in ExpirationDateCheckerService: {ex.Message}");
                }
            }
        }
    }
}
