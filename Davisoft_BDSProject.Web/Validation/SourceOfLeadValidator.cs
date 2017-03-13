using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class SourceOfLeadValidator: AbstractValidator<SourceOfLead>
    {
        public SourceOfLeadValidator()
        {
            RuleFor(m => m.Description).NotEmpty().WithMessage(Resource.TheFileMustNotBeEmpty);
        }
    }
}