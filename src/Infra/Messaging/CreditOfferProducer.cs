using Domain.Interfaces.Messaging;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Infra.Messaging
{
    public class CreditOfferProducer : ICreditOfferProducer
    {
        private readonly string _queueName = "bank-credit-concession";
        private readonly ILogger<CreditOfferProducer> _logger;
        private readonly ConnectionFactory _connectionFactory;

        public CreditOfferProducer(string queueName, ILogger<CreditOfferProducer> logger, ConnectionFactory connectionFactory)
        {
            _queueName = queueName;
            _logger = logger;
            _connectionFactory = connectionFactory;
        }

        public void ProducerMessage<T>(T message)
        {
            IConnection conn = _connectionFactory.CreateConnection();
            using var channel = conn.CreateModel();

            channel.QueueDeclare(_queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);

            channel.BasicPublish("", _queueName, null, body: body);

            _logger.LogInformation("{Class} | Published message | Message: {Message}",
                nameof(CreditOfferProducer),
                message);
        }
    }
}
