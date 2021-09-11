using MediatR;

namespace TenPinBowling.Api.Features.CalculateScores
{

    /// <summary>
    /// Calculate scores request
    /// </summary>
    public class CalculateScoresRequest : IRequest<CalculateScoresResponse>
    {
        /// <summary>
        /// Array of Pins downed in each throw
        /// </summary>
        public int[] PinsDowned { get; set; }
    }
}
