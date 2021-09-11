namespace TenPinBowling.Api.Features.CalculateScores
{
    /// <summary>
    /// Calculate scores response
    /// </summary>
    public class CalculateScoresResponse
    {
        /// <summary>
        /// Array of scores for each frame progressed
        /// </summary>
        public string[] FrameProgressScores { get; set; }

        /// <summary>
        /// Indicator for game completion
        /// </summary>
        public bool GameCompleted { get; set; }
    }
}
