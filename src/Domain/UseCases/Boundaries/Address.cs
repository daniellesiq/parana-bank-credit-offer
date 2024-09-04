namespace Domain.UseCases.Boundaries
{
    public record Address
    {
        public string Street { get; init; } = default!;
        public int Number { get; init; } = default!;
        public string Neighborhood { get; init; } = default!;
        public int PostalCode { get; init; } = default!;
        public string City { get; init; } = default!;
        public string State { get; init; } = default!;
    }
}
