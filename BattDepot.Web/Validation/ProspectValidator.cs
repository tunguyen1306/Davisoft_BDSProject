using FluentValidation;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class ProspectValidator : AbstractValidator<Prospect>
    {
        public ProspectValidator()
        {
            RuleFor(m => m.Category).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.SourceOfLeadID).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.Model.ModelID).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(m => m.HaveTestDrive).Must(CheckHaveTestDrive).WithMessage(Resource.TDVehicleAndTradePlateNumberMustBeNotEmpty);
        }

        private bool CheckHaveTestDrive(Prospect pros, bool haveTestDrive)
        {
            if (!haveTestDrive ||
                (pros.TDVehicleID != null && pros.TradePlateID != null))
            {
                return true;
            }
            return false;
        }
    }
}
