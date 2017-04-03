using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Web.Helpers;
using FluentValidation;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Resources;
using System.Text.RegularExpressions;

namespace Davisoft_BDSProject.Web.Validation
{


    public class BDSAccountValidator : AbstractValidator<BDSAccount>
    {
        private readonly IBDSAccountService _service;
        public BDSAccountValidator(IBDSAccountService service)
        {
            this._service = service;
            RuleFor(m => m.Email).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Email).Matches(new Regex(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")).WithMessage(Resource.TheEmailAddressEnteredIsInvalid);
            RuleFor(u => u.Email).Must((BDSAccount model, string email) =>
            {
                BDSAccount modelDB = _service.GetItem(model.ID);
                if (modelDB == null && _service.CheckItem(model))
                {
                    return false;
                }
                else
                {
                    if (model.Email != email && _service.CheckItem(model))
                    {
                        return false;
                    }
                }
                return true;
            }).WithMessage("'" + Resource.Email + "' " + Resource.AlreadyExist);
            RuleFor(m => m.PassWord).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.PassWord).Must((BDSAccount model, string pass) =>
            {
                if (model.ID == 0 && pass.Trim().Length == 0)
                {
                    return false;
                }
                return true;
            }).WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.RePassWord).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.RePassWord).Must((BDSAccount model, string pass) =>
            {
                if (model.ID == 0  && model.PassWord.Trim().Length > 0 &&  pass!=model.PassWord)
                {
                    return false;
                }
                return true;
            }).WithMessage(Resource.PasswordMismatch);
            RuleFor(m => m.Money).GreaterThan(0).WithMessage(Resource.TheFieldNotEqualTo0);
            RuleFor(m => m.Money).GreaterThanOrEqualTo(0).WithMessage(Resource.TheFieldNotEqualTo0);
            RuleFor(m => m.Point).GreaterThanOrEqualTo(0).WithMessage(Resource.TheFieldNotEqualTo0);
        }
    }
}