using System;
using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using CPO.Web.Infrastructure.Helpers;
using CPO.Web.Infrastructure.Utility;
using Resources;

namespace CPO.Web.Validation
{
    public class BookingValidator : AbstractValidator<Booking>
    {
        private readonly ISettingRepository _settingService;
        public BookingValidator(ISettingRepository settingService)
        {
            _settingService = settingService;
            RuleFor(m => m.RegistrationTypeID).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.LoanAmount).Must(LoanAmountValidate).WithMessage(Resource.LoanAmountMustBeAtLeast + " " + Settings.Booking.MinimumLoanAmount().ToMoneyString() + " " + Resource.IfYouWantToApplyForFinanceRebate);
            RuleFor(m => m.LoanTenure).Must(FinanceRebateValidate).WithMessage(Resource.CheckFinanceRebate);
            RuleFor(m => m.LoanTenure).LessThanOrEqualTo(10 * 12).WithMessage(Resource.LoanTenureLessThanOrEqualTo10Years);
            RuleFor(m => m.LoanInterestRate).LessThan(100).WithMessage(Resource.InterestedRateMustLessThan100);
            RuleFor(m => m.RoadTaxPeriod).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.SourceOfLeadID).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.FinanceCompanyID).Must(FinanceIDNotEmpty).WithMessage(Resource.TheFieldShouldNotBeEmpty);
        }

        public bool LoanAmountValidate(Booking booking, decimal amount)
        {
            if (amount == 0)
            {
                return true;
            }
            var minimumLoanAmount = _settingService.GetSetting(Setting.ModuleType.Booking.ToString(),
                Setting.ModuleKey.Booking.MinimumLoanAmount.ToString());
            if (amount >= Convert.ToInt32(minimumLoanAmount.Value))
            {
                return true;
            }
            return false;
        }

        public bool FinanceRebateValidate(Booking booking, int value)
        {
            return ((booking.LoanAmount > 0) == (booking.LoanTenure > 0) 
                //&& (booking.LoanAmount > 0) == (booking.LoanInterestRate > 0)
                );
        }

        public bool FinanceIDNotEmpty(Booking booking, int? value)
        {
            return ((booking.FinanceCompanyID != null) == (booking.LoanAmount > 0));
        }

    }
}
