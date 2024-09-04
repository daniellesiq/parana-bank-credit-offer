using Domain.Entity;
using MediatR;

namespace Domain.Interfaces
{
    public interface IInsertCreditOfferUseCase : IRequestHandler<ClientOfferMessage, string> { }
}
