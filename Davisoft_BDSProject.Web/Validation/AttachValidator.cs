using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class AttachItemValidator : AbstractValidator<AttachItem>
    {
        private readonly IUnitRepository _repo;
        public AttachItemValidator(IUnitRepository repo)
        {
            _repo = repo;
            RuleFor(m => m.Code).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Code).Must(BeAUniqueCode).WithMessage(Resource.AttachWithSameCodeAlreadyExists);
        }
        private bool BeAUniqueCode(AttachItem attach, string code)
        {
            AttachItem exist = _repo.GetAttachByCode(code);

            return exist == null || exist.ID == attach.ID;
        }
    }
}
