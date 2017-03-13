using System;
using FluentValidation;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class StandardItemValidator : AbstractValidator<StandardItem>
    {
        public StandardItemValidator()
        {
            RuleFor(m => m.Description).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Price).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Price).InclusiveBetween(Convert.ToDecimal(0), decimal.MaxValue).WithMessage(Resource.TheValueMustBeGreaterThanOrSame0);
            RuleFor(m => m.Cost).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Cost).InclusiveBetween(Convert.ToDecimal(0), decimal.MaxValue).WithMessage(Resource.TheValueMustBeGreaterThanOrSame0);
            RuleFor(m => m.StartDate).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.EndDate).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.EndDate).GreaterThanOrEqualTo(m => m.StartDate).WithMessage(Resource.StartDateNotLessThanEndDate);
            //RuleFor(m => m.PrintType).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
        }
    }

    public class OptionalItemValidator : AbstractValidator<OptionalItem>
    {
        public OptionalItemValidator()
        {
            RuleFor(m => m.Description).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Price).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Price).InclusiveBetween(Convert.ToDecimal(0), decimal.MaxValue).WithMessage(Resource.TheValueMustBeGreaterThanOrSame0);
            RuleFor(m => m.Cost).NotEmpty().WithMessage( Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Cost).InclusiveBetween(Convert.ToDecimal(0), decimal.MaxValue).WithMessage(Resource.TheValueMustBeGreaterThanOrSame0);
            RuleFor(m => m.StartDate).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.EndDate).NotEmpty().WithMessage( Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.EndDate).GreaterThanOrEqualTo(m => m.StartDate).WithMessage(Resource.StartDateNotLessThanEndDate);
            RuleFor(m => m.YearOfManufacture).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.SupplierID).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
        }
    }
    public class PromotionItemValidator : AbstractValidator<PromotionItem>
    {
        public PromotionItemValidator()
        {
            RuleFor(m => m.Description).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Price).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Price).InclusiveBetween(Convert.ToDecimal(0), decimal.MaxValue).WithMessage(Resource.TheValueMustBeGreaterThanOrSame0);
            RuleFor(m => m.Cost).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Cost).InclusiveBetween(Convert.ToDecimal(0), decimal.MaxValue).WithMessage(Resource.TheValueMustBeGreaterThanOrSame0);
            RuleFor(m => m.StartDate).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.EndDate).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.EndDate).GreaterThanOrEqualTo(m => m.StartDate).WithMessage(Resource.StartDateNotLessThanEndDate);
        }
    }
}
