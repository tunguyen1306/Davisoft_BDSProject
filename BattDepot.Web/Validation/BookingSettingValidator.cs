using FluentValidation;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class BookingSettingValidator : AbstractValidator<BookingSetting>
    {
        public BookingSettingValidator()
        {
            RuleFor(m => m.BookFromDate).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);                 
            RuleFor(m => m.BookToDate).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.LowRound).GreaterThan(0).WithMessage(Resource.TheFieldShouldGreaterThan + "0");
            RuleFor(m => m.HighRound).GreaterThan(m => m.LowRound).WithMessage(Resource.TheFieldShouldGreaterThan + " {0}", m=>m.LowRound);

        }
    }
}
