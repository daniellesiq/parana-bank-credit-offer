using Domain.Interfaces.Messaging;
using Infra.Messaging;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Infra.Extensions
{
    public static class InfraExtensions
    {
        public static IServiceCollection AddRabbitExtensions(this IServiceCollection services)
        {
            services.AddScoped<ICreditOfferProducer, CreditOfferProducer>();
            services.AddSingleton<ConnectionFactory>(new ConnectionFactory { HostName = "localhost" });
            services.AddHostedService<CreditOfferConsumer>();

            return services;
        }
    }
}
