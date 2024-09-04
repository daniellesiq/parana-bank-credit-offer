using Domain.UseCases.Boundaries;
using MediatR;

namespace Domain.Interfaces
{
    public interface IInsertCreditOfferUseCase : IRequestHandler<InsertClientInput, string> { }
}
