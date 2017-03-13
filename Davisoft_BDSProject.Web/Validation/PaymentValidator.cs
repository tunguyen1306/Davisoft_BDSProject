using System;
using System.Text.RegularExpressions;
using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using CPO.Domain.Enum;
using Resources;

namespace CPO.Web.Validation
{
    public class PaymentValidator : AbstractValidator<Payment>
    {
        private readonly IUnitRepository _unitService;
        public PaymentValidator(IUnitRepository unitService)
        {
            _unitService = unitService;
            RuleFor(m => m.PaymentBy).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.PaymentMethodID).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.PaymentTypeID).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.BookingID).Must(CheckBooking).WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.InvoiceID).Must(CheckInvoice).WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.FCInvoiceID).Must(CheckFCInvoice).WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.PaymentPayee.Value).Must(CheckPayee).WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.PayeeName).Must(CheckPayeeName).WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.ChequeNumber).Must(CheckChequeNo).WithMessage(Resource.ChequeNoInvalid);
            RuleFor(m => m.CreditCardNo).Must(CheckCreditCardNo).WithMessage(Resource.CreditNoMust16Character);
            RuleFor(m => m.CreditCardNo).Matches(new Regex("^[0-9]*$")).WithMessage(Resource.OnlyNumberAllowed);
//            RuleFor(m => m.ParfcoeNumber).Must(CheckRebate).WithMessage("PARF/COE No. is wrong");
            //RuleFor(m => m.CustomerID).Must(CusNotNull).WithMessage("Customer must not be null");
            //RuleFor(m => m.FinancialID).Must(FinanceNotNull).WithMessage("Financial must not be null");
            RuleFor(m => m.PaymentDate).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.DisbursementDate).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Amount).GreaterThan(0).WithMessage(Resource.TheFieldShouldGreaterThan + " 0");
            //RuleFor(m => m.Amount).InclusiveBetween(Convert.ToDecimal(0.0000001), Decimal.MaxValue).WithMessage(Resource.TheFieldShouldGreaterThan + " 0");
        }

        private bool FinanceNotNull(Payment payment, int? financeid)
        {
            return payment.PaymentBy == PaymentBy.Customer || (payment.PaymentBy == PaymentBy.Financial && financeid > 0);
        }

        private bool CusNotNull(Payment payment, int? customerid)
        {
            return payment.PaymentBy == PaymentBy.Financial || (payment.PaymentBy == PaymentBy.Customer && customerid > 0);
        }

        private bool CheckBooking(Payment payment, int? bookingID)
        {
            return payment.PaymentFor != PaymentFor.Booking || bookingID != null;
        }
        private bool CheckInvoice(Payment payment, int? invoiceID)
        {
            return payment.PaymentFor != PaymentFor.Invoice || invoiceID != null;
        }
        private bool CheckFCInvoice(Payment payment, int? fcinvoiceID)
        {
            return payment.PaymentFor != PaymentFor.FCInvoice || fcinvoiceID != null;
        }
        private bool CheckPayee(Payment payment, string payee)
        {
            return payment.PaymentFor != PaymentFor.Booking || (!string.IsNullOrEmpty(payee) && payee != "0");
        }
        private bool CheckPayeeName(Payment payment, string payeeName)
        {
            return payment.PaymentPayee != PaymentPayee.Others || !string.IsNullOrEmpty(payeeName);
        }

        private bool CheckChequeNo(Payment payment, string chequeNo)
        {
            return payment.PaymentMethodID != _unitService.GetPaymentMethod(PaymentMethodCode.PayByCheque).ID || !string.IsNullOrEmpty(chequeNo);
        }
        private bool CheckCreditCardNo(Payment payment, string creditCardNo)
        {
            return payment.PaymentMethodID != _unitService.GetPaymentMethod(PaymentMethodCode.PayByCreditCard).ID || (!string.IsNullOrEmpty(creditCardNo) && creditCardNo.Length == 16);
        }
        private bool CheckRebate(Payment payment, string parfcoeNumber)
        {
            return payment.PaymentMethodID != _unitService.GetPaymentMethod(PaymentMethodCode.PayByPcRebate).ID || !string.IsNullOrEmpty(parfcoeNumber);
        }
    }



    public class PaymentByCreditCardValidator : AbstractValidator<PaymentByCreditCard>
    {
        public PaymentByCreditCardValidator()
        {
            RuleFor(m => m.CreditCardNo).NotNull();
        }
    }

    public class PaymentByChequeValidator : AbstractValidator<PaymentByCheque>
    {
        public PaymentByChequeValidator()
        {
            RuleFor(m => m.ChequeNumber).NotNull();
        }
    }

    public class PaymentByNETSValidator : AbstractValidator<PaymentByNETS>
    {
        public PaymentByNETSValidator()
        {
        }
    }

    public class PaymentByCashValidator : AbstractValidator<PaymentByCash>
    {
        public PaymentByCashValidator()
        {
        }
    }

    public class PaymentByBankTransferValidator : AbstractValidator<PaymentByBankTransfer>
    {
        public PaymentByBankTransferValidator()
        {
            RuleFor(m => m.BankTransferNo).NotEmpty();
        }
    }

    public class PaymentByDuoPointValidator : AbstractValidator<PaymentByDuoPoint>
    {
        public PaymentByDuoPointValidator()
        {
            RuleFor(m => m.Voucher).NotEmpty();
        }
    }
}
