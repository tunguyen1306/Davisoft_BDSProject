using System.Text.RegularExpressions;
using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class BranchValidator : AbstractValidator<Branch>
    {
        private readonly IUnitRepository _repo;
        public BranchValidator(IUnitRepository repo)
        {
            _repo = repo;
            RuleFor(m => m.Code).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Name).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Code).Must(BeAUniqueCode).WithMessage(Resource.BranchCodeExists);
            RuleFor(m => m.PhoneNumber).Matches(new Regex(@"^[0-9\+]{1,}[0-9\-\ ]{3,15}$")).WithMessage(Resource.TheFieldIsInvalid);
            RuleFor(m => m.FaxNumber).Matches(new Regex(@"^[0-9\+]{1,}[0-9\-\ ]{3,15}$")).WithMessage(Resource.TheFieldIsInvalid);
            RuleFor(m => m.TaxNumber).Matches(new Regex(@"^[0-9\-\ ]*$")).WithMessage(Resource.TheFieldIsInvalid);

        }
        private bool BeAUniqueCode(Branch rType, string code)
        {
            Branch exist = _repo.GetBranchByCode(code);

            return exist == null || exist.ID == rType.ID;
        }
    }
}
