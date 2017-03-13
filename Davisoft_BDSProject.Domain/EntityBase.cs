using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Davisoft_BDSProject.Domain
{
    public abstract class EntityBase : IEquatable<EntityBase>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        #region IEquatable<EntityBase> Members

        public bool Equals(EntityBase other)
        {
            return ID != 0 && ID == other.ID;
        }

        #endregion

        #region Comparison

        public override int GetHashCode()
        {
            return ID;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            return GetType().IsInstanceOfType(obj) &&
                   Equals((EntityBase)obj);
        }

        #endregion
    }
}
