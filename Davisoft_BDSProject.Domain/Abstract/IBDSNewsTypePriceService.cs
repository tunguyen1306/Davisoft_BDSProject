using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSNewsTypePriceService
    {
        IQueryable<BDSNewsTypePrice> GetIQueryableItems();
        IEnumerable<BDSNewsTypePrice> GetAllItems(Expression<Func<BDSNewsTypePrice, object>> includeProperty);
        BDSNewsTypePrice GetItem(int id);
        BDSNewsTypePrice CreateItem(BDSNewsTypePrice item);
        bool UpdateItem(BDSNewsTypePrice item);
        bool DeleteItem(int id);
        bool CheckItem(BDSNewsTypePrice item);
    }
}

