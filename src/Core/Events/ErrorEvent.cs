namespace Core.Events
{
    public record ErrorEvent
    {
        public Guid CorrelationId { get; init; }
        public string ErrorMessage { get; init; }
        public string Source { get; init; }
    }
}
