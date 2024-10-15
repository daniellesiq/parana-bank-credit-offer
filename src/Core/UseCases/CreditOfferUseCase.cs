using Domain.Interfaces;
using Domain.Mappers;
using Domain.UseCases.Boundaries;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Domain.UseCases
{
    public class CreditOfferUseCase : ICreditOfferUseCase
    {
        private readonly ILogger<CreditOfferUseCase> _logger;
        private readonly IPublishEndpoint _publisher;
        const decimal LIMIT = 1000m;

        public CreditOfferUseCase(
            ILogger<CreditOfferUseCase> logger,
            IPublishEndpoint publisher)
        {
            _logger = logger;
            _publisher = publisher;
        }

        public async Task<string> Handle(CreditOfferInput input, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("{Class} | Starting | CorrelationId: {CorrelationId}",
                     nameof(CreditOfferUseCase),
                     input.CorrelationId);

                if (input.Score < 300)
                {
                    _logger.LogInformation("{Class} | Score Very Low Offer Denied  |  | CorrelationId: {CorrelationId}",
                     nameof(CreditOfferUseCase),
                     input.CorrelationId);

                    return "";
                }

                _logger.LogDebug("{Class} | Offer Approved | CorrelationId: {CorrelationId}",
                     nameof(CreditOfferUseCase),
                     input.CorrelationId);

                var offerEvent = OfferMappers.InputToEvent(input, LIMIT);
                await _publisher.Publish(offerEvent, cancellationToken);

                _logger.LogInformation("{Class} | Ending | CorrelationId: {CorrelationId}",
                    nameof(CreditOfferUseCase),
                    input.CorrelationId);

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
