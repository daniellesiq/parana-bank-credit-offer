using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Infra.InfraExtensions
{
    public static class InfraExtensions
    {
        public static IServiceCollection AddConsumers(this IServiceCollection services)
        {
            //services.AddScoped<ICreditOfferProducer, CreditOfferProducer>();
            services.AddSingleton<ConnectionFactory>(new ConnectionFactory { HostName = "localhost" });

            return services;
        }
    }
}
