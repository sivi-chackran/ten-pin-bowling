using TenPinBowling.Api.Features.CalculateScores;
using Xunit;

namespace TenPinBowling.Tests.Features.CalculateScores
{
    public class CalculateScoresValidatorTests : TestBase
    {

        [Fact]
        public void CalculateScores_ValidationFail_Empty()
        {
            var validator = new CalculateScoresValidator(appSettings);
            int[] input = { };

            Assert.False(validator.Validate(new CalculateScoresRequest() { PinsDowned = input }).IsValid);
        }

        [Fact]
        public void CalculateScores_ValidationFail_InvalidNumberOfPinsDowned()
        {
            var validator = new CalculateScoresValidator(appSettings);
            int[] input = { 1, 9, 0, 11 };

            Assert.False(validator.Validate(new CalculateScoresRequest() { PinsDowned = input }).IsValid);
        }

        [Fact]
        public void CalculateScores_ValidationFail_InvalidNumberOfThrows()
        {
            var validator = new CalculateScoresValidator(appSettings);
            int[] input = { 1, 9, 0, 10, 1, 3, 4, 6, 3, 7, 9, 0, 0, 0, 1, 1, 2, 7, 9, 0, 7, 2 };

            Assert.False(validator.Validate(new CalculateScoresRequest() { PinsDowned = input }).IsValid);
        }

        [Fact]
        public void CalculateScores_ValidationPass_ValidInput()
        {
            var validator = new CalculateScoresValidator(appSettings);
            int[] input = { 1, 9, 0, 10, 1, 3, 4, 6, 3, 7, 9, 0, 0, 0, 1, 1, 2, 7, 9, 0 };

            Assert.True(validator.Validate(new CalculateScoresRequest() { PinsDowned = input }).IsValid);
        }
    }
}
