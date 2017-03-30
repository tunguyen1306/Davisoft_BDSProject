using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSNewsTypePrice : Repository, IBDSNewsTypePriceService
    {
        public EFBDSNewsTypePrice(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSNewsTypePrice> GetIQueryableItems()
        {
            return _db.Set<BDSNewsTypePrice>();
        }

        public IEnumerable<Entities.BDSNewsTypePrice> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSNewsTypePrice, object>> includeProperty)
        {
            return _db.Set<BDSNewsTypePrice>();
        }

        public Entities.BDSNewsTypePrice GetItem(int id)
        {
            return _db.Set<BDSNewsTypePrice>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSNewsTypePrice CreateItem(Entities.BDSNewsTypePrice item)
        {
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSNewsTypePrice item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSNewsTypePrice>(id);
        }

        public bool CheckItem(Entities.BDSNewsTypePrice item)
        {
            var data = Retrieve<BDSNewsTypePrice>(
               T =>
                   T.Active == 1 && T.Name == item.Name);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}
