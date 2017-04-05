using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSBranchService
    {
        IQueryable<BDSBranch> GetIQueryableItems();
        IEnumerable<BDSBranch> GetAllItems(Expression<Func<BDSBranch, object>> includeProperty);
        BDSBranch GetItem(int id);
        BDSBranch CreateItem(BDSBranch item);
        bool UpdateItem(BDSBranch item);
        bool DeleteItem(int id);
        bool CheckItem(BDSBranch item);
    }
}

