using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSAccount : Repository, IBDSAccountService
    {
        public EFBDSAccount(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSAccount> GetIQueryableItems()
        {
            return _db.Set<BDSAccount>();
        }

        public IEnumerable<Entities.BDSAccount> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSAccount, object>> includeProperty)
        {
            return _db.Set<BDSAccount>();
        }

        public Entities.BDSAccount GetItem(int id)
        {
            return _db.Set<BDSAccount>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSAccount CreateItem(Entities.BDSAccount item)
        {
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSAccount item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSAccount>(id);
        }

        public bool CheckItem(Entities.BDSAccount item)
        {
            var data = Retrieve<BDSAccount>(
               T =>
                   T.Active == 1 && T.Email == item.Email);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}
