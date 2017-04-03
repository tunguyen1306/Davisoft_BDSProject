using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSAccountService
    {
        IQueryable<BDSAccount> GetIQueryableItems();
        IEnumerable<BDSAccount> GetAllItems(Expression<Func<BDSAccount, object>> includeProperty);
        BDSAccount GetItem(int id);
        BDSAccount CreateItem(BDSAccount item);
        bool UpdateItem(BDSAccount item);
        bool DeleteItem(int id);
        bool CheckItem(BDSAccount item);
    }
}

