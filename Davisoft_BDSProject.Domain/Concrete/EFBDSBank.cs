using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSBank : Repository,IBDSBankService
    {
        public EFBDSBank(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSBank> GetIQueryableItems()
        {
            return _db.Set<BDSBank>();
        }

        public IEnumerable<Entities.BDSBank> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSBank, object>> includeProperty)
        {
            return _db.Set<BDSBank>();
        }

        public Entities.BDSBank GetItem(int id)
        {
            return _db.Set<BDSBank>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSBank CreateItem(Entities.BDSBank item)
        {
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSBank item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSBank>(id);
        }

        public bool CheckItem(Entities.BDSBank item)
        {
            var data = Retrieve<BDSBank>(
               T =>
                   T.Active == 1 && T.Name == item.Name);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}
