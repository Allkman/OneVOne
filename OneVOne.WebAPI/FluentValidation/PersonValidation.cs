using FluentValidation;
using OneVOne.GameService.Core.Entities;

namespace OneVOne.WebAPI.FluentValidation
{
    public class PersonValidation : AbstractValidator<Person>
    {
        public PersonValidation()
        {
            RuleFor(person => person.FirstName).NotNull();
            RuleFor(person => person.LastName).NotNull();
        }
    }
}
