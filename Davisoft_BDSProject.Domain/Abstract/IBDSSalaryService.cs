using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSSalaryService
    {
        IQueryable<BDSSalary> GetIQueryableItems();
        IEnumerable<BDSSalary> GetAllItems(Expression<Func<BDSSalary, object>> includeProperty);
        BDSSalary GetItem(int id);
        BDSSalary CreateItem(BDSSalary item);
        bool UpdateItem(BDSSalary item);
        bool DeleteItem(int id);
        bool CheckItem(BDSSalary item);
    }
}

