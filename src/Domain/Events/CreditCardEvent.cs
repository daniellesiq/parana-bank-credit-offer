namespace Domain.Events
{
    public record CreditCardEvent
    {
        public CreditCardEvent(Guid correlationId, long document, decimal income, int score, int account)
        {
            CorrelationId = correlationId;
            Document = document;
            Income = income;
            Score = score;
            Account = account;
        }

        public Guid CorrelationId { get; init; } = default!;
        public long Document { get; init; } = default!;
        public decimal Income { get; init; } = default!;
        public int Score { get; init; } = default!;
        public int Account { get; init; } = default!;
    }
}
