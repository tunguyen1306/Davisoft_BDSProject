using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSBankService
    {
        IQueryable<BDSBank> GetIQueryableItems();
        IEnumerable<BDSBank> GetAllItems(Expression<Func<BDSBank, object>> includeProperty);
        BDSBank GetItem(int id);
        BDSBank CreateItem(BDSBank item);
        bool UpdateItem(BDSBank item);
        bool DeleteItem(int id);
        bool CheckItem(BDSBank item);
    }
}

