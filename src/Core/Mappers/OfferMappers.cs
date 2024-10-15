using Domain.Events;
using Domain.UseCases.Boundaries;
using MassTransit;

namespace Domain.Mappers
{
    public static class OfferMappers
    {
        public static CreditOfferInput EventToInput(ConsumeContext<ClientOfferEvent> clientEvent)
        {
            return new CreditOfferInput(
                clientEvent.Message.CorrelationId,
                clientEvent.Message.Document,
                clientEvent.Message.Income,
                clientEvent.Message.Score
                );
        }

        public static CreditCardEvent InputToEvent(CreditOfferInput input, decimal limit)
        {
            return new CreditCardEvent(
                input.CorrelationId,
                input.Document,
                input.Income,
                input.Score,
                limit
                );
        }
    }
}
