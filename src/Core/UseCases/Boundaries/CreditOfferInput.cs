using MediatR;

namespace Domain.UseCases.Boundaries
{
    public record CreditOfferInput : IRequest<string>
    {
        public CreditOfferInput(Guid correlationId, long document, decimal income, int score)
        {
            CorrelationId = correlationId;
            Document = document;
            Income = income;
            Score = score;
        }

        public Guid CorrelationId { get; init; } = default!;
        public long Document { get; init; } = default!;
        public decimal Income { get; init; } = default!;
        public int Score { get; init; } = default!;
    }
}
