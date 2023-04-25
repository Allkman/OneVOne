using FluentValidation;
using OneVOne.GameService.Core.Entities;

namespace OneVOne.WebAPI.FluentValidation
{
    public class PlayerValidation : AbstractValidator<Player>
    {
        public PlayerValidation()
        {
            RuleFor(player => player.Person).NotEmpty();
            RuleFor(player => player.InsideScoring)
                .Must(x => x == null || (x >= 1 && x <= 100))
                .WithMessage("InsideScoring must be between 1 and 100 or null.");
            RuleFor(player => player.OutsideScoring)
                .Must(x => x == null || (x >= 1 && x <= 100))
                .WithMessage("OutsideScoring must be between 1 and 100 or null.");
            RuleFor(player => player.Defending)
                .Must(x => x == null || (x >= 1 && x <= 100))
                .WithMessage("Defending must be between 1 and 100 or null.");
            RuleFor(player => player.Rebounding)
                .Must(x => x == null || (x >= 1 && x <= 100))
                .WithMessage("Rebounding must be between 1 and 100 or null.");
            RuleFor(player => player.Playmaking)
                .Must(x => x == null || (x >= 1 && x <= 100))
                .WithMessage("Playmaking must be between 1 and 100 or null.");
            RuleFor(player => player.Athleticism)
                .Must(x => x == null || (x >= 1 && x <= 100))
                .WithMessage("Athleticism must be between 1 and 100 or null.");
        }
    }
}
