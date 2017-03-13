using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Domain.Enum;
using Davisoft_BDSProject.Web.Infrastructure;

namespace Davisoft_BDSProject.Web.Models
{
    public class HomeSearchModel
    {
        //public IEnumerable<Indent> Indents { get; set; }
        //public IEnumerable<ConfirmDocument> ConfirmDocuments { get; set; }
        //public IEnumerable<Booking> Bookings { get; set; }

        public HomeSearchModel()
        {
        }

        public void Search(string query)
        {
            ////var indentRepo = DependencyHelper.GetService<IIndentRepository>();
            //var bookingRepository = DependencyHelper.GetService<IBookingService>();

            //if (string.IsNullOrEmpty(query))
            //{
            //    //Indents = indentRepo.GetAllIndents(IndentStatus.Initial).ToList();
            //    //ConfirmDocuments = indentRepo.GetAllConfirmDocuments(LCStatus.Requested).ToList();
            //    Bookings = bookingRepository.GetAllBookings(BookingStatus.Confirm).ToList();
            //    return;
            //}
            //query = query.Trim().ToLower();
            ////Indents = indentRepo.GetAllIndents(i => i.IndentNo.Trim().ToLower().Contains(query)).ToList();
            ////ConfirmDocuments = indentRepo.GetAllConfirmDocuments(b => b.ConfirmDocumentLines.Any(m => m.IndentLine.Indent.IndentNo.Trim().ToLower().Contains(query))).ToList();
            //Bookings = bookingRepository.GetAllBookings(b => b.ContractNumber.ToLower().Trim().Contains(query)).ToList();

        }
    }
}