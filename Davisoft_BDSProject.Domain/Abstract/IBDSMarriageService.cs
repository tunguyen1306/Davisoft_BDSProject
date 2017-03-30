using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSMarriageService
    {
        IQueryable<BDSMarriage> GetIQueryableItems();
        IEnumerable<BDSMarriage> GetAllItems(Expression<Func<BDSMarriage, object>> includeProperty);
        BDSMarriage GetItem(int id);
        BDSMarriage CreateItem(BDSMarriage item);
        bool UpdateItem(BDSMarriage item);
        bool DeleteItem(int id);
        bool CheckItem(BDSMarriage item);
    }
}

