using Domain.Events;
using Domain.UseCases.Boundaries;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Worker.Message;

namespace UnitTests
{
    public class OfferConsumerUnitTest
    {
        private readonly Mock<ILogger<OfferConsumer>> _loggerMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly OfferConsumer _offerConsumer;
        private readonly Mock<ConsumeContext<ClientOfferEvent>> _contextMock;

        public OfferConsumerUnitTest()
        {
            _loggerMock = new Mock<ILogger<OfferConsumer>>();
            _mediatorMock = new Mock<IMediator>();
            _offerConsumer = new OfferConsumer(_loggerMock.Object, _mediatorMock.Object);
            _contextMock = new Mock<ConsumeContext<ClientOfferEvent>>();
        }

        [Fact]
        public async Task Given_Consume_Should_LogInformation_When_EventIsReceived()
        {
            // Arrange
            var clientOfferEvent = new ClientOfferEvent
            {
                CorrelationId = Guid.NewGuid(),
                Document = 1234567890,
                Income = 5000,
                Score = 750
            };

            _contextMock.SetupGet(x => x.Message).Returns(clientOfferEvent);

            // Act
            await _offerConsumer.Consume(_contextMock.Object);

            // Assert
            _mediatorMock.Verify(mediator => mediator.Send(It.IsAny<CreditOfferInput>(), default), Times.Once);
            _loggerMock.Verify(
                logger => logger.Log(
                    It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((state, t) => state.ToString().Contains("Event received")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }

        [Fact]
        public async Task Given_Consume_Should_LogError_When_ExceptionIsThrown()
        {
            // Arrange
            var clientOfferEvent = new ClientOfferEvent
            {
                CorrelationId = Guid.NewGuid(),
                Document = 1234567890,
                Income = 5000,
                Score = 750
            };

            _contextMock.SetupGet(x => x.Message).Returns(clientOfferEvent);
            _mediatorMock.Setup(mediator => mediator.Send(It.IsAny<CreditOfferInput>(), default))
                        .Throws(new Exception("Test Exception"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _offerConsumer.Consume(_contextMock.Object));

            _loggerMock.Verify(
                 logger => logger.Log(
                     It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                     It.IsAny<EventId>(),
                     It.Is<It.IsAnyType>((state, t) => state.ToString().Contains("Error")),
                     It.IsAny<Exception>(),
                     It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                 Times.Once);
        }
    }
}