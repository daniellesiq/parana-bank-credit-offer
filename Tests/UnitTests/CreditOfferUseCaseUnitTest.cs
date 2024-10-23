using Domain.Events;
using Domain.UseCases;
using Domain.UseCases.Boundaries;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests
{
    public class CreditOfferUseCaseUnitTest
    {
        private readonly CreditOfferUseCase _useCase;
        private readonly Mock<ILogger<CreditOfferUseCase>> _loggerMock;
        private readonly Mock<IPublishEndpoint> _publishMock;

        public CreditOfferUseCaseUnitTest()
        {
            _loggerMock = new Mock<ILogger<CreditOfferUseCase>>();
            _publishMock = new Mock<IPublishEndpoint>();
            _useCase = new CreditOfferUseCase(_loggerMock.Object, _publishMock.Object);
        }

        [Fact]
        public async Task Given_Handle_Should_LogInformation_When_OfferIsApproved()
        {
            // Arrange
            var input = new CreditOfferInput
            {
                CorrelationId = Guid.NewGuid(),
                Document = 1234567890,
                Income = 5000,
                Score = 750
            };

            _publishMock.Setup(c => c.Publish(It.IsAny<CreditCardEvent>(), It.IsAny<CancellationToken>()));

            // Act
            var result = await _useCase.Handle(input, new CancellationToken());

            // Assert
            Assert.Equal("", result);
            _publishMock.Verify(publisher =>
                publisher.Publish(It.IsAny<CreditCardEvent>(), new CancellationToken()), Times.Once);
            _loggerMock.Verify(
                  logger => logger.Log(
                      It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
                      It.IsAny<EventId>(),
                      It.Is<It.IsAnyType>((state, t) => state.ToString().Contains("Offer Approved")),
                      It.IsAny<Exception>(),
                      It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                  Times.Once);
        }

        [Fact]
        public async Task Given_Handle_Should_LogInformation_When_OfferIsDenied()
        {
            // Arrange
            var input = new CreditOfferInput
            {
                CorrelationId = Guid.NewGuid(),
                Document = 1234567890,
                Income = 5000,
                Score = 250
            };

            var cancellationToken = new CancellationToken();
            _publishMock.Setup(c => c.Publish(It.IsAny<CreditCardEvent>(), It.IsAny<CancellationToken>()));

            // Act
            var result = await _useCase.Handle(input, cancellationToken);

            // Assert
            Assert.Equal("", result);
            _publishMock.Verify(publisher =>
                publisher.Publish(It.IsAny<CreditCardEvent>(), new CancellationToken()), Times.Never);
            _loggerMock.Verify(
                  logger => logger.Log(
                      It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
                      It.IsAny<EventId>(),
                      It.Is<It.IsAnyType>((state, t) => state.ToString().Contains("Offer Denied")),
                      It.IsAny<Exception>(),
                      It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                  Times.Once);
        }

        [Fact]
        public async Task Given_Handle_Should_LogError_When_ExceptionIsThrown()
        {
            // Arrange
            var input = new CreditOfferInput
            {
                CorrelationId = Guid.NewGuid(),
                Document = 1234567890,
                Income = 5000,
                Score = 750
            };

            var cancellationToken = new CancellationToken();
            _publishMock.Setup(p => p.Publish(It.IsAny<CreditCardEvent>(), cancellationToken))
                         .Throws(new Exception("Error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _useCase.Handle(input, cancellationToken));

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