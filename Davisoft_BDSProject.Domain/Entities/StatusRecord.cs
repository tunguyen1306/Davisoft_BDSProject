using System;
using System.Linq;
using Davisoft_BDSProject.Domain.Helpers;
using NS;

namespace Davisoft_BDSProject.Domain.Entities
{
    public abstract class StatusRecord<T> : EntityBase
        where T : Enumeration
    {
        #region Factory

        protected StatusRecord(int entryID)
            : this(entryID, Enumeration.GetAll<T>().FirstOrDefault())
        {
        }

        protected StatusRecord(int entryID, T status, string message = "")
            : this()
        {
            EntryId = entryID;
            Status = status;
            Message = message;
        }

        protected StatusRecord()
        {
            UpdatedTime = DateTime.Now;
        }

        #endregion

        #region Properties

        public string Message { get; set; }
        public int EntryId { get; set; }
        public DateTime UpdatedTime { get; set; }
        public T Status { get; set; }

        #endregion

        #region Conversion

        public static implicit operator T(StatusRecord<T> record)
        {
            return record.Status;
        }

        #endregion

        public override string ToString()
        {
            return Status.DisplayName;
        }
    }
}
