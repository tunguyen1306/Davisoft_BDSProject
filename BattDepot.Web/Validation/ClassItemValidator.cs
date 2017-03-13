using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class ClassItemValidator : AbstractValidator<ClassItem>
    {
        private readonly IUnitRepository _repo;
        public ClassItemValidator(IUnitRepository repo)
        {
            _repo = repo;
            RuleFor(m => m.Code).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Code).Must(BeAUniqueCode).WithMessage(Resource.ClassCodeExists);
        }
        private bool BeAUniqueCode(ClassItem classItem, string code)
        {
            ClassItem exist = _repo.GetClassItemByCode(code);

            return exist == null || exist.ID == classItem.ID;
        }
    }
}
