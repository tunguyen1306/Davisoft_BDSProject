using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSScope : Repository, IBDSScopeService
    {
        public EFBDSScope(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSScope> GetIQueryableItems()
        {
            return _db.Set<BDSScope>();
        }

        public IEnumerable<Entities.BDSScope> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSScope, object>> includeProperty)
        {
            return _db.Set<BDSScope>();
        }

        public Entities.BDSScope GetItem(int id)
        {
            return _db.Set<BDSScope>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSScope CreateItem(Entities.BDSScope item)
        {
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSScope item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSScope>(id);
        }

        public bool CheckItem(Entities.BDSScope item)
        {
            var data = Retrieve<BDSScope>(
               T =>
                   T.Active == 1 && T.Name == item.Name);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}
