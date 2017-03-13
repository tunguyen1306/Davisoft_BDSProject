using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFAuditTracker : Repository, IAuditTracker
    {
        public EFAuditTracker(DbContext db)
            : base(db)
        {
        }

        public IEnumerable<Audit> GetAuditRecords()
        {
            return GetAll<Audit>().OrderByDescending(a => a.TimeAccessed);
        }
        public IEnumerable<Audit> GetAuditRecords(Func<Audit, bool> predicate)
        {
            return GetAll<Audit>(predicate).OrderByDescending(a => a.TimeAccessed);
        }
        public IEnumerable<Audit> GetAuditRecords(string username)
        {
            username = username.ToLower();
            return GetAll<Audit>().Where(j => j.Username != null && j.Username.ToLower() == username.ToLower()).OrderByDescending(a => a.TimeAccessed);
        }

        public void CreateRecord(Audit record)
        {
            Create(record);
        }

        public Audit GetRecord(int id)
        {
            return Get<Audit>(id);
        }

        public IEnumerable<Audit> GetManualAuditRecord(string controller, int id)
        {
            return
                GetAll<Audit>().OrderByDescending(a => a.TimeAccessed).Where(
                    a => a.Controller == controller && a.ActionId == id);
        }
    }
}
