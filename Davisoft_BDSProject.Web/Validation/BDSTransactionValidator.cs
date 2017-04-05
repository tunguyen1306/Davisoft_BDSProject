using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Web.Helpers;
using FluentValidation;
using Resources;

namespace Davisoft_BDSProject.Web.Validation
{
    public class BDSTransactionValidator : AbstractValidator<BDSTransaction>
    {
        public BDSTransactionValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m. IdAccount).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Type).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.IdBank).Must((BDSTransaction model, int? IdBank) =>
            {
                if (model.Type==1 && !IdBank.HasValue)
                {
                    return false;
                }
                return true;
            }).WithMessage(Resource.TheFieldShouldNotBeEmpty);

            RuleFor(m => m.IdBranch).Must((BDSTransaction model, int? IdBranch) =>
            {
                if (model.Type == 2 && !IdBranch.HasValue)
                {
                    return false;
                }
                return true;
            }).WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Money).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Money).GreaterThanOrEqualTo(20000).WithMessage(String.Format(Utilities.Resource("TheValueMustBeGreaterThanValueSFM", "The value must be greater than {0}"),"20,0000.00"));
            
        }
    }
}