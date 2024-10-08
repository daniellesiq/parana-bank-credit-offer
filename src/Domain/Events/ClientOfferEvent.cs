namespace Domain.Events
{
    public record ClientOfferEvent
    {
        public ClientOfferEvent(
            Guid correlationId,
            long document,
            decimal income)
        {
            CorrelationId = correlationId;
            Document = document;
            Income = income;
        }

        public Guid CorrelationId { get; init; } = default!;
        public long Document { get; init; } = default!;
        public decimal Income { get; init; } = default!;
    }
}
