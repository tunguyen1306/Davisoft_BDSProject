using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class ModelWtsValidator : AbstractValidator<ModelWts>
    {
        private readonly ICarRepository _carRepository;

        public ModelWtsValidator(ICarRepository carRepository)
        {
            _carRepository = carRepository;

            RuleFor(m => m.UmKgs).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.MlwKgs).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
        }       
    }
}
