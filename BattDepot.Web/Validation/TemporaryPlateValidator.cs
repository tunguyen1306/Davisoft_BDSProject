using System;
using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using CPO.Domain.Enum;
using CPO.Domain.Helpers;
using CPO.Web.Infrastructure.Helpers;
using Resources;

namespace CPO.Web.Validation
{
    public class TemporaryPlateValidator : AbstractValidator<TemporaryPlate>
    {
        private readonly ITemporaryPlateRepository _plateRepository;

        public TemporaryPlateValidator(ITemporaryPlateRepository plateRepository)
        {
            _plateRepository = plateRepository;
            RuleFor(m => m.PlateNo).Must(BeAUniquePlateNo).WithMessage("'" + Resource.PlateNo + "' " + Resource.AlreadyExist);
        }

        private bool BeAUniquePlateNo(TemporaryPlate plate, string plateNo)
        {
            TemporaryPlate exitPlate = _plateRepository.GetTemporaryPlateByPlateNo(plateNo);
            return exitPlate == null || exitPlate.ID == plate.ID;
        }
    }

    public class AssignedTempPlateValidator : AbstractValidator<AssignedTempPlate>
    {
        private readonly ITemporaryPlateRepository _plateRepository;

        private static IBookingService _bookingService;
        public AssignedTempPlateValidator(ITemporaryPlateRepository plateRepository, IBookingService bookingService)
        {
            _plateRepository = plateRepository;
            _bookingService = bookingService;
            RuleSet("Collect", () =>
            {
                RuleFor(m => m.CollectionDate).NotNull().NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
                RuleFor(m => m.CollectionDate).Must(CollectionDateGreaterThanIssueDate).WithMessage(Resource.CollectionDateGreaterThanIssueDate);
            });
            RuleFor(m => m.IssueDate).LessThanOrEqualTo(UserDateTime.Now).WithMessage(Resource.IssueDateLessThanNow);
            RuleFor(m => m.BookingID).Must(Unique).WithMessage(Resource.ThisBookingAlreadyAssigned);
            RuleFor(m => m.TemporaryPlateID).Must(CheckAssign).WithMessage(Resource.TemporaryPlateHasBeenAssigned);
            RuleFor(m => m.BookingID).Must(CheckCanProcess).WithMessage(Resource.CannotImplementThisProcess);
        }
        private bool CheckAssign(AssignedTempPlate temp, int tempId)
        {
            var existPdi = _plateRepository.GetTemporaryPlate(tempId);
            return (existPdi != null && string.IsNullOrEmpty(existPdi.ChassisNo)) ||
                   (existPdi != null && existPdi.ID == temp.TemporaryPlateID);
        }
        private bool Unique(AssignedTempPlate temp, int bookingId)
        {
            var existPdi = _plateRepository.GetAssignedTempPlateByBooking(bookingId);
            return existPdi == null || existPdi.ID == temp.ID;
        }
        private bool CheckCanProcess(int bookingId)
        {
            var booking = _bookingService.GetBooking(bookingId);
            return booking.CanProcess(BookingModule.DeliveryBranchTo);
        }
        //private bool Invoicing(int bookingId)
        //{
        //return _invoiceService.GetInvoiceByBookingID(bookingId) != null;
        //}
        private bool CollectionDateGreaterThanIssueDate(AssignedTempPlate item, DateTime? collectionDate)
        {
            return item.CollectionDate == null || item.CollectionDate > item.IssueDate;
        }
    }
}
