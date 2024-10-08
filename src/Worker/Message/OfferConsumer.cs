using Domain.Events;
using Domain.Mappers;
using MassTransit;

namespace Worker.Message
{
    public class OfferConsumer : IConsumer<ClientOfferEvent>
    {
        private readonly ILogger<OfferConsumer> _logger;
        private readonly IPublishEndpoint _publisher;

        public OfferConsumer(ILogger<OfferConsumer> logger, IPublishEndpoint publisher)
        {
            _logger = logger;
            _publisher = publisher;
        }

        public async Task Consume(ConsumeContext<ClientOfferEvent> context)
        {
            var document = context.Message.Document;
            try
            {
                _logger.LogInformation($"Event received: {nameof(OfferConsumer)}: {document} ");

                var message = OfferMappers.ToEvent(context);
                await _publisher.Publish(message);

                _logger.LogInformation($"Sent event: {nameof(OfferConsumer)}: {document} ");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
