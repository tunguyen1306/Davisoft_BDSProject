using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSEvent : Repository,IBDSEventService
    {
        public EFBDSEvent(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSEvent> GetIQueryableItems()
        {
            var q = from a in _db.Set<BDSEvent>()
                    join b in _db.Set<User>() on a.CreateUser equals b.ID into bs
                    from b in bs.DefaultIfEmpty()
                    select a;
            return q;
        }

        public IEnumerable<Entities.BDSEvent> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSEvent, object>> includeProperty)
        {
            return _db.Set<BDSEvent>();
        }

        public Entities.BDSEvent GetItem(int id)
        {
            return _db.Set<BDSEvent>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSEvent CreateItem(Entities.BDSEvent item)
        {
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSEvent item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSEvent>(id);
        }

        public bool CheckItem(Entities.BDSEvent item)
        {
            var data = Retrieve<BDSEvent>(
               T =>
                   T.Active == 1 && T.Name == item.Name);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}
