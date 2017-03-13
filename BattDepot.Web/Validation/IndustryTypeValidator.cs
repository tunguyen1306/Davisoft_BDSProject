using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class IndustryTypeValidator : AbstractValidator<IndustryType>
    {
        public IndustryTypeValidator()
        {
            RuleFor(m => m.Code).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Description).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
        }
    }
}