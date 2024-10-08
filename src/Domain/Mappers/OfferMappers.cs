using Domain.Events;
using MassTransit;

namespace Domain.Mappers
{
    public static class OfferMappers
    {
        public static CreditCardEvent ToEvent(ConsumeContext<ClientOfferEvent> clientEvent)
        {
            return new CreditCardEvent(
                clientEvent.Message.CorrelationId,
                clientEvent.Message.Document,
                clientEvent.Message.Income,
                300,
                123456
                );
        }
    }
}
