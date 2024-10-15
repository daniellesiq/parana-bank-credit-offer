using Domain.UseCases.Boundaries;
using MediatR;

namespace Domain.Interfaces
{
    public interface ICreditOfferUseCase : IRequestHandler<CreditOfferInput, string> { }
}
