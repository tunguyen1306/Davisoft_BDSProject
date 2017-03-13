using System.Text.RegularExpressions;
using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class FinanceCompanyValidator : AbstractValidator<FinanceCompany>
    {
        private readonly IUnitRepository _repo;
        public FinanceCompanyValidator(IUnitRepository repo)
        {
            _repo = repo;
            RuleFor(m => m.Code).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Code).Must(BeAUniqueCode_Finance).WithMessage(Resource.CompanyCodeExists);
            RuleFor(m => m.Name).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Email).EmailAddress().WithMessage(Resource.EmailFormatInvalid);
            RuleFor(m => m.PhoneNumber).Must(IsNumber).WithMessage(Resource.FieldNumber);
            RuleFor(m => m.FaxNumber).Must(IsNumber).WithMessage(Resource.FieldNumber);
            RuleFor(m => m.ReferenceContact).Must(IsNumber).WithMessage(Resource.FieldNumber);

        }
        private bool BeAUniqueCode_Finance(FinanceCompany cpn, string code)
        {
            FinanceCompany existRType = _repo.GetFinanceCompanyByCode(code);

            return existRType == null || existRType.ID == cpn.ID;
        }
        private bool IsNumber(string str)
        {
            if (str == null)
                return true;
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(str);
        }
    }
    public class InsuranceCompanyValidator : AbstractValidator<InsuranceCompany>
    {
        private readonly IUnitRepository _repo;
        public InsuranceCompanyValidator(IUnitRepository repo)
        {
            _repo = repo;
            RuleFor(m => m.Code).NotEmpty();
            RuleFor(m => m.Code).Must(BeAUniqueCode_Insurance).WithMessage("Company Code with same code already exists.");
            RuleFor(m => m.Name).NotEmpty();
            RuleFor(m => m.Email).EmailAddress();
            RuleFor(m => m.PhoneNumber).Must(IsNumber).WithMessage("Phone is must number.");
            RuleFor(m => m.FaxNumber).Must(IsNumber).WithMessage("Fax is must number.");
            RuleFor(m => m.ReferenceContact).Must(IsNumber).WithMessage("Reference is must number.");
        }
        private bool BeAUniqueCode_Insurance(InsuranceCompany cpn, string code)
        {
            InsuranceCompany existRType = _repo.GetInsuranceCompanyByCode(code);

            return existRType == null || existRType.ID == cpn.ID;
        }
        private bool IsNumber(string str)
        {
            if (str == null)
                return true;
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(str);
        }
    }
}
