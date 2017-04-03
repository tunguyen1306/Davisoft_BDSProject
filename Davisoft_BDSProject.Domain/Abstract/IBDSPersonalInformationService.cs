using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSPersonalInformationService
    {
        IQueryable<BDSPersonalInformation> GetIQueryableItems();
        IEnumerable<BDSPersonalInformation> GetAllItems(Expression<Func<BDSPersonalInformation, object>> includeProperty);
        BDSPersonalInformation GetItem(int id);
        BDSPersonalInformation CreateItem(BDSPersonalInformation item);
        bool UpdateItem(BDSPersonalInformation item);
        bool DeleteItem(int id);
        bool CheckItem(BDSPersonalInformation item);
    }
}

