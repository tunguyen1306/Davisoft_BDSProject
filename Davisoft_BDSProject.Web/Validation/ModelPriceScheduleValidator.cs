using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class ModelPriceScheduleValidator : AbstractValidator<ModelPriceSchedule>
    {

        public ModelPriceScheduleValidator()
        {
            RuleFor(m => m.ModelYear).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.ModelYear).GreaterThan(0).WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.ModelID).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.ModelID).GreaterThan(0).WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.EffectiveDate).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
        }
    }
}