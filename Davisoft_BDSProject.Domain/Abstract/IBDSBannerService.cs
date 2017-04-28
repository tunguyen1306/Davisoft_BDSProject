using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSBannerService
    {
        IQueryable<BDSBanner> GetIQueryableItems();
        IEnumerable<BDSBanner> GetAllItems(Expression<Func<BDSBanner, object>> includeProperty);
        BDSBanner GetItem(int id);
        BDSBanner CreateItem(BDSBanner item);
        bool UpdateItem(BDSBanner item);
        bool DeleteItem(int id);
        bool CheckItem(BDSBanner item);
    }
}

