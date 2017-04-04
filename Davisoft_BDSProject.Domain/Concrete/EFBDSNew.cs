﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFBDSNew : Repository, IBDSNewService
    {
        public EFBDSNew(DbContext db)
            : base(db)
        {
        }
        public IQueryable<Entities.BDSNew> GetIQueryableItems()
        {
            return _db.Set<BDSNew>();
        }

        public IEnumerable<Entities.BDSNew> GetAllItems(System.Linq.Expressions.Expression<Func<Entities.BDSNew, object>> includeProperty)
        {
            return _db.Set<BDSNew>();
        }

        public Entities.BDSNew GetItem(int id)
        {
            return _db.Set<BDSNew>().Where(T => T.ID == id).FirstOrDefault();
        }

        public Entities.BDSNew CreateItem(Entities.BDSNew item)
        {
            item.Active = 1;
            item.CreateDate = DateTime.Now;
            return Create(item, false);
        }

        public bool UpdateItem(Entities.BDSNew item)
        {
            item.ModifiedDate= DateTime.Now;
            return Update(item);
        }

        public bool DeleteItem(int id)
        {
            return Delete<BDSNew>(id);
        }

        public bool CheckItem(Entities.BDSNew item)
        {
            var data = Retrieve<BDSNew>(
               T =>
                   T.Active == 1 && T.Title == item.Title);

            return
                data.ToList().Count > 0
                    ? true
                    : false;
        }
    }
}