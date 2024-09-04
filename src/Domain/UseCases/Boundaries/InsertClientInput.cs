using MediatR;

namespace Domain.UseCases.Boundaries
{
    public record InsertClientInput : IRequest<string>
    {
        public InsertClientInput(Guid correlationId, Client client)
        {
            CorrelationId = correlationId;
            Client = client;
        }

        public Guid CorrelationId { get; init; }
        public Client Client { get; init; }
    }
}
