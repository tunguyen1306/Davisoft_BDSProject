using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSBanner : Repository, IBDSBannerService
    {
        public EFBDSBanner(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSBanner> GetIQueryableItems()
        {
            var q = from a in _db.Set<BDSBanner>()
                    join b in _db.Set<User>() on a.CreateUser equals b.ID into bs
                    from b in bs.DefaultIfEmpty()
                    select a;
            return q;
        }

        public IEnumerable<Entities.BDSBanner> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSBanner, object>> includeProperty)
        {
            return _db.Set<BDSBanner>();
        }

        public Entities.BDSBanner GetItem(int id)
        {
            return _db.Set<BDSBanner>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSBanner CreateItem(Entities.BDSBanner item)
        {
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSBanner item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSBanner>(id);
        }

        public bool CheckItem(Entities.BDSBanner item)
        {
            var data = Retrieve<BDSBanner>(
               T =>
                   T.Active == 1 && T.Name == item.Name);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}
