using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSNewsDateService
    {
        IQueryable<BDSNewsDate> GetIQueryableItems();
        IEnumerable<BDSNewsDate> GetAllItems(Expression<Func<BDSNewsDate, object>> includeProperty);
        BDSNewsDate GetItem(int id);
        BDSNewsDate CreateItem(BDSNewsDate item);
        bool UpdateItem(BDSNewsDate item);
        bool DeleteItem(int id);
        bool CheckItem(BDSNewsDate item);
    }
}

