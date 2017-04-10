using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSNewsType : Repository, IBDSNewsTypeService
    {
        public EFBDSNewsType(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSNewsType> GetIQueryableItems()
        {
            var q = from a in _db.Set<BDSNewsType>()
                    join b in _db.Set<User>() on a.CreateUser equals b.ID into bs
                    from b in bs.DefaultIfEmpty()
                    select a;
            return q;
        }

        public IEnumerable<Entities.BDSNewsType> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSNewsType, object>> includeProperty)
        {
            return _db.Set<BDSNewsType>();
        }

        public Entities.BDSNewsType GetItem(int id)
        {
            return _db.Set<BDSNewsType>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSNewsType CreateItem(Entities.BDSNewsType item)
        {
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSNewsType item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSNewsType>(id);
        }

        public bool CheckItem(Entities.BDSNewsType item)
        {
            var data = Retrieve<BDSNewsType>(
               T =>
                   T.Active == 1 && T.Name == item.Name);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}
