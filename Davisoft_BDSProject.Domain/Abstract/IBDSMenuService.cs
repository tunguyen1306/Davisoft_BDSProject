using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSMenuService
    {
        IQueryable<BDSMenu> GetIQueryableItems();
        IEnumerable<BDSMenu> GetAllItems(Expression<Func<BDSMenu, object>> includeProperty);
        BDSMenu GetItem(int id);
        BDSMenu CreateItem(BDSMenu item);
        bool UpdateItem(BDSMenu item);
        bool DeleteItem(int id);
        bool CheckItem(BDSMenu item);
    }
}

