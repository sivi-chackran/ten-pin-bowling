using FluentValidation;
using TenPinBowling.Common.Model.Config;

namespace TenPinBowling.Api.Features.CalculateScores
{       
    /// <summary>
    /// Validates the calculate score request.
    /// </summary>
    public class CalculateScoresValidator : AbstractValidator<CalculateScoresRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalculateScoresValidator"/> class.
        /// </summary>
        /// <param name="configuration">The configuration</param>
        public CalculateScoresValidator(AppSettings appSettings)
        {
            //Validate if PinsDowned is empty
            RuleFor(m => m.PinsDowned).NotEmpty().WithMessage("Please enter the pins downed");

            //Validate if the total pins downed in a throw does not exceed maximum pins per throw 
            var maxPinsPerThrow = appSettings.MaxPinsPerFrame;
            RuleForEach(m => m.PinsDowned).InclusiveBetween(0, maxPinsPerThrow)
                .WithMessage($"Pins downed should be between 0 and {maxPinsPerThrow}");

            //Validate if the total throws do not exceed the maximum allowed throws 
            var maxThrows = appSettings.MaxThrows;
            RuleFor(m => m.PinsDowned.Length).LessThanOrEqualTo(maxThrows)
                .WithMessage($"Number of throws cannot exceed {maxThrows}");

        }
    }
}
