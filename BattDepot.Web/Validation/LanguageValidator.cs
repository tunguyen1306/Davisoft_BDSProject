using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using Resources;

namespace Davisoft_BDSProject.Web.Validation
{
    public class LanguageValidator : AbstractValidator<Language>
    {
        private readonly IUnitRepository _repo;

        public LanguageValidator(IUnitRepository repo)
        {
            _repo = repo;
            RuleFor(m => m.Value).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleSet("Edit", () => RuleFor(m => m.DisplayName).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty));

        }
    }
}