using System;
using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using CPO.Domain.Enum;
using Resources;

namespace CPO.Web.Validation
{
    public class InvoiceValidator: AbstractValidator<Invoice>
    {
        private readonly IInvoiceService _rebateService;
        public InvoiceValidator(IInvoiceService rebateService)
        {
            _rebateService = rebateService;
        }
        private bool BeAUniqueCode(Invoice rType, string code)
        {
            Invoice existRType = _rebateService.GetInvoice(code);
            return existRType == null || existRType.ID == rType.ID;
        }
    }
    public class RefundValidator : AbstractValidator<Refund>
    {
        private readonly IInvoiceService _rebateService;
        public RefundValidator(IInvoiceService rebateService)
        {
            _rebateService = rebateService;
            //RuleFor(m => m.RefundNo).NotEmpty();
            RuleFor(m => m.RefundDate).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.RefundNo).Must(BeAUniqueCode).WithMessage(Resource.RefundRefNoExists);
            RuleFor(m => m.Amount).GreaterThan(0).WithMessage(Resource.TheFieldShouldGreaterThan + " 0");
            RuleFor(m => m.Amount).Must(CheckWithEqual).WithMessage(Resource.TheAmountSmallerBalance);
        }

        private bool CheckWithEqual(Refund refund, decimal amount)
        {
            if (refund.RefundType == RefundType.Invoice)
            {
                var invoice = _rebateService.GetInvoice(Convert.ToInt32(refund.InvoiceID));
                if (amount > (invoice.CustomerPay - invoice.CustomerDue))
                    return false;
                return true;
            }
            return true;
        }

        private bool BeAUniqueCode(Refund rType, string code)
        {
            Refund existRType = _rebateService.GetRefund(code);
            return existRType == null || existRType.ID == rType.ID;
        }
    }
    public class CreditValidator : AbstractValidator<Credit>
    {
        private readonly IInvoiceService _rebateService;
        public CreditValidator(IInvoiceService rebateService)
        {
            _rebateService = rebateService;
          
            RuleFor(m => m.CreditNo).Must(BeAUniqueCode).WithMessage(Resource.CreditNoExist);
            RuleFor(m => m.InvoiceID).Must(HasID).WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.FcInvoiceID).Must(HasID).WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.AmountAfterGST).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.AmountAfterGST).InclusiveBetween(Convert.ToDecimal(0.00000000000000000001),decimal.MaxValue).WithMessage(Resource.TheFieldShouldGreaterThan + " 0");
            RuleFor(m => m.CreditBy).NotEmpty().WithMessage(Resource.PleaseChooseFromList);
            RuleFor(m => m.CreditType).NotEmpty().WithMessage(Resource.PleaseChooseFromList);
        }

        private bool BeAUniqueCode(Credit rType, string code)
        {
            Credit existRType = _rebateService.GetCredit(code);
            return existRType == null || existRType.ID == rType.ID;
        }
        private bool HasID(Credit rType, int? code)
        {
            bool valid = !(rType.FcInvoiceID == null && rType.InvoiceID == null);
            return valid;
        }
    }
    public class DebitValidator : AbstractValidator<Debit>
    {
        private readonly IInvoiceService _rebateService;
        public DebitValidator(IInvoiceService rebateService)
        {
            _rebateService = rebateService;

            RuleFor(m => m.DebitNo).Must(BeAUniqueCode).WithMessage(Resource.DebitNoExists);
            RuleFor(m => m.InvoiceID).Must(HasID).WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.FcInvoiceID).Must(HasID).WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.AmountAfterGST).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.AmountAfterGST).InclusiveBetween(Convert.ToDecimal(0.00000000000000000001), decimal.MaxValue).WithMessage(Resource.TheFieldShouldGreaterThan + " 0");
            RuleFor(m => m.DebitBy).NotEmpty().WithMessage(Resource.PleaseChooseFromList);
            RuleFor(m => m.DebitType).NotEmpty().WithMessage(Resource.PleaseChooseFromList);
        }

        private bool BeAUniqueCode(Debit rType, string code)
        {
            Debit existRType = _rebateService.GetDebit(code);
            return existRType == null || existRType.ID == rType.ID;
        }
        private bool HasID(Debit rType, int? code)
        {
            bool valid = !(rType.FcInvoiceID == null && rType.InvoiceID == null);
            return valid;
        }
    }
}
