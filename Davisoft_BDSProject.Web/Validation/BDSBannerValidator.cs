using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Davisoft_BDSProject.Domain.Entities;
using FluentValidation;
using Resources;

namespace Davisoft_BDSProject.Web.Validation
{
    public class BDSBannerValidator : AbstractValidator<BDSBanner>
    {
        public BDSBannerValidator()
        {
        
            RuleFor(m => m.Page).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Position).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Type).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Banner).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
      
        }
    }
}