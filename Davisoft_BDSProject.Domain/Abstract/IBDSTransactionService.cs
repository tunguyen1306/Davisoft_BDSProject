using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSTransactionService
    {
        IQueryable<BDSTransaction> GetIQueryableItems();
        IEnumerable<BDSTransaction> GetAllItems(Expression<Func<BDSTransaction, object>> includeProperty);
        BDSTransaction GetItem(int id);
        BDSTransaction CreateItem(BDSTransaction item);
        bool UpdateItem(BDSTransaction item);
        bool DeleteItem(int id);
        bool CheckItem(BDSTransaction item);
    }
}

