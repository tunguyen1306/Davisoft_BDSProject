using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class MasterColorValidator : AbstractValidator<MasterColor>
    {
        private readonly IUnitRepository _repo;
        public MasterColorValidator(IUnitRepository repo)
        {
            _repo = repo;
            RuleFor(m => m.Code).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Code).Must(BeAUniqueCode).WithMessage(Resource.MasterColorCodeExists);
        }
        private bool BeAUniqueCode(MasterColor color, string code)
        {
            MasterColor exist = _repo.GetMasterColorByCode(code);

            return exist == null || exist.ID == color.ID;
        }
    }
}
