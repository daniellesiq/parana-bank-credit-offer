using Domain.Events;
using Domain.Mappers;
using MassTransit;
using MediatR;

namespace Worker.Message
{
    public class OfferConsumer : IConsumer<ClientOfferEvent>
    {
        private readonly ILogger<OfferConsumer> _logger;
        private readonly IMediator _mediator;

        public OfferConsumer(ILogger<OfferConsumer> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<ClientOfferEvent> context)
        {
            var document = context.Message.Document;
            try
            {
                _logger.LogInformation($"Event received: {nameof(OfferConsumer)}: {document} ");

                var input = OfferMappers.EventToInput(context);
                await _mediator.Send(input);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                throw;
            }
        }
    }
}
