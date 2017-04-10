using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSTypeContact : Repository, IBDSTypeContactService
    {
        public EFBDSTypeContact(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSTypeContact> GetIQueryableItems()
        {
            var q = from a in _db.Set<BDSTypeContact>()
                    join b in _db.Set<User>() on a.CreateUser equals b.ID into bs
                    from b in bs.DefaultIfEmpty()
                    select a;
            return q;
        }

        public IEnumerable<Entities.BDSTypeContact> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSTypeContact, object>> includeProperty)
        {
            return _db.Set<BDSTypeContact>();
        }

        public Entities.BDSTypeContact GetItem(int id)
        {
            return _db.Set<BDSTypeContact>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSTypeContact CreateItem(Entities.BDSTypeContact item)
        {
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSTypeContact item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSTypeContact>(id);
        }

        public bool CheckItem(Entities.BDSTypeContact item)
        {
            var data = Retrieve<BDSTypeContact>(
               T =>
                   T.Active == 1 && T.Name == item.Name);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}
