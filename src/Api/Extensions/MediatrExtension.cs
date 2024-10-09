using Domain.Interfaces;
using Domain.UseCases;

namespace parana_bank_credit_offer.Extensions
{
    public static class MediatrExtension
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            services.AddScoped<ICreditOfferUseCase, CreditOfferUseCase>();
            return services;
        }
    }
}
