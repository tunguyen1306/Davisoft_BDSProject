using System;
using System.Text.RegularExpressions;
using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using CPO.Domain.Enum;
using Resources;

namespace CPO.Web.Validation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        private readonly ICustomerService _customerService;
        private readonly IUnitRepository _unitRepository;
        public CustomerValidator(ICustomerService customerService, IUnitRepository unitRepository)
        {
            _customerService = customerService;
            _unitRepository = unitRepository;
            RuleFor(m => m.CustomerTypeID).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            //RuleFor(m => m.Email).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Address).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.PostalCode).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.TaxNumber).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Code).Must(BeAUniqueCode).WithMessage(Resource.CustomerCodeExists);
            RuleFor(m => m.NRIC).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.FirstName).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
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
            //RuleFor(m => m.MobileNumber).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            //RuleFor(m => m.DateOfBirth).Must(BeRequireDateOfBirth).WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.CompanyName).Must(BeRequireCompanyName).WithMessage(Resource.TheFieldShouldNotBeEmpty);
            //RuleFor(m => m.TaxNumber).Must(BeAUniqueTaxNumber).WithMessage(Resource.CustomerTaxExists);


        }

        //private bool ContactNotNull(Customer customer, string phoneNumber)
        //{
        //    return (!string.IsNullOrEmpty(customer.PhoneNumber) && !string.IsNullOrWhiteSpace(customer.PhoneNumber)) ||
        //           (!string.IsNullOrEmpty(customer.OfficeNumber) && !string.IsNullOrWhiteSpace(customer.OfficeNumber)) ||
        //           (!string.IsNullOrEmpty(customer.MobileNumber) && !string.IsNullOrWhiteSpace(customer.MobileNumber));
        //}
        private bool BeAUniqueCode(Customer customer, string code)
        {
            if (string.IsNullOrEmpty(code)) return true;
            Customer existCustomer = _customerService.GetCustomerByCode(code);
            return existCustomer == null || existCustomer.ID == customer.ID;
        }

        private bool BeAUniqueTaxNumber(Customer customer, string taxNumber)
        {
            if (string.IsNullOrEmpty(taxNumber)) return true;
            Customer existCustomer = _customerService.GetCustomerByTaxNumber(taxNumber);
            return existCustomer == null || existCustomer.ID == customer.ID;
        }

        private bool BeAUniqueNRIC(Customer customer, string nric)
        {
            Customer existCustomer = _customerService.GetCustomerByNRIC(nric);
            return existCustomer == null || existCustomer.ID == customer.ID;
        }

        private bool BeTrueNRICFormat(Customer customer, string nric)
        {
            if (customer.CustomerTypeID != null && !string.IsNullOrEmpty(nric))
                customer.CustomerType = _unitRepository.GetCustomerType((int)customer.CustomerTypeID);
            else
                return false;
            if (customer.CustomerType != null && !string.IsNullOrEmpty(customer.CustomerType.RegexPattern))
            {
                var regex = new Regex(customer.CustomerType.RegexPattern);
                return regex.IsMatch(nric);
            }
            return true;
            ////if (customer.CustomerType.Code == CPO.Domain.Enum.CustomerType.SingaporeNRIC) return SingaporeNRICValidate(customer, nric);
            ////if (customer.CustomerType.Code == CPO.Domain.Enum.CustomerType.LocalCompany) return LocalCompanyValidate(customer, nric);
            ////if (customer.CustomerType.Code == CPO.Domain.Enum.CustomerType.MalaysiaNRIC) return MalaysiaNRICValidate(customer, nric);
            ////if (customer.CustomerType.Code == CPO.Domain.Enum.CustomerType.Business) return BusinessValidate(customer, nric);
            ////if (customer.CustomerType.Code == CPO.Domain.Enum.CustomerType.ForeignPassport) return true;
            //if (customer.CustomerType.Code == CustomerTypeCode.SingaporeNRIC) return SingaporeNRICValidate(customer, nric);
            //if (customer.CustomerType.Code == CustomerTypeCode.LocalCompany) return LocalCompanyValidate(customer, nric);
            //if (customer.CustomerType.Code == CustomerTypeCode.MalaysiaNRIC) return MalaysiaNRICValidate(customer, nric);
            //if (customer.CustomerType.Code == CustomerTypeCode.Business) return BusinessValidate(customer, nric);
            //if (customer.CustomerType.Code == CustomerTypeCode.ForeignPassport) return true;
            //return OtherValidate(customer, nric);
        }

        private bool BeRequireDateOfBirth(Customer customer, DateTime? dateTime)
        {
            if (customer.CustomerType != null && customer.CustomerType.Code != CustomerTypeCode.LocalCompany &&
                customer.CustomerType.Code != CustomerTypeCode.ForeignCompany &&
                customer.DateOfBirth == null) return false;
            return true;
        }

        private bool BeRequireCompanyName(Customer customer, string name)
        {
            if (customer.CustomerType != null && customer.CustomerType.Code == CustomerTypeCode.LocalCompany &&
                string.IsNullOrEmpty(customer.CompanyName)) return false;
            return true;
        }
        private bool SingaporeNRICValidate(Customer customer, string nric)
        {
            if (nric.Length != 9) return false;
            if (!(nric[0] != 'S' || nric[0] != 'T')) return false;
            int temp;
            if (!int.TryParse(nric.Substring(1, 7), out temp)) return false;
            //int year = Convert.ToInt32((nric[0] == 'S' ? "19" : "20") + nric.Substring(1,2));
            //if (year != customer.DateOfBirth.Value.Year) return false;
            int end = (Convert.ToInt32(nric[1].ToString()) * 2 + Convert.ToInt32(nric[2].ToString()) * 7
                + Convert.ToInt32(nric[3].ToString()) * 6 + Convert.ToInt32(nric[4].ToString()) * 5
                + Convert.ToInt32(nric[5].ToString()) * 4 + Convert.ToInt32(nric[6].ToString()) * 3 + Convert.ToInt32(nric[7].ToString()) * 2) % 11;
            switch (end)
            {
                case 0:
                    if (nric[8].ToString().ToUpper() != "J") return false;
                    break;
                case 1:
                    if (nric[8].ToString().ToUpper() != "Z") return false;
                    break;
                case 2:
                    if (nric[8].ToString().ToUpper() != "I") return false;
                    break;
                case 3:
                    if (nric[8].ToString().ToUpper() != "H") return false;
                    break;
                case 4:
                    if (nric[8].ToString().ToUpper() != "G") return false;
                    break;
                case 5:
                    if (nric[8].ToString().ToUpper() != "F") return false;
                    break;
                case 6:
                    if (nric[8].ToString().ToUpper() != "E") return false;
                    break;
                case 7:
                    if (nric[8].ToString().ToUpper() != "D") return false;
                    break;
                case 8:
                    if (nric[8].ToString().ToUpper() != "C") return false;
                    break;
                case 9:
                    if (nric[8].ToString().ToUpper() != "B") return false;
                    break;
                case 10:
                    if (nric[8].ToString().ToUpper() != "A") return false;
                    break;
                default:
                    return true;
            }
            return true;
        }

        private bool LocalCompanyValidate(Customer customer, string nric)
        {
            if (string.IsNullOrEmpty(customer.FullName)) return false;
            if (customer.FullName.Contains("LTD"))
            {
                if (customer.FullName.Contains("PTE LTD"))
                {
                    if (nric[0] != 'A') return false;
                }
                else
                {
                    if (nric[0] != 'B') return false;
                }
                if (nric.Length != 11) return false;
                nric = nric.Substring(1, 10);
            }
            if (nric.Length != 10) return false;
            int temp;
            if (!int.TryParse(nric.Substring(0, 9), out temp)) return false;
            //if (Convert.ToInt32(nric.Substring(0, 4)) != customer.DateOfBirth.Year) return false;
            if (!Char.IsLetter(nric[9])) return false;
            return true;
        }

        private bool MalaysiaNRICValidate(Customer customer, string nric)
        {
            if (nric.Length != 14) return false;
            //if (nric.Substring(0, 2) != customer.DateOfBirth.Year.ToString().Substring(2, 2)) return false;
            //if (nric.Substring(2, 2) != customer.DateOfBirth.Month.ToString("00")) return false;
            //if (nric.Substring(4, 2) != customer.DateOfBirth.Day.ToString("00")) return false;
            int temp;
            if (!int.TryParse(nric.Substring(0, 6), out temp)) return false;
            if (!(nric[6] == '-' && nric[9] == '-')) return false;
            if (!(int.TryParse(nric.Substring(7, 2), out temp) && int.TryParse(nric.Substring(10, 4), out temp))) return false;
            return true;
        }

        private bool BusinessValidate(Customer customer, string nric)
        {
            if (nric.Length != 9) return false;
            int temp;
            if (!int.TryParse(nric.Substring(0, 8), out temp)) return false;
            if (!char.IsLetter(nric[8])) return false;
            return true;
        }

        private bool OtherValidate(Customer customer, string nric)
        {
            if (nric == null || nric.Length != 10) return false;
            if (nric[0] != 'T') return false;
            int temp;
            if (!int.TryParse(nric.Substring(1, 2), out temp)) return false;
            if (!(char.IsLetter(nric[3]) && char.IsLetter(nric[4]))) return false;

            if (!int.TryParse(nric.Substring(5, 4), out temp)) return false;
            if (!char.IsLetter(nric[9])) return false;
            return true;
        }

    }
}
