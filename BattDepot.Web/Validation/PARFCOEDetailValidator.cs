using System;
using FluentValidation;
using CPO.Domain.Entities;
using CPO.Domain.Helpers;

namespace CPO.Web.Validation
{
    public class PARFCOEDetailValidator : AbstractValidator<PARFCOEDetail>
    {
        public PARFCOEDetailValidator()
        {
            RuleFor(m => m.CertificateNo).NotEmpty().WithMessage("Certificate No. must not be null");
            RuleFor(m => m.CertificateNo).Length(13).WithMessage("The Cert. No must be exactly 13 characters");
            RuleFor(m => m.CertificateNo).Must(checkformat).WithMessage("Please enter a valid Parf/COE Cert. No format");
            RuleFor(m => m.ExpiryDate).NotNull();
            RuleFor(m => m.ExpiryDate).GreaterThan(UserDateTime.Today).WithMessage("Expire date must greater than current date");
        }
        public bool checkformat(string str)
        {
            const string parten = "[0-9]{7}[CP]{2}[0-9]{4}";
            return System.Text.RegularExpressions.Regex.IsMatch(str, parten);
        }
    }
}
