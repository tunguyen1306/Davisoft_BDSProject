using System.Text.RegularExpressions;
using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class DealerValidator : AbstractValidator<Dealer>
    {
        private static IOfficeService _repo;
        public DealerValidator(IOfficeService repo)
        {
            _repo = repo;
            RuleFor(m => m.DealerName).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.DatabaseName).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.CountryID).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.TimeZone).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.TaxNumber).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.TaxNumber).Matches(new Regex(@"^[0-9\-\ ]*$")).WithMessage(Resource.TheFieldIsInvalid);
            RuleFor(m => m.TaxNumber).Must(BeAUniqueTaxNumber).WithMessage(Resource.TaxNumberExists);
            RuleFor(m => m.Email).Matches(new Regex(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")).WithMessage(Resource.TheEmailAddressEnteredIsInvalid);
            RuleFor(m => m.Phone).Matches(new Regex(@"^[0-9\+]{1,}[0-9\-\ ]{3,15}$")).WithMessage(Resource.TheFieldIsInvalid);
            RuleFor(m => m.MobilePhone).Matches(new Regex(@"^[0-9\+]{1,}[0-9\-\ ]{3,15}$")).WithMessage(Resource.TheFieldIsInvalid);
            RuleFor(m => m.FaxNo).Matches(new Regex(@"^[0-9\+]{1,}[0-9\-\ ]{3,15}$")).WithMessage(Resource.TheFieldIsInvalid);

        }
        private bool BeAUniqueTaxNumber(Dealer customer, string taxNumber)
        {
            if (string.IsNullOrEmpty(taxNumber)) return true;
            var dealer = _repo.GetDealerByTaxNo(taxNumber);
            return dealer == null || dealer.ID == customer.ID;
        }

    }
}
