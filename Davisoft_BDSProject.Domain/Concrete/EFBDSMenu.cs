using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSMenu : Repository, IBDSMenuService
    {
        public EFBDSMenu(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSMenu> GetIQueryableItems()
        {
            var q = from a in _db.Set<BDSMenu>()
                    join b in _db.Set<User>() on a.CreateUser equals b.ID into bs
                    from b in bs.DefaultIfEmpty()
                    select a;
            return q;
        }

        public IEnumerable<Entities.BDSMenu> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSMenu, object>> includeProperty)
        {
            return _db.Set<BDSMenu>();
        }

        public Entities.BDSMenu GetItem(int id)
        {
            return _db.Set<BDSMenu>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSMenu CreateItem(Entities.BDSMenu item)
        {
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSMenu item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSMenu>(id);
        }

        public bool CheckItem(Entities.BDSMenu item)
        {
            var data = Retrieve<BDSMenu>(
               T =>
                   T.Active == 1 && T.Name == item.Name);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}
