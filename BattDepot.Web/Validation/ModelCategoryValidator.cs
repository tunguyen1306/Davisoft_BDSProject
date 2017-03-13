using System;
using System.Linq;
using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class ModelCategoryValidator : AbstractValidator<ModelCategory>
    {
        private readonly ICarRepository _repo;

        public ModelCategoryValidator(ICarRepository repo)
        {
            _repo = repo;
            RuleFor(m => m.Name).Must(BeUniqueName).WithMessage(Resource.ModelCategoryNameExists);
            RuleFor(m => m.Name).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.FuelCost).InclusiveBetween(Convert.ToDecimal(0), decimal.MaxValue).WithMessage(Resource.TheFieldShouldGreaterThan + " 0");
        }

        private bool BeUniqueName(ModelCategory sender, string name)
        {
            var existObj = _repo.GetAllModelCategories().Where(x => x.Name == name).FirstOrDefault();
            return existObj == null || existObj.ID == sender.ID;
        }
    }
}
