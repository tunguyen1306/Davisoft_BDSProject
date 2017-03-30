using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Davisoft_BDSProject.Domain.Entities;
using FluentValidation;
using Resources;

namespace Davisoft_BDSProject.Web.Validation
{
    public class BDSBankValidator : AbstractValidator<BDSBank>
    {
        public BDSBankValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.AccountName).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.AccountNumber).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Branch).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
        }
    }
}