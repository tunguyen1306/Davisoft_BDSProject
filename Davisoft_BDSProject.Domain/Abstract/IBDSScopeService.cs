using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSScopeService
    {
        IQueryable<BDSScope> GetIQueryableItems();
        IEnumerable<BDSScope> GetAllItems(Expression<Func<BDSScope, object>> includeProperty);
        BDSScope GetItem(int id);
        BDSScope CreateItem(BDSScope item);
        bool UpdateItem(BDSScope item);
        bool DeleteItem(int id);
        bool CheckItem(BDSScope item);
    }
}

