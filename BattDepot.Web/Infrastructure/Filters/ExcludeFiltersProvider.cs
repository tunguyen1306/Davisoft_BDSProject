using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Davisoft_BDSProject.Web.Infrastructure.Filters
{
    public class ExcludeFiltersProvider : IFilterProvider
    {
        private readonly FilterProviderCollection _filterProviders;

        public ExcludeFiltersProvider(IFilterProvider[] filters)
        {
            _filterProviders = new FilterProviderCollection(filters);
        }

        #region IFilterProvider Members

        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            Filter[] filters = _filterProviders.GetFilters(controllerContext, actionDescriptor).ToArray();

            List<ExcludeFiltersAttribute> excludeFilters = filters.Where(f => f.Instance is ExcludeFiltersAttribute)
                                                                 .Select(f => f.Instance as ExcludeFiltersAttribute)
                                                                 .ToList();

            List<Type> filtersToExclude = excludeFilters.SelectMany(excludeFilter => excludeFilter.Filters).ToList();

            List<Filter> filtersToApply = filters.Where(filter => !filtersToExclude.Contains(filter.Instance.GetType())).ToList();

            return filtersToApply;
        }

        #endregion
    }
}
