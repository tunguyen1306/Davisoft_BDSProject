using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSNewsDate : Repository, IBDSNewsDateService
    {
        public EFBDSNewsDate(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSNewsDate> GetIQueryableItems()
        {
            var q = from a in _db.Set<BDSNewsDate>()
                    join b in _db.Set<User>() on a.CreateUser equals b.ID into bs
                    from b in bs.DefaultIfEmpty()
                    select a;
            return q;
        }

        public IEnumerable<Entities.BDSNewsDate> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSNewsDate, object>> includeProperty)
        {
            return _db.Set<BDSNewsDate>();
        }

        public Entities.BDSNewsDate GetItem(int id)
        {
            return _db.Set<BDSNewsDate>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSNewsDate CreateItem(Entities.BDSNewsDate item)
        {
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSNewsDate item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSNewsDate>(id);
        }

        public bool CheckItem(Entities.BDSNewsDate item)
        {
            var data = Retrieve<BDSNewsDate>(
               T =>
                   T.Active == 1 && T.Name == item.Name);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}
