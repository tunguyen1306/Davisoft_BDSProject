using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSTransactionHistory : Repository, IBDSTransactionHistoryService
    {
        public EFBDSTransactionHistory(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSTransactionHistory> GetIQueryableItems()
        {
            var q = from a in _db.Set<BDSTransactionHistory>()
                    join b in _db.Set<User>() on a.CreateUser equals b.ID into bs
                    from b in bs.DefaultIfEmpty()
                    select a;
            return q;
        }

        public IEnumerable<Entities.BDSTransactionHistory> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSTransactionHistory, object>> includeProperty)
        {
            return _db.Set<BDSTransactionHistory>();
        }

        public Entities.BDSTransactionHistory GetItem(int id)
        {
            return _db.Set<BDSTransactionHistory>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSTransactionHistory CreateItem(Entities.BDSTransactionHistory item)
        {
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSTransactionHistory item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSTransactionHistory>(id);
        }

        public bool CheckItem(Entities.BDSTransactionHistory item)
        {
            var data = Retrieve<BDSTransactionHistory>(
               T =>
                   T.Active == 1 && T.Name == item.Name);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}
