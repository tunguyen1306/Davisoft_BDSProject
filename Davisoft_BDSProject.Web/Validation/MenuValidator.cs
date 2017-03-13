using FluentValidation;
using Davisoft_BDSProject.Domain.Entities;
using Resources;

namespace Davisoft_BDSProject.Web.Validation
{
    public class MenuValidator : AbstractValidator<Menu>
    {
        public MenuValidator()
        {
            RuleFor(m => m.Title).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
        }
    }
}
