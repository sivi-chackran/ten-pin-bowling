using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TenPinBowling.Api.Features.CalculateScores
{
    /// <summary>
    /// Handler to calculate scores
    /// </summary>
    public class CalculateScoresHandler : IRequestHandler<CalculateScoresRequest, CalculateScoresResponse>
    {
        public Task<CalculateScoresResponse> Handle(CalculateScoresRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
