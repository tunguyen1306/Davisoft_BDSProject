using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class RegistrationTypeValidator:AbstractValidator<RegistrationType>
    {
        private readonly IUnitRepository _repo;
        public RegistrationTypeValidator(IUnitRepository repo)
        {
            _repo = repo;
            RuleFor(m => m.Code).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Code).Must(BeAUniqueCode).WithMessage("'" + Resource.Code + "' " + Resource.AlreadyExist);
        }
        private bool BeAUniqueCode(RegistrationType rType,string code)
        {
            RegistrationType existRType = _repo.GetRegistrationTypeByCode(code);

            return existRType == null || existRType.ID == rType.ID;
        }
    }
}
