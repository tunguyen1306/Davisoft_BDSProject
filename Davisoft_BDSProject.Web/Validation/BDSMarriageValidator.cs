using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Davisoft_BDSProject.Domain.Entities;
using FluentValidation;
using Resources;

namespace Davisoft_BDSProject.Web.Validation
{
    public class BDSMarriageValidator : AbstractValidator<BDSMarriage>
    {
        public BDSMarriageValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
         
        }
    }
}