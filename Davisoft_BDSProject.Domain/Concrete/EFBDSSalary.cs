using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSSalary : Repository, IBDSSalaryService
    {
        public EFBDSSalary(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSSalary> GetIQueryableItems()
        {
            var q = from a in _db.Set<BDSSalary>()
                    join b in _db.Set<User>() on a.CreateUser equals b.ID into bs
                    from b in bs.DefaultIfEmpty()
                    select a;
            return q;
        }

        public IEnumerable<Entities.BDSSalary> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSSalary, object>> includeProperty)
        {
            return _db.Set<BDSSalary>();
        }

        public Entities.BDSSalary GetItem(int id)
        {
            return _db.Set<BDSSalary>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSSalary CreateItem(Entities.BDSSalary item)
        {
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSSalary item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSSalary>(id);
        }

        public bool CheckItem(Entities.BDSSalary item)
        {
            var data = Retrieve<BDSSalary>(
               T =>
                   T.Active == 1 && T.Name == item.Name);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}
