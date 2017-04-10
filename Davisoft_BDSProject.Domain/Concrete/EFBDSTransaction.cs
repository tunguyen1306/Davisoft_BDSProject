using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSTransaction : Repository, IBDSTransactionService
    {
        public EFBDSTransaction(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSTransaction> GetIQueryableItems()
        {
            var q = from a in _db.Set<BDSTransaction>()
                    join b in _db.Set<User>() on a.CreateUser equals b.ID into bs
                    from b in bs.DefaultIfEmpty()
                    select a;
            return q;
        }

        public IEnumerable<Entities.BDSTransaction> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSTransaction, object>> includeProperty)
        {
            return _db.Set<BDSTransaction>();
        }

        public Entities.BDSTransaction GetItem(int id)
        {
            return _db.Set<BDSTransaction>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSTransaction CreateItem(Entities.BDSTransaction item)
        {
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSTransaction item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSTransaction>(id);
        }

        public bool CheckItem(Entities.BDSTransaction item)
        {
            var data = Retrieve<BDSTransaction>(
               T =>
                   T.Active == 1 && T.Name == item.Name);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}
