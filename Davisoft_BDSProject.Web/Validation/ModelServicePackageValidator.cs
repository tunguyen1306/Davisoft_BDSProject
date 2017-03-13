using System;
using FluentValidation;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class ModelServicePackageValidator : AbstractValidator<ModelServicePackage>
    {
        public ModelServicePackageValidator()
        {
            RuleFor(m => m.Price).InclusiveBetween(Convert.ToDecimal(0), Decimal.MaxValue).WithMessage(Resource.TheFieldGreaterThanOrSame + " 0.");
        }
    }
}
