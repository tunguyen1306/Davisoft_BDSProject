using System;
using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class RebateValidator:AbstractValidator<Rebate>
    {
        private readonly IRebateService _repo;
        public RebateValidator(IRebateService repo)
        {
            _repo = repo;
            RuleFor(m => m.Amount).InclusiveBetween(Convert.ToDecimal(0.0000001), Decimal.MaxValue).WithMessage(Resource.TheValueMustBeGreaterThanZero);
            //RuleFor(m => m.Code).NotEmpty();
            //RuleFor(m => m.Code).Must(BeAUniqueCode).WithMessage("Rebate with same code already exists");
        }
        //private bool BeAUniqueCode(Rebate rType, string code)
        //{
        //    Rebate existRType = _repo.GetRebate(code);

        //    return existRType == null || existRType.ID == rType.ID;
        //}
    }
    public class RebateItemValidator : AbstractValidator<RebateItem>
    {
        private readonly IRebateService _rebateService;
        public RebateItemValidator(IRebateService rebateService)
        {
            _rebateService = rebateService;
            RuleFor(m => m.CertificateNo).NotEmpty();
            RuleFor(m => m.CertificateNo).Length(13).WithMessage("The Cert. No must be exactly 13 characters");
            RuleFor(m => m.CertificateNo).Must(checkformat).WithMessage("Please enter a valid Cert. No format");
            RuleFor(m => m.CertificateNo).Must(BeAUniqueCode).WithMessage("RebateItem with same Certificate No already exists");
            RuleFor(m => m.Amount).Must(m => m > 0).WithMessage("Amount is wrong");
        }

        public bool checkformat(string str)
        {
            const string parten = "[0-9]{7}[CP]{2}[0-9]{4}";
            return System.Text.RegularExpressions.Regex.IsMatch(str, parten);
        }
        private bool BeAUniqueCode(RebateItem rType, string code)
        {
            RebateItem existRType = _rebateService.GetRebateItem(code);

            return existRType == null || existRType.ID == rType.ID;
        }
    }
}
