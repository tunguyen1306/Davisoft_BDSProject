using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSExtNewsService
    {
        IQueryable<BDSExtNews> GetIQueryableItems();
        IEnumerable<BDSExtNews> GetAllItems(Expression<Func<BDSExtNews, object>> includeProperty);
        BDSExtNews GetItem(int id);
        BDSExtNews CreateItem(BDSExtNews item);
        bool UpdateItem(BDSExtNews item);
        bool DeleteItem(int id);
        bool CheckItem(BDSExtNews item);
    }
}

