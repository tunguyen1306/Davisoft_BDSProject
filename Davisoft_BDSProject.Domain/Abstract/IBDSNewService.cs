using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSNewService
    {
        IQueryable<BDSNew> GetIQueryableItems();
        IEnumerable<BDSNew> GetAllItems(Expression<Func<BDSNew, object>> includeProperty);
        BDSNew GetItem(int id);
        BDSNew CreateItem(BDSNew item);
        bool UpdateItem(BDSNew item);
        bool DeleteItem(int id);
        bool CheckItem(BDSNew item);
    }
}

