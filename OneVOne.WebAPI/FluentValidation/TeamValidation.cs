using FluentValidation;
using OneVOne.GameService.Core.Entities;

namespace OneVOne.WebAPI.FluentValidation
{
    public class TeamValidation : AbstractValidator<Team>
    {
        public TeamValidation()
        {
            RuleFor(team => team.TeamName).NotEmpty();
            RuleFor(team => team.Country).NotEmpty();
            RuleFor(team => team.City).NotEmpty();
            RuleFor(team => team.Abbreviation).NotEmpty();
        }
    }
}
