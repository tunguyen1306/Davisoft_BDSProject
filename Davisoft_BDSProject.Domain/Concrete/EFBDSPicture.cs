using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSPicture : Repository, IBDSPictureService
    {
        public EFBDSPicture(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSPicture> GetIQueryableItems()
        {
            return _db.Set<BDSPicture>();
        }

        public IEnumerable<Entities.BDSPicture> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSPicture, object>> includeProperty)
        {
            return _db.Set<BDSPicture>();
        }

        public Entities.BDSPicture GetItem(int id)
        {
            return _db.Set<BDSPicture>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSPicture CreateItem(Entities.BDSPicture item)
        {
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSPicture item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSPicture>(id);
        }

        public bool CheckItem(Entities.BDSPicture item)
        {
            var data = Retrieve<BDSPicture>(
               T =>
                   T.Active == 1 && T.id == item.id);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}
