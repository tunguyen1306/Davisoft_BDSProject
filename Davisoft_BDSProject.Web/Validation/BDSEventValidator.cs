using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Web.Helpers;
using FluentValidation;
using OfficeOpenXml.FormulaParsing.Utilities;
using Resources;

namespace Davisoft_BDSProject.Web.Validation
{
    public class BDSEventValidator : AbstractValidator<BDSEvent>
    {
        public BDSEventValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.DisPercent).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.DisPercent.ToString("n2"))
                .Matches(new Regex("^[0-9]*$"))
                .WithMessage(Utilities.Resource("OnlyNumberAllowed", "The field should number"));
            RuleFor(m => m.DisPercent).InclusiveBetween(0, 100).WithMessage(String.Format(Utilities.Resource("TheFieldShouldBetween", "The field should between {0} and {1}"),0,100));
            RuleFor(m => m.FromDateToDate).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
         
        }
    }
}