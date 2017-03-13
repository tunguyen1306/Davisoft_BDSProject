using System.IO;
using System.Linq;
using System.Web;
using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using CPO.Domain.Enum;
using CPO.Web.Infrastructure.Helpers;
using Resources;

namespace CPO.Web.Validation
{
    public class PdiDeliveryValidator : AbstractValidator<PdiDelivery>
    {
        private static IPdiDeliveryRepository _repo;
        private static IBookingService _bookingService;
        public PdiDeliveryValidator(IPdiDeliveryRepository repo, IBookingService bookingService)
        {
            _repo = repo;
            _bookingService = bookingService;
            RuleSet("Create_Update", () =>
            {
                RuleFor(m => m.BookingID).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
                RuleFor(m => m.PDITo).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
                RuleFor(m => m.BookingID).Must(Unique).WithMessage(Resource.DeliveryRecordAdded);
                RuleFor(m => m.BookingID).Must(CheckCanProcessPdiTo).WithMessage(Resource.CannotImplementThisProcess);
            });

            RuleSet("Complete", () =>
            {
                RuleFor(m => m.BranchTo).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
                RuleFor(m => m.ActualDelivery).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
                RuleFor(m => m.Picture).NotNull().WithMessage(Resource.TheFieldShouldNotBeEmpty);
                RuleFor(m => m.Picture).Must(CheckFileUpload).WithMessage(Resource.FileTypeNotAllowed);
                RuleFor(m => m.BookingID).Must(CheckCanProcessBranchTo).WithMessage(Resource.CannotImplementThisProcess);
            });
            //RuleFor(m => m.ActualDelivery).NotNull().WithMessage("Actual Delivery Date is required.");
            //RuleFor(m => m.CustomerSigned).NotNull().WithMessage("Customer Signed is required.");
            //RuleFor(m => m.PDIToID).NotNull().WithMessage("Please choose booking.");
            //RuleFor(m => m.BookingID).NotNull().WithMessage("Please choose booking.");
        }

        private bool CheckFileUpload(HttpPostedFileBase arg)
        {
            string extension = Path.GetExtension(arg.FileName);
            return !string.IsNullOrEmpty(extension) &&
                   !extension.ToLower().Contains("jpg") &&
                   !extension.ToLower().Contains("png") &&
                   !extension.ToLower().Contains("jpeg");
        }

        private bool CheckCanProcessPdiTo(int bookingId)
        {
            var booking = _bookingService.GetBooking(bookingId);
            return booking.CanProcess(BookingModule.DeliveryPdiTo);
        }
        private bool CheckCanProcessBranchTo(int bookingId)
        {
            var booking = _bookingService.GetBooking(bookingId);
            return booking.CanProcess(BookingModule.DeliveryBranchTo);
        }
        //private bool Invoicing(int bookingId)
        //{
        //    return _invoiceService.GetInvoiceByBookingID(bookingId) != null;
        //}

        private bool Unique(PdiDelivery pdi, int bookingId)
        {
            var existPdi = _repo.GetAllPdiDeliveries(m => m.BookingID == bookingId).FirstOrDefault();
            return existPdi == null || existPdi.ID == pdi.ID;
        }
    }
}
