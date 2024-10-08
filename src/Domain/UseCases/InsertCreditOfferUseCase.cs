using Domain.Interfaces;
using Domain.UseCases.Boundaries;
using Microsoft.Extensions.Logging;

namespace Domain.UseCases
{
    public class InsertCreditOfferUseCase : IInsertCreditOfferUseCase
    {
        private readonly ILogger<InsertCreditOfferUseCase> _logger;

        public InsertCreditOfferUseCase(
            ILogger<InsertCreditOfferUseCase> logger)
        {
            _logger = logger;
        }

        public async Task<string> Handle(InsertClientInput message, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("{Class} | Publish message | CorrelationId: {CorrelationId}",
                 nameof(InsertCreditOfferUseCase),
                 message.CorrelationId);

                ///Add use case

                ///Add Mapper
                //_producer.ProducerMessage(message);

                return "";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                throw;
            }
        }
    }
}
