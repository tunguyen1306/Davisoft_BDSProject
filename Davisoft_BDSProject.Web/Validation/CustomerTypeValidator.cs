using System;
using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class CustomerTypeValidator : AbstractValidator<CustomerType>
    {
        private static IUnitRepository _repo;

        public CustomerTypeValidator(IUnitRepository repo)
        {
            _repo = repo;
            RuleFor(m => m.Code).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Code).Must(MustBeUnique).WithMessage("'" + Resource.Code + "' " + Resource.AlreadyExist);
        }

        private bool MustBeUnique(CustomerType cus, string code)
        {
            CustomerType exist = _repo.GetCustomerType(code);
            return exist == null || exist.ID == cus.ID;
        }
    }
}
