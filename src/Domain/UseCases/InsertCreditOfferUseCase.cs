using Domain.Interfaces;
using Domain.Interfaces.Messaging;
using Domain.UseCases.Boundaries;
using Microsoft.Extensions.Logging;

namespace Domain.UseCases
{
    public class InsertCreditOfferUseCase : IInsertCreditOfferUseCase
    {
        private readonly ICreditOfferProducer _producer;
        private readonly ILogger<InsertCreditOfferUseCase> _logger;

        public InsertCreditOfferUseCase(
            ILogger<InsertCreditOfferUseCase> logger,
            ICreditOfferProducer producer)
        {
            _logger = logger;
            _producer = producer;
        }

        public async Task<string> Handle(InsertClientInput input, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("{Class} | Publish message | CorrelationId: {CorrelationId}",
                 nameof(InsertCreditOfferUseCase),
                 input.CorrelationId);

                //Mapper
                //var message = 

                _producer.ProducerMessage(input);

                return "";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error to publish message.");
                throw;
            }
        }
    }
}
