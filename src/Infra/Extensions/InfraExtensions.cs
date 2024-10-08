using Domain.Interfaces;
using Domain.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Extensions
{
    public static class InfraExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IInsertCreditOfferUseCase, InsertCreditOfferUseCase>();
            return services;
        }
    }
}
