using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Davisoft_BDSProject.Domain.Entities;
using FluentValidation;
using Resources;

namespace Davisoft_BDSProject.Web.Validation
{
    public class BDSSalaryValidator : AbstractValidator<BDSSalary>
    {
        public BDSSalaryValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Type).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
             RuleFor(m => m.FromSalary).Must((BDSSalary model,int FromSalary) => {
                 if ((model.Type == 1 || model.Type == 2 || model.Type == 3) && model.FromSalary > 0)
                                                                                     {
                                                                                         return true;
                                                                                     }
                 else if (model.Type==4)
                 {
                     return true;
                 }
                                                                                     return false;
             }).WithMessage(Resource.TheFieldNotEqualTo0);
             RuleFor(m => m.ToSalary).Must((BDSSalary model, int ToSalary) =>
             {
                 if (model.Type == 2 && model.ToSalary > 0)
                 {
                     return true;
                 }
                 else if (model.Type == 4)
                 {
                     return true;
                 }
                 return false;
             }).WithMessage(Resource.TheFieldNotEqualTo0);
          
        }
    }
}