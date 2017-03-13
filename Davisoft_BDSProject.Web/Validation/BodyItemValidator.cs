using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class BodyItemValidator : AbstractValidator<BodyItem>
    {
        private readonly IUnitRepository _repo;
        public BodyItemValidator(IUnitRepository repo)
        {
            _repo = repo;
            RuleFor(m => m.Code).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Code).Must(BeAUniqueCode).WithMessage(Resource.BodyWithSameCodeAlreadyExists);
        }
        private bool BeAUniqueCode(BodyItem body, string code)
        {
            BodyItem exist = _repo.GetBodyItemByCode(code);

            return exist == null || exist.ID == body.ID;
        }
    }
}
