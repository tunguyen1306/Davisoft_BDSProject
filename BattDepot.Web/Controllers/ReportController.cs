using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using Davisoft_BDSProject.Web.Infrastructure.Filters;
using NS.Mvc;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Domain.Enum;
using Davisoft_BDSProject.Web.Infrastructure.Helpers;
using Davisoft_BDSProject.Web.Models;
using AutoMapper;
using System.Web;
namespace Davisoft_BDSProject.Web.Controllers
{
    [ExcludeFilters(typeof(RequestAuthorizeAttribute))]
    public class ReportController : Controller
    {
        private readonly IUnitRepository _unitRepo;

        public ReportController(IUnitRepository unitRepo)
        {
            _unitRepo = unitRepo;
        }
    }
}
