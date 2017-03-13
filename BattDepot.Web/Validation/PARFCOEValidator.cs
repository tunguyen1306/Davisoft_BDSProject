using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;

namespace CPO.Web.Validation
{
    public class PARFCOEValidator : AbstractValidator<PARFCOE>
    {
        private readonly IPARFCOEService _repo;
        public PARFCOEValidator(IPARFCOEService repo)
        {
            _repo = repo;
            RuleFor(m => m.PARFCOEInvoiceNo).NotEmpty().WithMessage("PARF/COE Invoice No must not be null");
            RuleFor(m => m.PARFCOEInvoiceNo).Must(BeAUniqueCode).WithMessage("PARF/COE Invoice number must be unique");
            RuleFor(m => m.Date).NotEmpty().WithMessage("The Date must not not be emty");
            RuleFor(m => m.SupplierID).NotNull().WithMessage("Please choose Supplier");
            RuleFor(m => m.CustomerID).NotNull().WithMessage("Please choose Customer");
            RuleFor(m => m.CompanyCategory).NotNull().WithMessage("Please choose Company Category");
        }

        private bool BeAUniqueCode(PARFCOE parfcoe, string invoiceNo)
        {
            PARFCOE existPARFCOE = _repo.GetPARFCOEByInvoiceNo(invoiceNo);
            return existPARFCOE == null || existPARFCOE.ID == parfcoe.ID;
        }
    }
}
