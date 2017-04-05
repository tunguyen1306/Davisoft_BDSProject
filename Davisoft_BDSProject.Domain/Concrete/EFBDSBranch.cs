using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSBranch : Repository, IBDSBranchService
    {
        public EFBDSBranch(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSBranch> GetIQueryableItems()
        {
            return _db.Set<BDSBranch>();
        }

        public IEnumerable<Entities.BDSBranch> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSBranch, object>> includeProperty)
        {
            return _db.Set<BDSBranch>();
        }

        public Entities.BDSBranch GetItem(int id)
        {
            return _db.Set<BDSBranch>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSBranch CreateItem(Entities.BDSBranch item)
        {
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSBranch item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSBranch>(id);
        }

        public bool CheckItem(Entities.BDSBranch item)
        {
            var data = Retrieve<BDSBranch>(
               T =>
                   T.Active == 1 && T.Name == item.Name);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}
