using Newtonsoft.Json;
using TenPinBowling.Api.Features.CalculateScores;
using Xunit;

namespace TenPinBowling.Tests.Features.CalculateScores
{
    public class CalculateScoresHandlerTests: TestBase
    {

        [Fact]
        public void CalculateScores_AllGutterBall()
        {
            var handler = new CalculateScoresHandler(Configuration);

            int[] input = { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

            string[] frameProgressScores = { "0","0","0","0","0","0","0","0","0","0" };
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
            var handler = new CalculateScoresHandler(Configuration);

            int[] input = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };

            string[] frameProgressScores = { "30","60","90","120","150","180","210","240","270","300" };
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
        public void CalculateScores_6FramesCompleted()
        {
            var handler = new CalculateScoresHandler(Configuration);

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
        public void CalculateScores_7FramesCompletedWithSparesAndStrike()
        {
            var handler = new CalculateScoresHandler(Configuration);

            int[] input = { 1,1,1,1,9,1,2,8,9,1,10,10 };

            string[] frameProgressScores = { "2", "4", "16", "35", "55", "*","*" };
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
            var handler = new CalculateScoresHandler(Configuration);

            int[] input = { 10, 1, 1, 1, 9, 1, 2, 8, 1, 9, 1, 5, 4, 10, 9, 0, 6};

            string[] frameProgressScores = { "12", "14", "25", "28", "37", "52", "61", "80", "89", "*"};
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
    }
}
