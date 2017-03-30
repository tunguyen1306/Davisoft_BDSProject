using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSArea : Repository, IBDSAreaService
    {
        public EFBDSArea(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSArea> GetIQueryableItems()
        {
            return _db.Set<BDSArea>();
        }

        public IEnumerable<Entities.BDSArea> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSArea, object>> includeProperty)
        {
            return _db.Set<BDSArea>();
        }

        public Entities.BDSArea GetItem(int id)
        {
            return _db.Set<BDSArea>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSArea CreateItem(Entities.BDSArea item)
        {
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSArea item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSArea>(id);
        }

        public bool CheckItem(Entities.BDSArea item)
        {
            var data = Retrieve<BDSArea>(
               T =>
                   T.Active == 1 && T.Name == item.Name);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}
