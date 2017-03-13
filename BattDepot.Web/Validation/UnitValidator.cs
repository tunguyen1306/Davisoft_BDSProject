using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class PlatTypeValidator : AbstractValidator<PlatType>
    {
        private readonly IUnitRepository _repo;
        public PlatTypeValidator(IUnitRepository repo)
        {
            _repo = repo;
            RuleFor(m => m.Code).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Code).Must(BeAUniqueCode).WithMessage("'" + Resource.Code + "' " + Resource.AlreadyExist);
        }
        private bool BeAUniqueCode(PlatType rType, string code)
        {
            PlatType existRType = _repo.GetPlatType(code);

            return existRType == null || existRType.ID == rType.ID;
        }
    }
}
