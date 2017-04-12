using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSTransactionHistoryService
    {
        IQueryable<BDSTransactionHistory> GetIQueryableItems();
        IEnumerable<BDSTransactionHistory> GetAllItems(Expression<Func<BDSTransactionHistory, object>> includeProperty);
        BDSTransactionHistory GetItem(int id);
        BDSTransactionHistory CreateItem(BDSTransactionHistory item);
        bool UpdateItem(BDSTransactionHistory item);
        bool DeleteItem(int id);
        bool CheckItem(BDSTransactionHistory item);
    }
}

