using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class PersonalIncomeLevelValidator : AbstractValidator<PersonalIncomeLevel>
    {
        public PersonalIncomeLevelValidator()
        {
            RuleFor(m => m.Code).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Description).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
        }
    }
}