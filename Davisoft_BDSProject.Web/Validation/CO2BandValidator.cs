using System.Text.RegularExpressions;
using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class CO2BandValidator : AbstractValidator<CO2Band>
    {
        private static ICO2BandService _repo;
        public CO2BandValidator(ICO2BandService repo)
        {
            _repo = repo;
            RuleFor(m => m.CO2From).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.CO2From).LessThan(m => m.CO2To).WithMessage(Resource.CO2FromLessThanCO2To);
            RuleFor(m => m.CO2To).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Band).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Band).Must(MustBeUniqueBand).WithMessage(Resource.BandExists);
            RuleFor(m => m.Rebate).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Surcharge).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
      
        }
        private bool MustBeUniqueBand(CO2Band co2Band, string band)
        {
            CO2Band co2Band_exist = _repo.GetCO2Band_By_Band(band);
            if (co2Band_exist == null || co2Band_exist.ID == co2Band.ID)
            {
                return true;
            }
            else
            {
                return false;
            }
          
        }
        private bool IsNumber(string str)
        {

            if (str == null)
                return true;
            Regex regex = new Regex(@"^[-+]?[0-9-+]*\.?[0-9-+]+$");
            return regex.IsMatch(str);

        }
    }
}
