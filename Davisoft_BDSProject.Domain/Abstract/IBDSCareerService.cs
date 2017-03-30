using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSCareerService
    {
        IQueryable<BDSCareer> GetIQueryableItems();
        IEnumerable<BDSCareer> GetAllItems(Expression<Func<BDSCareer, object>> includeProperty);
        BDSCareer GetItem(int id);
        BDSCareer CreateItem(BDSCareer item);
        bool UpdateItem(BDSCareer item);
        bool DeleteItem(int id);
        bool CheckItem(BDSCareer item);
    }
}

