using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class CarModelValidator : AbstractValidator<CarModel>
    {
        private readonly ICarRepository _repo;

        public CarModelValidator(ICarRepository repo)
        {
            _repo = repo;
            RuleFor(m => m.ModelCategoryID).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Code).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m=>m.Code).Must(BeAUniqueCode).WithMessage(Resource.ModelCodeSpecCodeExists);
            RuleFor(m => m.SpecCode).Must(BeAUniqueCode).WithMessage(Resource.ModelCodeSpecCodeExists);
            RuleFor(m => m.Description).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.ModelYear).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.ProductCode).Must(BeAUniqueProductCode).WithMessage(Resource.ModelProductCodeExists);
        }

        private bool BeAUniqueCode(CarModel model, string code)
        {
            CarModel existingModel = _repo.GetModelByCodeAndSpec(code,model.SpecCode);

            if (existingModel != null && existingModel.ID != model.ID)
                return false;

            return true;
        }

        private bool BeAUniqueProductCode(CarModel model, string productCode)
        {
            CarModel existModel = _repo.GetModelByProductCode(productCode);
            return existModel == null || existModel.ID == model.ID;
        }
    }
}
