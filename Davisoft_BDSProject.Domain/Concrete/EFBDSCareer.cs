using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSCareer : Repository,IBDSCareerService
    {
        public EFBDSCareer(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSCareer> GetIQueryableItems()
        {
            return _db.Set<BDSCareer>();
        }

        public IEnumerable<Entities.BDSCareer> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSCareer, object>> includeProperty)
        {
            return _db.Set<BDSCareer>();
        }

        public Entities.BDSCareer GetItem(int id)
        {
            return _db.Set<BDSCareer>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSCareer CreateItem(Entities.BDSCareer item)
        {
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSCareer item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSCareer>(id);
        }

        public bool CheckItem(Entities.BDSCareer item)
        {
            var data = Retrieve<BDSCareer>(
               T =>
                   T.Active == 1 && T.Name == item.Name);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}
