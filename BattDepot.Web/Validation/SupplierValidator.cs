using System.Text.RegularExpressions;
using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class SupplierValidator : AbstractValidator<Supplier>
    {
        private readonly ICarRepository _repo;

        public SupplierValidator(ICarRepository repo)
        {
            _repo = repo;

            RuleFor(s => s.SupplierType).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(s => s.Code).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(s => s.Code).Must(BeAUniqueCode).WithMessage("'" + Resource.Code + "' " + Resource.AlreadyExist);

            RuleFor(s => s.Name).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(s => s.Email).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Email).Matches(new Regex(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")).WithMessage(Resource.TheEmailAddressEnteredIsInvalid);
            RuleFor(m => m.PostalCode).Matches(new Regex("^[0-9]*$")).WithMessage(Resource.TheNumberFormatIsInvalid);
            RuleFor(m => m.PhoneNumber).Matches(new Regex("^[0-9]*$")).WithMessage(Resource.TheNumberFormatIsInvalid);
            RuleFor(m => m.FaxNumber).Matches(new Regex("^[0-9]*$")).WithMessage(Resource.TheNumberFormatIsInvalid);
        }

        private bool BeAUniqueCode(Supplier carSupplier, string code)
        {
            var existCarSupplier = _repo.GetSupplierByCode(code);
            return existCarSupplier == null || existCarSupplier.ID == carSupplier.ID;
        }
    }
}
