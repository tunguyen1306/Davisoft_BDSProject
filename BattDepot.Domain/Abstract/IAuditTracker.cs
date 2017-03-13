using System;
using System.Collections.Generic;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IAuditTracker
    {
        IEnumerable<Audit> GetAuditRecords();
        IEnumerable<Audit> GetAuditRecords(Func<Audit,bool> predicate);
        IEnumerable<Audit> GetAuditRecords(string username);
        void CreateRecord(Audit record);
        Audit GetRecord(int id);
        IEnumerable<Audit> GetManualAuditRecord(string controller, int id);
    }
}
