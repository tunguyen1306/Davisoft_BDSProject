using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class CarColorValidator : AbstractValidator<CarColor>
    {
        private readonly ICarRepository _repo;

        public CarColorValidator(ICarRepository repo)
        {
            _repo = repo;

            RuleFor(m => m.Code).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Code).Must(BeAUniqueCode).WithMessage(Resource.ColorCodeExists);
        }

        private bool BeAUniqueCode(CarColor color, string code)
        {
            CarColor existColor = _repo.GetColorByCode(code);

            return existColor == null || existColor.ID == color.ID;
        }
    }
}
