using FluentValidation;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class QuotationAttributeValidator : AbstractValidator<QuotationAttribute>
    {
        public QuotationAttributeValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Description).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
        }
    }
}
