using Domain.Events;
using Domain.UseCases.Boundaries;
using MassTransit;

namespace Domain.Mappers
{
    public static class OfferMappers
    {
        public static CreditOfferInput EventToInput(ConsumeContext<ClientOfferEvent> clientEvent)
        {
            var creditOfferInput = new CreditOfferInput
            {
                CorrelationId = clientEvent.Message.CorrelationId,
                Document = clientEvent.Message.Document,
                Income = clientEvent.Message.Income,
                Score = clientEvent.Message.Score
            };
            return creditOfferInput;
        }

        public static CreditCardEvent InputToEvent(CreditOfferInput input, decimal limit)
        {
            var creditCardEvent = new CreditCardEvent
            {
                CorrelationId = input.CorrelationId,
                Document = input.Document,
                Income = input.Income,
                Score = input.Score,
                CreditLimit = limit
            };
            return creditCardEvent;
        }
    }
}
