using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using FluentValidation;
using Resources;

namespace Davisoft_BDSProject.Web.Validation
{
    public class BDSEmployerInformationValidator : AbstractValidator<BDSEmployerInformation>
    {
        private readonly IBDSAccountService _service;
        public BDSEmployerInformationValidator(IBDSAccountService service)
        {
            _service = service;
            //RuleFor(m => m.BDSAccount.Email).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            //RuleFor(m => m.BDSAccount.Email).Matches(new Regex(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")).WithMessage(Resource.TheEmailAddressEnteredIsInvalid);
            //RuleFor(m => m.BDSAccount.Email).Must((BDSEmployerInformation model, string email) =>
            //{
            //    BDSAccount modelDB = _service.GetItem(model.ID);
            //    if (modelDB == null && _service.CheckItem(model.BDSAccount))
            //    {
            //        return false;
            //    }
            //    else
            //    {
            //        if (model.BDSAccount.Email != email && _service.CheckItem(model.BDSAccount))
            //        {
            //            return false;
            //        }
            //    }
            //    return true;
            //}).WithMessage("'" + Resource.Email + "' " + Resource.AlreadyExist);

            //RuleFor(m => m.BDSAccount.PassWord).Must((BDSEmployerInformation model, string pass) =>
            //{
            //    if (model.BDSAccount.ID == 0 && pass.Trim().Length == 0)
            //    {
            //        return false;
            //    }
            //    return true;
            //}).WithMessage(Resource.TheFieldShouldNotBeEmpty);
            //RuleFor(m => m.BDSAccount.Money).GreaterThan(0).WithMessage(Resource.TheFieldNotEqualTo0);
            //RuleFor(m => m.BDSAccount.Money).GreaterThanOrEqualTo(0).WithMessage(Resource.TheFieldNotEqualTo0);
            //RuleFor(m => m.BDSAccount.Point).GreaterThanOrEqualTo(0).WithMessage(Resource.TheFieldNotEqualTo0);

            RuleFor(m => m.Name).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Scope).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.UrlImage).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Address).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
          
            RuleFor(m => m.City).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.District).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
          
            RuleFor(u => u.Fax).Matches(new Regex("^[0-9]*$")).WithMessage(Resource.OnlyNumberAllowed);
            RuleFor(m => m.Phone).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(u => u.Phone).Matches(new Regex("^[0-9]*$")).WithMessage(Resource.OnlyNumberAllowed);


            RuleFor(m => m.NameContact).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.EmailContact).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.EmailContact).Matches(new Regex(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")).WithMessage(Resource.TheEmailAddressEnteredIsInvalid);
            RuleFor(m => m.PhoneContact).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(u => u.PhoneContact).Matches(new Regex("^[0-9]*$")).WithMessage(Resource.OnlyNumberAllowed);
            RuleFor(m => m.AddressContact).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.CityContact).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.DistrictContact).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.TypeContact).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
          
        }
    }
}