using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSEmployerInformationService
    {
        IQueryable<BDSEmployerInformation> GetIQueryableItems();
        IEnumerable<BDSEmployerInformation> GetAllItems(Expression<Func<BDSEmployerInformation, object>> includeProperty);
        BDSEmployerInformation GetItem(int id);
        BDSEmployerInformation CreateItem(BDSEmployerInformation item);
        bool UpdateItem(BDSEmployerInformation item);
        bool DeleteItem(int id);
        bool CheckItem(BDSEmployerInformation item);
    }
}

