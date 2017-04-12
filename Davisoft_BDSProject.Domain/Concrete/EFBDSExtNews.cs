using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSExtNews : Repository, IBDSExtNewsService
    {
        public EFBDSExtNews(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSExtNews> GetIQueryableItems()
        {
            var q = from a in _db.Set<BDSExtNews>()
                    join b in _db.Set<User>() on a.CreateUser equals b.ID into bs
                    from b in bs.DefaultIfEmpty()
                    select a;
            return q;
        }

        public IEnumerable<Entities.BDSExtNews> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSExtNews, object>> includeProperty)
        {
            return _db.Set<BDSExtNews>();
        }

        public Entities.BDSExtNews GetItem(int id)
        {
            return _db.Set<BDSExtNews>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSExtNews CreateItem(Entities.BDSExtNews item)
        {
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSExtNews item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSExtNews>(id);
        }

        public bool CheckItem(Entities.BDSExtNews item)
        {
            var data = Retrieve<BDSExtNews>(
               T =>
                   T.Active == 1 && T.Name == item.Name);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}
