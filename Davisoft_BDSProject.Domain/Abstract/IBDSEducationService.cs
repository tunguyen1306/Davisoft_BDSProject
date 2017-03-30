using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSEducationService
    {
        IQueryable<BDSEducation> GetIQueryableItems();
        IEnumerable<BDSEducation> GetAllItems(Expression<Func<BDSEducation, object>> includeProperty);
        BDSEducation GetItem(int id);
        BDSEducation CreateItem(BDSEducation item);
        bool UpdateItem(BDSEducation item);
        bool DeleteItem(int id);
        bool CheckItem(BDSEducation item);
    }
}

