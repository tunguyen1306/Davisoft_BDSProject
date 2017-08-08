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
    public class BDSPersonalInfomationValidator : AbstractValidator<BDSPersonalInformation>
    {
        private readonly IBDSAccountService _service;
        public BDSPersonalInfomationValidator(IBDSAccountService service)
        {
            _service = service;
           

            RuleFor(m => m.Name).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Birthday).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
          
            RuleFor(m => m.Phone).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(u => u.Phone).Matches(new Regex("^[0-9]*$")).WithMessage(Resource.OnlyNumberAllowed);
            RuleFor(m => m.Sex).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.MaritalStatus).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.TemporaryAddress).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.PermanentAddress).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Province).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.UrlImage).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
           
          
       
        
         
          
        }
    }
}