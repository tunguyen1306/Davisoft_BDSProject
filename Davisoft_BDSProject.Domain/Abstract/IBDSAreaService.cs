using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSAreaService
    {
        IQueryable<BDSArea> GetIQueryableItems();
        IEnumerable<BDSArea> GetAllItems(Expression<Func<BDSArea, object>> includeProperty);
        BDSArea GetItem(int id);
        BDSArea CreateItem(BDSArea item);
        bool UpdateItem(BDSArea item);
        bool DeleteItem(int id);
        bool CheckItem(BDSArea item);
    }
}

