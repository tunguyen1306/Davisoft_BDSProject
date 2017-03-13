using FluentValidation;
using CPO.Domain.Entities;
using CPO.Domain.Enum;
using Resources;

namespace CPO.Web.Validation
{
    public class AdjustmentValidator : AbstractValidator<Adjustment>
    {

        public AdjustmentValidator()
        {
            RuleFor(m => m.AdjustmentType).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.AdjustmentBy).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.AdjustmentFor).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Amount).GreaterThan(0).WithMessage(Resource.TheAmountBeforeGSTMustGreaterThan0);
            RuleFor(m => m.Remark).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.InvoiceID).Must(CheckInvoice).WithMessage(Resource.InvoiceMustNotBeNull);
            RuleFor(m => m.FcInvoiceID).Must(CheckFCInvoice).WithMessage(Resource.FCInvoiceMustNotBeNull);
        }
        private bool CheckInvoice(Adjustment adjustment, int? invoiceID)
        {
            return adjustment.AdjustmentType != AdjustmentType.Invoice || invoiceID != null;
        }
        private bool CheckFCInvoice(Adjustment adjustment, int? fcinvoiceID)
        {
            return adjustment.AdjustmentType != AdjustmentType.FCInvoice || fcinvoiceID != null;
        }
    }
}
