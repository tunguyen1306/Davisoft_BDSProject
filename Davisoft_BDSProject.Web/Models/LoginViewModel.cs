using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Attributes;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Web.Infrastructure.Utility;
using Resources;

namespace Davisoft_BDSProject.Web.Models
{
    [Validator(typeof(LoginViewModelValidator))]
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public int CountryID { get; set; }
    }

    internal class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(m => m.Email).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Password).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
        }
    }
}
