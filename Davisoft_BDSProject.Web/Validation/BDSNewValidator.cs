using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Web.Helpers;
using FluentValidation;
using Resources;

namespace Davisoft_BDSProject.Web.Validation
{
    public class BDSNewValidator : AbstractValidator<BDSNew>
    {
        private readonly IBDSEmployerInformationService _service;
        public BDSNewValidator(IBDSEmployerInformationService service)
        {
            this._service = service;
            RuleFor(m => m.Title).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.AddressWork).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Quantity).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.FromSalary).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.ToSalary).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Bonus).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Sex).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.IdTimeWork).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.IdEducation).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Career).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.DesCompany).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.DesWork).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.TimeProbationary).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.FromAge).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.ToAge).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);

         
            RuleFor(m => m.NameCompany).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);

            RuleFor(m => m.AddressApply).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);


            RuleFor(m => m.NamesContact).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);

            RuleFor(m => m.PhoneContact).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);

            RuleFor(m => m.Email).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);


            RuleFor(m => m.ToDeadline).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);

            RuleFor(m => m.Email).Matches(new Regex(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")).WithMessage(Resource.TheEmailAddressEnteredIsInvalid);

            RuleFor(m => m.IdLanguage).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);

            RuleFor(m => m.IdTypeNews).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);

            RuleFor(m => m.FromDateToDateString).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);

            RuleFor(m => m.TotalMoney).Must((BDSNew model, double money) =>
            {
                var account = _service.GetItem(model.IdAcount);
                if (model.ID==0 && account.BDSAccount.Money<money)
                {
                    return false;
                }
                return true;
            }).WithMessage(Utilities.Resource("ValidateNewMoney", "Over due money"));
          
        }
    }
}