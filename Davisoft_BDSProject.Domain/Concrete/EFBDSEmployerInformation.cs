using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSEmployerInformation : Repository, IBDSEmployerInformationService
    {
        public EFBDSEmployerInformation(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSEmployerInformation> GetIQueryableItems()
        {
            return _db.Set<BDSEmployerInformation>();
        }

        public IEnumerable<Entities.BDSEmployerInformation> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSEmployerInformation, object>> includeProperty)
        {
            return _db.Set<BDSEmployerInformation>();
        }

        public Entities.BDSEmployerInformation GetItem(int id)
        {
            return _db.Set<BDSEmployerInformation>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSEmployerInformation CreateItem(Entities.BDSEmployerInformation item)
        {
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSEmployerInformation item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSEmployerInformation>(id);
        }

        public bool CheckItem(Entities.BDSEmployerInformation item)
        {
            var data = Retrieve<BDSEmployerInformation>(
               T =>
                   T.Active == 1 && T.Name == item.Name);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}
