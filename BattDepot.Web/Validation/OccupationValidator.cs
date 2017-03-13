using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class OccupationValidator : AbstractValidator<Occupation>
    {
        private readonly IUnitRepository _UnitRepository;

        public OccupationValidator(IUnitRepository UnitRepository)
        {
            _UnitRepository = UnitRepository;

            RuleFor(m => m.Code).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Code).Must(BeCode).WithMessage(Resource.OccupationCodeExist);
            RuleFor(m => m.Description).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
        }

        private bool BeCode(Occupation Occ, string Code)
        {
            var oc = _UnitRepository.GetOccupationByCode(Code);
            return oc == null || oc.ID == Occ.ID;
        }
    }
}
