using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IBDSPictureService
    {
        IQueryable<BDSPicture> GetIQueryableItems();
        IEnumerable<BDSPicture> GetAllItems(Expression<Func<BDSPicture, object>> includeProperty);
        BDSPicture GetItem(int id);
        BDSPicture CreateItem(BDSPicture item);
        bool UpdateItem(BDSPicture item);
        bool DeleteItem(int id);
        bool CheckItem(BDSPicture item);
    }
}

