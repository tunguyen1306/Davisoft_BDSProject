using System.Web.Mvc;
using MIT.Domain.Abstract;
using MIT.Domain.Entities;

namespace MIT.Web.Infrastructure.Utility
{
    public class UnitHelper
    {
        public static IUnitRepository UnitRepository
        {
            get { return DependencyResolver.Current.GetService<IUnitRepository>(); }
        }

        //public static Currency DefaultCurrency
        //{
        //    get { return UnitRepository.GetDefaultCurrency(); }
        //    set { UnitRepository.SetDefaultCurrency(value.ID); }
        //}

        //public static Country DefaultCountry
        //{
        //    get { return UnitRepository.GetDefaultCountry(); }
        //    set { UnitRepository.SetDefaultCountry(value.ID); }
        //}
    }
}
