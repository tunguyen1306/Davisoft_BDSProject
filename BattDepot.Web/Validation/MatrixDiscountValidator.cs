using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class MatrixDiscountValidator : AbstractValidator<MatrixDiscount>
    {
        public MatrixDiscountValidator(IUnitRepository unitRepo)
        {
            RuleFor(m => m.DiscountEnd).Must(MinMaxValidate).WithMessage(Resource.MinDiscountSmallerMaxDiscount);
        }

        public bool MinMaxValidate(MatrixDiscount discount, decimal max)
        {
            return discount.DiscountFrom <= discount.DiscountEnd;
        }
    }
}
