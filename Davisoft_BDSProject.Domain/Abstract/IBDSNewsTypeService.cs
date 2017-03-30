using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSNewsTypeService
    {
        IQueryable<BDSNewsType> GetIQueryableItems();
        IEnumerable<BDSNewsType> GetAllItems(Expression<Func<BDSNewsType, object>> includeProperty);
        BDSNewsType GetItem(int id);
        BDSNewsType CreateItem(BDSNewsType item);
        bool UpdateItem(BDSNewsType item);
        bool DeleteItem(int id);
        bool CheckItem(BDSNewsType item);
    }
}

