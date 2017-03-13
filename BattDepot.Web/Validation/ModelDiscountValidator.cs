using System;
using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class ModelDiscountValidator : AbstractValidator<ModelDiscount>
    {
        private readonly ICarRepository _repo;

        public ModelDiscountValidator( ICarRepository repo)
        {
            //_indentRepository = indentRepository;
            _repo = repo;

            //RuleFor(m => m.ModelCode).NotEmpty();
            //RuleFor(m => m.ModelCode).Must(BeModelCode).WithMessage("Model Code not exists");
            RuleFor(m => m.Description).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.DiscountValue).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            //RuleFor(m => m.StartTime).NotEmpty();
            //RuleFor(m => m.EndTime).NotEmpty();
            RuleFor(m => m.EndTime).Must(BeEndTimeCode).WithMessage(Resource.EndTimeNotLessThanStartTime);
            
        }

        //private bool BeModelCode(ModelDiscount discount, string ModelCode)
        //{
        //    IEnumerable<string> indentNos = _indentRepository.GetAllStockableIndents()
        //                                                     .Where(i => i.IndentNo == ModelCode)
        //                                                     .Select(i => i.IndentNo);
        //    if (indentNos == null) return false;
        //    return true;
        //}

        private bool BeEndTimeCode(ModelDiscount discount, DateTime EndTime)
        {
            return discount.StartTime < EndTime;
        }
    }
}
