using FluentValidation;
using OneVOne.GameService.Core.Entities;

namespace OneVOne.WebAPI.FluentValidation
{
    public class GameValidation : AbstractValidator<Game>
    {
        public GameValidation()
        {
            RuleFor(x => x.PlayerOne).NotEmpty();
            RuleFor(x => x.PlayerTwo).NotEmpty();
            RuleFor(x => x.PlayerTwoTotalScore)
                .NotEmpty()
                .Must(x => (x >=0 && x<=12));
            RuleFor(x => x.PlayerOneTotalScore)
                .NotEmpty()
                .Must(x => (x >= 0 && x <= 12));
            RuleFor(x => x.GameTime).NotEmpty();
        }
    }
}
