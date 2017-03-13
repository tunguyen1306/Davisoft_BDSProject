using System.Linq;
using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class TDVehicleValidator : AbstractValidator<TDVehicle>
    {
        private readonly IUnitRepository _unitRepo;

        public TDVehicleValidator(IUnitRepository unitRepo)
        {
            _unitRepo = unitRepo;

            RuleFor(m => m.BranchID).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.ChassisNo).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.EngineNo).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.LocationRef).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            //RuleFor(m => m.RegistrationNo).NotEmpty().WithMessage("Trade Plate is required.");
            RuleFor(m => m.ChassisNo).Must(BeAUniqueChassis).WithMessage("'" + Resource.ChassisNo + "' " + Resource.AlreadyExist);
        }

        private bool BeAUniqueChassis(TDVehicle vehicle, string chassis)
        {
            TDVehicle existTdVehicle = _unitRepo.GetAllTDVehicle(m => m.ChassisNo == chassis).FirstOrDefault();
            return existTdVehicle == null || existTdVehicle.ID == vehicle.ID;
        }
    }
}
