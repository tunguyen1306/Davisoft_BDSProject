using FluentValidation;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class RegistrationValidator : AbstractValidator<Registration>
    {
        public RegistrationValidator()
        {
            RuleFor(m => m.RegistrationNo).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.RegistrationDate).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            //RuleFor(m => m.Retained).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.PlateTypeID).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
        }
    }
}
