using System;
using FluentValidation;
using CPO.Domain.Entities;
using CPO.Domain.Enum;
using Resources;

namespace CPO.Web.Validation
{
    public class ReservationValidator : AbstractValidator<Reservation>
    {
        public ReservationValidator()
        {
            RuleFor(m => m.ReservationFor).NotNull();
            RuleFor(m => m.Charges).InclusiveBetween(Convert.ToDecimal(-0.000001), decimal.MaxValue).WithMessage(Resource.TheValueMustBeGreaterThanZero);
            RuleFor(m => m.Discount).InclusiveBetween(Convert.ToDecimal(-0.000001), decimal.MaxValue).WithMessage(Resource.TheValueMustBeGreaterThanZero);
            RuleFor(m => m.Mileage).InclusiveBetween(Convert.ToDecimal(-0.000001), decimal.MaxValue).WithMessage(Resource.TheValueMustBeGreaterThanZero);
            RuleFor(m => m.Reason).Must(RequiredReason).WithMessage(Resource.TheFieldShouldNotBeEmpty);
        }

        private bool RequiredReason(Reservation reservation, string reason)
        {
            return reservation.ReservationFor != ReservationFor.Other || !string.IsNullOrEmpty(reservation.Reason);
        }
    }
}
