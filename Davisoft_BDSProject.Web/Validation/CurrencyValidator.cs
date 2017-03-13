using FluentValidation;
using Davisoft_BDSProject.Domain.Entities;
using Resources;

namespace Davisoft_BDSProject.Web.Validation
{
    public class CurrencyValidator : AbstractValidator<Currency>
    {
        public CurrencyValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Symbol).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
        }
    }
}
