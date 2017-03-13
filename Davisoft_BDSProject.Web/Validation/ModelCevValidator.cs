using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using CPO.Domain.Models;
using Resources;

namespace CPO.Web.Validation
{
    public class ModelCevValidator : AbstractValidator<ModelCev>
    {
        private readonly ICarRepository _repo;
        public ModelCevValidator(ICarRepository repo)
        {
            _repo = repo;
            RuleFor(x => x.CO2).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(x => x.Band).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(x => x.Rebate).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(x => x.Surcharge).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
        }

        private bool BeAUnique(ModelCev modelCEV, decimal rebate)
        {
            return rebate == null || rebate == 0;
        }
    }

    public class  MappedModelValidator : AbstractValidator<MappedModel>
    {
        
        public MappedModelValidator()
        {
            //CEV
            RuleFor(m => m.CO2).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(x => x.Band).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(x => x.Rebate).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(x => x.Surcharge).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);

            //Vitas

            RuleFor(m => m.AttachID).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);

            RuleFor(m => m.Value).GreaterThan(0).WithMessage(Resource.TheFieldShouldGreaterThan + " 0");
            RuleFor(m => m.EPType).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);


        }
        private bool BeAUnique(MappedModel modelCEV, decimal rebate)
        {
            return rebate != null && rebate != 0;
        }
    }
}
