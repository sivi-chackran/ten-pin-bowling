using Newtonsoft.Json;
using System;
using TenPinBowling.Api.Features.CalculateScores;
using TenPinBowling.Common.Exceptions;
using TenPinBowling.Common.Model;
using Xunit;

namespace TenPinBowling.Tests.Features.CalculateScores
{
    public class CalculateScoresHandlerTests: TestBase
    {

        [Fact]
        public void CalculateScores_AllGutterBall()
        {
            var handler = new CalculateScoresHandler(appSettings);

            int[] input = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            string[] frameProgressScores = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
            var expectedResponse = new CalculateScoresResponse
            {
                FrameProgressScores = frameProgressScores,
                GameCompleted = true
            };

            var actualResponse = handler.Handle(new CalculateScoresRequest { PinsDowned = input },
                default);

            Assert.Equal(JsonConvert.SerializeObject(expectedResponse),
                JsonConvert.SerializeObject(actualResponse.Result));
        }

        [Fact]
        public void CalculateScores_PerfectGame()
        {
            var handler = new CalculateScoresHandler(appSettings);

            int[] input = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };

            string[] frameProgressScores = { "30", "60", "90", "120", "150", "180", "210", "240", "270", "300" };
            var expectedResponse = new CalculateScoresResponse
            {
                FrameProgressScores = frameProgressScores,
                GameCompleted = true
            };

            var actualResponse = handler.Handle(new CalculateScoresRequest { PinsDowned = input },
                default);

            Assert.Equal(JsonConvert.SerializeObject(expectedResponse),
                JsonConvert.SerializeObject(actualResponse.Result));
        }

        [Fact]
        public void CalculateScores_GameInProgress_6FramesCompleted()
        {
            var handler = new CalculateScoresHandler(appSettings);

            int[] input = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

            string[] frameProgressScores = { "2", "4", "6", "8", "10", "12" };
            var expectedResponse = new CalculateScoresResponse
            {
                FrameProgressScores = frameProgressScores,
                GameCompleted = false
            };

            var actualResponse = handler.Handle(new CalculateScoresRequest { PinsDowned = input },
                default);

            Assert.Equal(JsonConvert.SerializeObject(expectedResponse),
                JsonConvert.SerializeObject(actualResponse.Result));
        }

        [Fact]
        public void CalculateScores_GameInProgress_ThirdFrameIncomplete()
        {
            var handler = new CalculateScoresHandler(appSettings);

            int[] input = { 6, 3, 5, 2, 7 };

            string[] frameProgressScores = { "9", "16", "*" };
            var expectedResponse = new CalculateScoresResponse
            {
                FrameProgressScores = frameProgressScores,
                GameCompleted = false
            };

            var actualResponse = handler.Handle(new CalculateScoresRequest { PinsDowned = input },
                default);

            Assert.Equal(JsonConvert.SerializeObject(expectedResponse),
                JsonConvert.SerializeObject(actualResponse.Result));
        }

        [Fact]
        public void CalculateScores_GameInProgress_WithStrikeAndSpare()
        {
            var handler = new CalculateScoresHandler(appSettings);

            int[] input = { 1, 1, 1, 1, 9, 1, 2, 8, 9, 1, 10, 10 };

            string[] frameProgressScores = { "2", "4", "16", "35", "55", "*", "*" };
            var expectedResponse = new CalculateScoresResponse
            {
                FrameProgressScores = frameProgressScores,
                GameCompleted = false
            };

            var actualResponse = handler.Handle(new CalculateScoresRequest { PinsDowned = input },
                default);

            Assert.Equal(JsonConvert.SerializeObject(expectedResponse),
                JsonConvert.SerializeObject(actualResponse.Result));
        }

        [Fact]
        public void CalculateScores_OneThrowCompletedInLastFrame()
        {
            var handler = new CalculateScoresHandler(appSettings);

            int[] input = { 10, 1, 1, 1, 9, 1, 2, 8, 1, 9, 1, 5, 4, 10, 9, 0, 6 };

            string[] frameProgressScores = { "12", "14", "25", "28", "37", "52", "61", "80", "89", "*" };
            var expectedResponse = new CalculateScoresResponse
            {
                FrameProgressScores = frameProgressScores,
                GameCompleted = false
            };

            var actualResponse = handler.Handle(new CalculateScoresRequest { PinsDowned = input },
                default);

            Assert.Equal(JsonConvert.SerializeObject(expectedResponse),
                JsonConvert.SerializeObject(actualResponse.Result));
        }

