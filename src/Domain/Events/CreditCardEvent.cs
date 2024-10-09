﻿namespace Domain.Events
{
    public record CreditCardEvent
    {
        public CreditCardEvent(Guid correlationId, long document, decimal income, int score, decimal creditLimit)
        {
            CorrelationId = correlationId;
            Document = document;
            Income = income;
            Score = score;
            CreditLimit = creditLimit;
        }

        public Guid CorrelationId { get; init; } = default!;
        public long Document { get; init; } = default!;
        public decimal Income { get; init; } = default!;
        public int Score { get; init; } = default!;
        public decimal CreditLimit { get; set; }
    }
}
