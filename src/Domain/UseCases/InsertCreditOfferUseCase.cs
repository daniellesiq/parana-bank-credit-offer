using Domain.Entity;
using Domain.Interfaces;
using Domain.Interfaces.Messaging;
using Microsoft.Extensions.Logging;

namespace Domain.UseCases
{
    public class InsertCreditOfferUseCase : IInsertCreditOfferUseCase
    {
        private readonly ICreditOfferProducer _producer;
        private readonly ILogger<InsertCreditOfferUseCase> _logger;

        public InsertCreditOfferUseCase(
            ICreditOfferProducer producer,
            ILogger<InsertCreditOfferUseCase> logger)
        {
            _logger = logger;
            _producer = producer;
        }

        public async Task<string> Handle(ClientOfferMessage message, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("{Class} | Publish message | CorrelationId: {CorrelationId}",
                 nameof(InsertCreditOfferUseCase),
                 message.CorrelationId);

                ///Add use case

                ///Add Mapper
                _producer.ProducerMessage(message);

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
