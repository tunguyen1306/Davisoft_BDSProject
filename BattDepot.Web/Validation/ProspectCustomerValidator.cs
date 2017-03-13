using System;
using System.Text.RegularExpressions;
using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using CPO.Domain.Enum;
using Resources;

namespace CPO.Web.Validation
{
    public class ProspectCustomerValidator : AbstractValidator<ProspectCustomer>
    {
        private readonly ICustomerService _customerService;
        private readonly IUnitRepository _unitRepository;
        public ProspectCustomerValidator(ICustomerService customerService, IUnitRepository unitRepository)
        {
            _customerService = customerService;
            _unitRepository = unitRepository;
            RuleFor(m => m.CustomerTypeID).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            //            RuleFor(m => m.Email).NotEmpty();
            //            RuleFor(m => m.Address).NotEmpty();
            RuleFor(m => m.Code).Must(BeAUniqueCode).WithMessage(Resource.CustomerCodeExists);
            //            RuleFor(m => m.NRIC).NotEmpty().WithMessage("NRIC must not be empty.");
            RuleFor(m => m.FirstName).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.LastName).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.PostalCode).Matches(new Regex("^[0-9]*$")).WithMessage(Resource.OnlyNumberAllowed);
            RuleFor(m => m.PresentPostalCode).Matches(new Regex("^[0-9]*$")).WithMessage(Resource.OnlyNumberAllowed);
            RuleFor(m => m.PhoneNumber).Matches(new Regex(@"^[0-9\+]{1,}[0-9\-\ ]{3,15}$")).WithMessage(Resource.OnlyNumberAllowed);
            RuleFor(m => m.OfficeNumber).Matches(new Regex(@"^[0-9\+]{1,}[0-9\-\ ]{3,15}$")).WithMessage(Resource.OnlyNumberAllowed);
            RuleFor(m => m.MobileNumber).Matches(new Regex(@"^[0-9\+]{1,}[0-9\-\ ]{3,15}$")).WithMessage(Resource.OnlyNumberAllowed);
            RuleFor(m => m.FaxNumber).Matches(new Regex(@"^[0-9\+]{1,}[0-9\-\ ]{3,15}$")).WithMessage(Resource.OnlyNumberAllowed);
            RuleFor(m => m.Email).Matches(new Regex(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")).WithMessage(Resource.OnlyEmailFormat);
            RuleFor(m => m.FirstName).Matches(new Regex(@"^([^0-9~&@#$^*()_+=[\]{}|\\,.?:-]*)$")).WithMessage(Resource.OnlyCharactersAllowed);
            RuleFor(m => m.MiddleName).Matches(new Regex(@"^([^0-9~&@#$^*()_+=[\]{}|\\,.?:-]*)$")).WithMessage(Resource.OnlyCharactersAllowed);
            RuleFor(m => m.LastName).Matches(new Regex(@"^([^0-9~&@#$^*()_+=[\]{}|\\,.?:-]*)$")).WithMessage(Resource.OnlyCharactersAllowed);
            RuleFor(m => m.NRIC).Must(BeAUniqueNRIC).WithMessage(Resource.CustomerNRICExists);
            RuleFor(m => m.NRIC).Must(BeTrueNRICFormat).WithMessage(Resource.CustomerNRICFormatInvalid);
            //RuleFor(m => m.PhoneNumber).Must(ContactNotNull).WithMessage("Contact number is required.");
            //RuleFor(m => m.MobileNumber).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty); ;
            //            RuleFor(m => m.DateOfBirth).Must(BeRequireDateOfBirth).WithMessage("Date of Birth is required.");
            RuleFor(m => m.CompanyName).Must(BeRequireCompanyName).WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.TaxNumber).Must(BeAUniqueTaxNumber).WithMessage(Resource.CustomerTaxExists);

        }

        private bool BeAUniqueTaxNumber(ProspectCustomer customer, string taxNumber)
        {
            if (string.IsNullOrEmpty(taxNumber)) return true;
            var existCustomer = _customerService.GetProspectCustomerByTaxNumber(taxNumber);
            return existCustomer == null || existCustomer.ID == customer.ID;
        }
        private bool BeRequireCompanyName(ProspectCustomer customer, string name)
        {
            if (customer.CustomerType == null || (customer.CustomerType.Code == CustomerTypeCode.LocalCompany &&
                string.IsNullOrEmpty(customer.CompanyName))) return false;
            return true;
        }
        private bool ContactNotNull(ProspectCustomer customer, string phoneNumber)
        {
            return (!string.IsNullOrEmpty(customer.PhoneNumber) && !string.IsNullOrWhiteSpace(customer.PhoneNumber)) ||
                   (!string.IsNullOrEmpty(customer.OfficeNumber) && !string.IsNullOrWhiteSpace(customer.OfficeNumber)) ||
                   (!string.IsNullOrEmpty(customer.MobileNumber) && !string.IsNullOrWhiteSpace(customer.MobileNumber));
        }
        private bool BeAUniqueCode(ProspectCustomer customer, string code)
        {
            if (string.IsNullOrEmpty(code)) return true;
            var existCustomer = _customerService.GetProspectCustomerByCode(code);
            return existCustomer == null || existCustomer.ID == customer.ID;
        }

        private bool BeAUniqueNRIC(ProspectCustomer customer, string nric)
        {
            if (string.IsNullOrEmpty(nric)) return true;
            var existCustomer = _customerService.GetProspectCustomerByNRIC(nric);
            return existCustomer == null || existCustomer.ID == customer.ID;
        }

        private bool BeTrueNRICFormat(ProspectCustomer customer, string nric)
        {
            if (customer.CustomerTypeID != null)
                customer.CustomerType = _unitRepository.GetCustomerType((int)customer.CustomerTypeID);
            else
                return false;
            if (!string.IsNullOrEmpty(customer.CustomerType.RegexPattern) &&
                !string.IsNullOrEmpty(nric))
            {
                var regex = new Regex(customer.CustomerType.RegexPattern);
                return regex.IsMatch(nric);
            }
            return true;
        }

        private bool BeRequireDateOfBirth(ProspectCustomer customer, DateTime? dateTime)
        {
            if (customer.CustomerType == null || (customer.CustomerType.Code != CustomerTypeCode.LocalCompany &&
                customer.CustomerType.Code != CustomerTypeCode.ForeignCompany &&
                customer.DateOfBirth == null)) return false;
            return true;
        }

    }
}
