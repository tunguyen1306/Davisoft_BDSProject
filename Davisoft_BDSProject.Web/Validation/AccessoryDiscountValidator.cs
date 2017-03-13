using System;
using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using CPO.Domain.Enum;
using Resources;

namespace CPO.Web.Validation
{
    public class AccessoryDiscountValidator : AbstractValidator<AccessoryDiscount>
    {
        private readonly ICarRepository _repo;
        //private readonly IIndentRepository _indentRepository;
        private readonly IAccessoriesRepository _accessoriesRepository;

        public AccessoryDiscountValidator(ICarRepository repo,IAccessoriesRepository accessoriesRepository)
        {
            _accessoriesRepository = accessoriesRepository;
            _repo = repo;

            RuleFor(m => m.SerialNumber).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.SerialNumber).Must(BeSerialNumber).WithMessage(Resource.SerialNumber + " " + Resource.IsNotExist);
            RuleFor(m => m.AccessoryType).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Description).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.DiscountValue).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.EndTime).Must(BeEndTimeCode).WithMessage(Resource.StartTimeNotLessThanTheEndTime);
            
        }
        private bool BeSerialNumber(AccessoryDiscount discount, string serialNumber)
        {
            if (discount != null && discount.AccessoryType == AccessoryDiscountType.Standard && !string.IsNullOrEmpty(serialNumber))
            {
                var exist = _accessoriesRepository.GetStandardItemSerial(Convert.ToInt32(serialNumber));
                return exist != null;
            }

            if (discount != null && discount.AccessoryType == AccessoryDiscountType.Optional && !string.IsNullOrEmpty(serialNumber))
            {
                var exist = _accessoriesRepository.GetOptionalItemSerial(Convert.ToInt32(serialNumber));
                return exist != null;
            }
            return false;
        }
        private bool BeEndTimeCode(AccessoryDiscount discount, DateTime EndTime)
        {
            return discount.StartTime < EndTime;
        }
    }
}
