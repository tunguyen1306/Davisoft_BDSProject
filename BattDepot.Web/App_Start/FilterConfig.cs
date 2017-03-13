using System.Linq;
using System.Web.Mvc;
using Davisoft_BDSProject.Web.Infrastructure.Filters;

namespace Davisoft_BDSProject.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new ErrorLoggerAttribute());
            filters.Add(new AuditAttribute());
            filters.Add(new RequestAuthorizeAttribute());
        }

        public static void RegisterFilterProviders(FilterProviderCollection providers)
        {
            // http://blogs.microsoft.co.il/blogs/oric/archive/2011/10/28/exclude-a-filter.aspx
            IFilterProvider[] currentProviders = providers.ToArray();
            providers.Clear();
            providers.Add(new ExcludeFiltersProvider(currentProviders));
        }
    }
}
