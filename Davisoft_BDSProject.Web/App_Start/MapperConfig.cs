using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NS;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Domain.Enum;
using Davisoft_BDSProject.Domain.Helpers;
using Davisoft_BDSProject.Web.Models;

// ReSharper disable once CheckNamespace

namespace Davisoft_BDSProject.Web
{
    public static class MapperConfig
    {
        public static void RegisterMappers()
        {
            Mapper.CreateMap<Enumeration, int>().ConvertUsing<EnumerationTypeConverter>();
        }
    }
}
