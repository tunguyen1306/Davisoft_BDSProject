using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class LocationValidator : AbstractValidator<Location>
    {
        private readonly IUnitRepository _repo;

        public LocationValidator(IUnitRepository repo)
        {
            _repo = repo;
            RuleFor(m => m.Code).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Code).Must(BeAUniqueCode).WithMessage(Resource.LocationCodeExists);
        }

        private bool BeAUniqueCode(Location rType, string code)
        {
            Location existRType = _repo.GetLocationByCode(code);

            return existRType == null || existRType.ID == rType.ID ||
                   (existRType.Bonded != rType.Bonded);
        }
    }
}
