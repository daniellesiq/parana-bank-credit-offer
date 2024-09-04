using Domain.UseCases.Boundaries;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Infra.Messaging
{
    public class CreditOfferConsumer : BackgroundService
    {
        private readonly string _queueName = "bank-credit-offer";
        private readonly ConnectionFactory _connectionFactory;
        private readonly ILogger<CreditOfferConsumer> _logger;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMediator _mediator;

        public CreditOfferConsumer(
            ConnectionFactory connectionFactory,
            ILogger<CreditOfferConsumer> logger,
            IServiceProvider serviceProvider,
            IMediator mediator)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
            _serviceProvider = serviceProvider;
            _mediator = mediator;

            IConnection connection = _connectionFactory.CreateConnection();
            _channel = connection.CreateModel();

            _channel.QueueDeclare(queue: _queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, eventArgs) =>
            {
                var contentArray = eventArgs.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);
                var message = JsonConvert.DeserializeObject<InsertClientInput>(contentString);

                _logger.LogInformation($"Message: {message!.Client.ToString()}");
                _channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);

                _mediator.Send(message, cancellationToken);
            };

            _channel.BasicConsume(queue: _queueName,
                                     autoAck: false,
                                     consumer: consumer);

            _logger.LogInformation("Mensage Consumed!");
            return Task.CompletedTask;
        }
    }
}
