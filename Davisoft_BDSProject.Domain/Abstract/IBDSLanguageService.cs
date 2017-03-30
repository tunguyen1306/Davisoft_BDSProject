using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSLanguageService
    {
        IQueryable<BDSLanguage> GetIQueryableItems();
        IEnumerable<BDSLanguage> GetAllItems(Expression<Func<BDSLanguage, object>> includeProperty);
        BDSLanguage GetItem(int id);
        BDSLanguage CreateItem(BDSLanguage item);
        bool UpdateItem(BDSLanguage item);
        bool DeleteItem(int id);
        bool CheckItem(BDSLanguage item);
    }
}

