using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class ModelVitasValidator: AbstractValidator<ModelVitas>
    {
        private readonly ICarRepository _repo;
        public ModelVitasValidator(ICarRepository repo)
        {
            _repo = repo;
            RuleFor(x => x.Code).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
        }
    }
}
