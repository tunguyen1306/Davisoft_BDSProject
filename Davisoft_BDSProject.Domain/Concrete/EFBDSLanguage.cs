using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSLanguage : Repository,IBDSLanguageService
    {
        public EFBDSLanguage(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSLanguage> GetIQueryableItems()
        {
            return _db.Set<BDSLanguage>();
        }

        public IEnumerable<Entities.BDSLanguage> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSLanguage, object>> includeProperty)
        {
            return _db.Set<BDSLanguage>();
        }

        public Entities.BDSLanguage GetItem(int id)
        {
            return _db.Set<BDSLanguage>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSLanguage CreateItem(Entities.BDSLanguage item)
        {
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSLanguage item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSLanguage>(id);
        }

        public bool CheckItem(Entities.BDSLanguage item)
        {
            var data = Retrieve<BDSLanguage>(
               T =>
                   T.Active == 1 && T.Name == item.Name);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}