        [Fact]
        public void CalculateScores_GameInProgress_SpareInLastFrame()
        {
            var handler = new CalculateScoresHandler(appSettings);

            int[] input = { 10, 1, 1, 1, 9, 1, 2, 8, 1, 9, 1, 5, 4, 10, 9, 0, 6, 4 };

            string[] frameProgressScores = { "12", "14", "25", "28", "37", "52", "61", "80", "89", "*" };
            var expectedResponse = new CalculateScoresResponse
            {
                FrameProgressScores = frameProgressScores,
                GameCompleted = false
            };

            var actualResponse = handler.Handle(new CalculateScoresRequest { PinsDowned = input },
                default);

            Assert.Equal(JsonConvert.SerializeObject(expectedResponse),
                JsonConvert.SerializeObject(actualResponse.Result));
        }

        [Fact]
        public void CalculateScores_GameComplete_StrikeInLastFrame()
        {
            var handler = new CalculateScoresHandler(appSettings);

            int[] input = { 10, 1, 1, 1, 9, 1, 2, 8, 1, 9, 1, 5, 4, 10, 9, 0, 10, 4, 3 };

            string[] frameProgressScores = { "12", "14", "25", "28", "37", "52", "61", "80", "89", "106" };
            var expectedResponse = new CalculateScoresResponse
            {
                FrameProgressScores = frameProgressScores,
                GameCompleted = true
            };

            var actualResponse = handler.Handle(new CalculateScoresRequest { PinsDowned = input },
                default);

            Assert.Equal(JsonConvert.SerializeObject(expectedResponse),
                JsonConvert.SerializeObject(actualResponse.Result));
        }

        [Fact]
        public void CalculateScores_GameComplete_StrikeAndSpareInLastFrame()
        {
            var handler = new CalculateScoresHandler(appSettings);

            int[] input = { 10, 1, 1, 1, 9, 1, 2, 8, 1, 9, 1, 5, 4, 10, 9, 0, 10, 7, 3 };

            string[] frameProgressScores = { "12", "14", "25", "28", "37", "52", "61", "80", "89", "109" };
            var expectedResponse = new CalculateScoresResponse
            {
                FrameProgressScores = frameProgressScores,
                GameCompleted = true
            };

            var actualResponse = handler.Handle(new CalculateScoresRequest { PinsDowned = input },
                default);

            Assert.Equal(JsonConvert.SerializeObject(expectedResponse),
                JsonConvert.SerializeObject(actualResponse.Result));
        }

        [Fact]
        public void CalculateScores_GameComplete_AllStrikeInLastFrame()
        {
            var handler = new CalculateScoresHandler(appSettings);

            int[] input = { 10, 1, 1, 1, 9, 1, 2, 8, 1, 9, 1, 5, 4, 10, 9, 0, 10, 10, 10 };

            string[] frameProgressScores = { "12", "14", "25", "28", "37", "52", "61", "80", "89", "119" };
            var expectedResponse = new CalculateScoresResponse
            {
                FrameProgressScores = frameProgressScores,
                GameCompleted = true
            };

            var actualResponse = handler.Handle(new CalculateScoresRequest { PinsDowned = input },
                default);

            Assert.Equal(JsonConvert.SerializeObject(expectedResponse),
                JsonConvert.SerializeObject(actualResponse.Result));
        }

        [Fact]
        public void CalculateScores_GameComplete_NormalLastFrame()
        {
            var handler = new CalculateScoresHandler(appSettings);

            int[] input = { 10, 1, 1, 1, 9, 1, 2, 8, 1, 9, 1, 5, 4, 10, 9, 0, 4, 5 };

            string[] frameProgressScores = { "12", "14", "25", "28", "37", "52", "61", "80", "89", "98" };
            var expectedResponse = new CalculateScoresResponse
            {
                FrameProgressScores = frameProgressScores,
                GameCompleted = true
            };

            var actualResponse = handler.Handle(new CalculateScoresRequest { PinsDowned = input },
                default);

            Assert.Equal(JsonConvert.SerializeObject(expectedResponse),
                JsonConvert.SerializeObject(actualResponse.Result));
        }

        [Fact]
        public async System.Threading.Tasks.Task CalculateScores_InvalidNumberOfPinsDownedInFrame()
        {
            var handler = new CalculateScoresHandler(appSettings);
            int[] input = { 10,0,9,10,2,9,3,6,10,1,5,7,0,0,0,2 };

            var expectedResponse = new Error
            {
                ErrorType = "BadRequest",
                ErrorMessage = "Pins downed in a frame cannot exceed 10 - pinsDowned[4] + pinsDowned[5]"
            };

            var actualResponse = await Assert.ThrowsAsync<BadRequestException>( () => handler.Handle(new CalculateScoresRequest { PinsDowned = input },
                    default));

            Assert.Equal(JsonConvert.SerializeObject(expectedResponse),
                JsonConvert.SerializeObject(actualResponse.Error));
        }
    }
}
