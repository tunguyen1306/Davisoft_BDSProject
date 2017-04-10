using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSEducation : Repository,IBDSEducationService
    {
        public EFBDSEducation(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSEducation> GetIQueryableItems()
        {
            var q = from a in _db.Set<BDSEducation>()
                    join b in _db.Set<User>() on a.CreateUser equals b.ID into bs
                    from b in bs.DefaultIfEmpty()
                    select a;
            return q;
        }

        public IEnumerable<Entities.BDSEducation> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSEducation, object>> includeProperty)
        {
            return _db.Set<BDSEducation>();
        }

        public Entities.BDSEducation GetItem(int id)
        {
            return _db.Set<BDSEducation>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSEducation CreateItem(Entities.BDSEducation item)
        {
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSEducation item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSEducation>(id);
        }

        public bool CheckItem(Entities.BDSEducation item)
        {
            var data = Retrieve<BDSEducation>(
               T =>
                   T.Active == 1 && T.Name == item.Name);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}
