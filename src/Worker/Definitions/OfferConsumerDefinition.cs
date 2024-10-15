using MassTransit;
using Worker.Message;

namespace Worker.Definitions
{
    public class OfferConsumerDefinition : ConsumerDefinition<OfferConsumer>
    {
        protected override void ConfigureConsumer(
           IReceiveEndpointConfigurator endpointConfigurator,
           IConsumerConfigurator<OfferConsumer> consumerConfigurator)
        {
            consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(5)));
        }
    }
}
