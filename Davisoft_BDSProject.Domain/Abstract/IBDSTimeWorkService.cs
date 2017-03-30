using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSTimeWorkService
    {
        IQueryable<BDSTimeWork> GetIQueryableItems();
        IEnumerable<BDSTimeWork> GetAllItems(Expression<Func<BDSTimeWork, object>> includeProperty);
        BDSTimeWork GetItem(int id);
        BDSTimeWork CreateItem(BDSTimeWork item);
        bool UpdateItem(BDSTimeWork item);
        bool DeleteItem(int id);
        bool CheckItem(BDSTimeWork item);
    }
}

