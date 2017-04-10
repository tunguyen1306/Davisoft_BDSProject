using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSPersonalInformation : Repository, IBDSPersonalInformationService
    {
        public EFBDSPersonalInformation(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSPersonalInformation> GetIQueryableItems()
        {
            var q = from a in _db.Set<BDSPersonalInformation>()
                    join b in _db.Set<User>() on a.CreateUser equals b.ID into bs
                    from b in bs.DefaultIfEmpty()
                    select a;
            return q;
        }

        public IEnumerable<Entities.BDSPersonalInformation> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSPersonalInformation, object>> includeProperty)
        {
            return _db.Set<BDSPersonalInformation>();
        }

        public Entities.BDSPersonalInformation GetItem(int id)
        {
            return _db.Set<BDSPersonalInformation>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSPersonalInformation CreateItem(Entities.BDSPersonalInformation item)
        {
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSPersonalInformation item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSPersonalInformation>(id);
        }

        public bool CheckItem(Entities.BDSPersonalInformation item)
        {
            var data = Retrieve<BDSPersonalInformation>(
               T =>
                   T.Active == 1 && T.Name == item.Name);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}
