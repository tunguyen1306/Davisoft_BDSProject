using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Davisoft_BDSProject.Domain.Helpers;
using Davisoft_BDSProject.Web.Infrastructure.Helpers;
using Davisoft_BDSProject.Web.Infrastructure.Utility;
using NS.Mvc;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Domain.Enum;

namespace Davisoft_BDSProject.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Branch> Branches { get; set; }
        public IEnumerable<User> Salesmans { get; set; }

        public void Prepare()
        {
    }
    }
}