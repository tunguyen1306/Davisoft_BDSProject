using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSCommentService
    {
        IQueryable<BDSComment> GetIQueryableItems();
        IEnumerable<BDSComment> GetAllItems(Expression<Func<BDSComment, object>> includeProperty);
        BDSComment GetItem(int id);
        BDSComment CreateItem(BDSComment item);
        bool UpdateItem(BDSComment item);
        bool DeleteItem(int id);
        bool CheckItem(BDSComment item);
    }
}

