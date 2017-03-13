using FluentValidation;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class SettingModelQuotationValidator : AbstractValidator<SettingModelQuotation>
    {
        public SettingModelQuotationValidator()
        {
            RuleFor(m => m.CatACoeRebate).GreaterThanOrEqualTo(0).WithMessage(Resource.TheValueMustBeGreaterThanOrSame0);
            RuleFor(m => m.CatBCoeRebate).GreaterThanOrEqualTo(0).WithMessage(Resource.TheValueMustBeGreaterThanOrSame0);
            RuleFor(m => m.CatArebate).GreaterThanOrEqualTo(0).WithMessage(Resource.TheValueMustBeGreaterThanOrSame0);
            RuleFor(m => m.CatBCoeRebate).GreaterThanOrEqualTo(0).WithMessage(Resource.TheValueMustBeGreaterThanOrSame0);
            RuleFor(m => m.FromDate).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.ToDate).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.ToDate).GreaterThan(m => m.FromDate).WithMessage(Resource.ToDateMustGreaterThanFromDate);
            RuleFor(m => m.TandC).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
        }

    }
}
