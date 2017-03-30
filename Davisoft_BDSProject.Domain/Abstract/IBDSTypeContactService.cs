using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSTypeContactService
    {
        IQueryable<BDSTypeContact> GetIQueryableItems();
        IEnumerable<BDSTypeContact> GetAllItems(Expression<Func<BDSTypeContact, object>> includeProperty);
        BDSTypeContact GetItem(int id);
        BDSTypeContact CreateItem(BDSTypeContact item);
        bool UpdateItem(BDSTypeContact item);
        bool DeleteItem(int id);
        bool CheckItem(BDSTypeContact item);
    }
}

