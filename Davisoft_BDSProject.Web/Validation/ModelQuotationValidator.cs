using FluentValidation;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class ModelQuotationValidator : AbstractValidator<ModelQuotation>
    {
        public ModelQuotationValidator()
        {
            RuleFor(m => m.FuelCL).GreaterThan(-1).WithMessage(Resource.TheFuleCLGreaterThanNegativeNumber);
            RuleFor(m => m.Co2EL).GreaterThan(-1).WithMessage(Resource.TheCO2ELGreaterThanNegativeNumber);
            //RuleFor(m => m.ListPrice).GreaterThan(0).WithMessage("The ListPrice should not equal 0");
        }
    }
}
