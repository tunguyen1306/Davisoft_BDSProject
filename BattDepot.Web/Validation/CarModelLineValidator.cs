using FluentValidation;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class CarModelLineValidator : AbstractValidator<VehicleModel>
    {
        public CarModelLineValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
        }
    }

    public class CarManufactureValidator : AbstractValidator<VehicleMake>
    {
        public CarManufactureValidator()
        {
            RuleFor(m => m.Name).NotEmpty();
        }
    }
}
