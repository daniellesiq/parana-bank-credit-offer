using MediatR;

namespace Domain.Entity
{
    public record ClientOfferMessage : IRequest<string>
    {
        public ClientOfferMessage(
            Guid correlationId,
            long document,
            decimal income,
            string rating,
            string account)
        {
            Document = document;
            Income = income;
            Rating = rating;
            Account = account;
        }

        public Guid CorrelationId { get; init; } = default!;
        public long Document { get; init; } = default!;
        public decimal Income { get; init; } = default!;
        public string Rating { get; init; } = default!;
        public string Account { get; init; } = default!;
    }
}
