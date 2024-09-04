namespace Domain.Interfaces.Messaging
{
    public interface ICreditOfferProducer
    {
        void ProducerMessage<T>(T message);
    }
}
