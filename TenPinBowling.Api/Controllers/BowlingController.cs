using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TenPinBowling.Api.Features.CalculateScores;

namespace TenPinBowling.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BowlingController : ControllerBase
    {

        private readonly IMediator _mediator;

        public BowlingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns the calculated frame progress scores and an indicator of whether the game is complete.
        /// </summary>
        /// <param name="request">The calculate score request.</param>
        /// <returns>A <see cref="Task{IActionResult}"/>.</returns>
        [HttpPost("/scores")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CalculateScoresResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> CalculateScores([FromBody] CalculateScoresRequest request)
        {
            CalculateScoresResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
