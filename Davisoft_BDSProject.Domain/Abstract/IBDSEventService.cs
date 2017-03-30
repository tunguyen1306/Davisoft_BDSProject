using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSEventService
    {
        IQueryable<BDSEvent> GetIQueryableItems();
        IEnumerable<BDSEvent> GetAllItems(Expression<Func<BDSEvent, object>> includeProperty);
        BDSEvent GetItem(int id);
        BDSEvent CreateItem(BDSEvent item);
        bool UpdateItem(BDSEvent item);
        bool DeleteItem(int id);
        bool CheckItem(BDSEvent item);
    }
}

