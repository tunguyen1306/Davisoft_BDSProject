using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Davisoft_BDSProject.Domain.Entities;
using FluentValidation;
using Resources;

namespace Davisoft_BDSProject.Web.Validation
{
    public class BDSCommentValidator : AbstractValidator<BDSComment>
    {
        public BDSCommentValidator()
        {
            RuleFor(m => m.NameComment).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.DateComment).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.CompanyComment).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.TextComment).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
        }
    }
}