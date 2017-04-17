using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSComment : Repository, IBDSCommentService
    {
        public EFBDSComment(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSComment> GetIQueryableItems()
        {
            var q = from a in _db.Set<BDSComment>()
                    join b in _db.Set<User>() on a.CreateUser equals b.ID into bs
                    from b in bs.DefaultIfEmpty()
                    select a;
            return q;
        }

        public IEnumerable<Entities.BDSComment> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSComment, object>> includeProperty)
        {
            return _db.Set<BDSComment>();
        }

        public Entities.BDSComment GetItem(int id)
        {
            return _db.Set<BDSComment>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSComment CreateItem(Entities.BDSComment item)
        {
            
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSComment item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSComment>(id);
        }

        public bool CheckItem(Entities.BDSComment item)
        {
            var data = Retrieve<BDSComment>(
               T =>
                   T.Active == 1 && T.Name == item.Name);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}
