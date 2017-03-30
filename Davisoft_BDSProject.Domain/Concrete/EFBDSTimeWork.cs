using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSTimeWork : Repository, IBDSTimeWorkService
    {
        public EFBDSTimeWork(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSTimeWork> GetIQueryableItems()
        {
            return _db.Set<BDSTimeWork>();
        }

        public IEnumerable<Entities.BDSTimeWork> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSTimeWork, object>> includeProperty)
        {
            return _db.Set<BDSTimeWork>();
        }

        public Entities.BDSTimeWork GetItem(int id)
        {
            return _db.Set<BDSTimeWork>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSTimeWork CreateItem(Entities.BDSTimeWork item)
        {
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSTimeWork item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSTimeWork>(id);
        }

        public bool CheckItem(Entities.BDSTimeWork item)
        {
            var data = Retrieve<BDSTimeWork>(
               T =>
                   T.Active == 1 && T.Name == item.Name);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}
