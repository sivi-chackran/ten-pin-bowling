﻿using MediatR;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TenPinBowling.Api.Features.CalculateScores
{
    /// <summary>
    /// Handler to calculate scores
    /// </summary>
    public class CalculateScoresHandler : IRequestHandler<CalculateScoresRequest, CalculateScoresResponse>
    {
        private readonly IConfiguration _configuration;
        public CalculateScoresHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<CalculateScoresResponse> Handle(CalculateScoresRequest request, CancellationToken cancellationToken)
        {
            var frameIndex = 0;
            var throwIndex = 0;
            var progressScore = 0;

            var frameScores = new List<string>();

            var maxFrames = _configuration.GetValue<int>("MaxFrames");
            var maxPins = _configuration.GetValue<int>("MaxPinsPerThrow");
            
            while (throwIndex < request.PinsDowned.Length && frameIndex < maxFrames)
            {
                var isCalculationPossible = true;

                //Check if all throws from a frame are available
                if (request.PinsDowned.Length > throwIndex + 1)
                {
                    //Strike or Spare
                    if (request.PinsDowned[throwIndex] == maxPins
                       || (request.PinsDowned[throwIndex] + request.PinsDowned[throwIndex + 1] == maxPins))
                    {
                        //check if score calculation is possible
                        if (request.PinsDowned.Length > throwIndex + 2)
                        {
                            progressScore += (request.PinsDowned[throwIndex]
                                 + request.PinsDowned[throwIndex + 1] + request.PinsDowned[throwIndex + 2]);
                        }
                        else
                        {
                            isCalculationPossible = false;
                        }

                        throwIndex += request.PinsDowned[throwIndex] == maxPins ? 1 : 2;
                    }
                    //normal frame
                    else
                    {
                        progressScore += request.PinsDowned[throwIndex]
                            + request.PinsDowned[throwIndex + 1];

                        throwIndex += 2;
                    }
                }
                else
                {
                    isCalculationPossible = false;
                    throwIndex++;
                }

                frameScores.Add(isCalculationPossible ? progressScore.ToString() : "*");

                frameIndex++;
            }
            var response = new CalculateScoresResponse
            {
                FrameProgressScores = frameScores.ToArray(),
                GameCompleted = frameIndex == maxFrames && frameScores.Last() != "*"
            };

            return Task.FromResult(response);
        }
    }
}
