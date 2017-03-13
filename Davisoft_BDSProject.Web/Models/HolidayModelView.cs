using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Davisoft_BDSProject.Domain.Entities;
using Resources;

namespace Davisoft_BDSProject.Web.Models
{
    public class HolidayModelView
    {
        public IEnumerable<Holiday> Holidays { get; set; }
        public HolidayModel HolidayModel { get; set; }
    }

    public class HolidayModel
    {
        public int ID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string DateSelected { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TheFieldShouldNotBeEmpty")]
        public string Description { get; set; }
        public bool IsFullDay { get; set; }
    }
}