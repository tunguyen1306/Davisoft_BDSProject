using FluentValidation;
using CPO.Domain.Entities;
using CPO.Web.Infrastructure.Helpers;
using CPO.Web.Infrastructure.Utility;
using Resources;

namespace CPO.Web.Validation
{
    public class QuotationValidator : AbstractValidator<Quotation>
    {
        public QuotationValidator()
        {
            //            RuleFor(m => m.COEType).NotNull();
            RuleFor(m => m.RegistrationTypeID).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.LoanAmount).Must(LoanAmountValidate).WithMessage(Resource.LoanAmountMustBeAtLeast + " " + Settings.Booking.MinimumLoanAmount().ToMoneyString() + " " + Resource.IfYouWantToApplyForFinanceRebate);
            RuleFor(m => m.LoanTenure).Must(FinanceRebateValidate).WithMessage(Resource.TheValueMustBeGreaterThanZero);
            RuleFor(m => m.LoanTenure).LessThanOrEqualTo(10).WithMessage(Resource.LoanTenureLessThanOrEqualTo10Years);
            RuleFor(m => m.RegisteredAddress).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.LoanInterestRate).LessThan(100).WithMessage(Resource.TheInterestedRateMustLessThan100);
            RuleFor(m => m.RoadTaxPeriod).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.FinanceCompanyID).Must(FinanceIDNotEmpty).WithMessage(Resource.TheFieldShouldNotBeEmpty);
        }

        public bool LoanAmountValidate(Quotation booking, decimal amount)
        {
            return amount == 0 || amount >= 30000;
        }

        public bool FinanceRebateValidate(Quotation booking, int value)
        {
            return ((booking.LoanAmount > 0) == (booking.LoanTenure > 0) && (booking.LoanAmount > 0) == (booking.LoanInterestRate > 0));
        }

        public bool FinanceIDNotEmpty(Quotation quotation, int? value)
        {
            return ((quotation.FinanceCompanyID != null) == (quotation.LoanAmount > 0));
        }
    }
}
