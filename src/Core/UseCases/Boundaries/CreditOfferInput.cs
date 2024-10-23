using MediatR;

namespace Domain.UseCases.Boundaries
{
    public record CreditOfferInput : IRequest<string>
    {
        public Guid CorrelationId { get; init; } = default!;
        public long Document { get; init; } = default!;
        public decimal Income { get; init; } = default!;
        public int Score { get; init; } = default!;
    }
}
