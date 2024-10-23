using Bogus;
using Domain.Mappers;
using Domain.UseCases.Boundaries;

namespace UnitTests
{
    public class OfferMappersUnitTest
    {
        [Fact]
        public async Task Given_InputToEvent_ReturnsExpectedCreditCardEvent()
        {
            // Arrange
            var limit = 1000m;
            var input = new Faker<CreditOfferInput>()
                .RuleFor(c => c.Document, 1234567890)
                .RuleFor(c => c.Income, 1000m)
                .RuleFor(c => c.Score, 500)
                .Generate();

            // Act
            var result = OfferMappers.InputToEvent(input, limit);

            // Assert
            Assert.Equal(input.Document, result.Document);
            Assert.Equal(input.Income, result.Income);
            Assert.Equal(input.Score, result.Score);
        }
    }
}