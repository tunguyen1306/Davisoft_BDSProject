using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSMarriage : Repository,IBDSMarriageService
    {
        public EFBDSMarriage(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSMarriage> GetIQueryableItems()
        {
            var q = from a in _db.Set<BDSMarriage>()
                    join b in _db.Set<User>() on a.CreateUser equals b.ID into bs
                    from b in bs.DefaultIfEmpty()
                    select a;
            return q;
        }

        public IEnumerable<Entities.BDSMarriage> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSMarriage, object>> includeProperty)
        {
            return _db.Set<BDSMarriage>();
        }

        public Entities.BDSMarriage GetItem(int id)
        {
            return _db.Set<BDSMarriage>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSMarriage CreateItem(Entities.BDSMarriage item)
        {
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSMarriage item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSMarriage>(id);
        }

        public bool CheckItem(Entities.BDSMarriage item)
        {
            var data = Retrieve<BDSMarriage>(
               T =>
                   T.Active == 1 && T.Name == item.Name);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}
