using System;
using FluentValidation;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class ServicePackageValidator : AbstractValidator<ServicePackage>
    {
        public ServicePackageValidator()
        {
            RuleFor(m => m.Description).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.StartDate).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.EndDate).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Cost).InclusiveBetween(Convert.ToDecimal(0), Decimal.MaxValue).WithMessage(Resource.TheValueMustBeGreaterThanOrSame0);
            RuleFor(m => m.Price).InclusiveBetween(Convert.ToDecimal(0), Decimal.MaxValue).WithMessage(Resource.TheValueMustBeGreaterThanOrSame0);
        }
    }
}
