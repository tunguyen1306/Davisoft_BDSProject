using System;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class Currency : EntityBase, IEquatable<Currency>, IEquatable<string>
    {
        public Currency()
        {
            //Set default value
            Precision = 2;
        }

        public string Name { get; set; }
        public string Symbol { get; set; }
        public bool IsDefault { get; set; }
        public int Precision { get; set; }

        #region IEquatable<Currency> Members

        public bool Equals(Currency other)
        {
            if (ReferenceEquals(this, other))
                return true;

            return ID == other.ID;
        }

        #endregion

        #region IEquatable<string> Members

        public bool Equals(string otherName)
        {
            return Name.Equals(otherName, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion

        public override string ToString()
        {
            return Name;
        }

        public string Format(decimal value, bool withUnit = false)
        {
            string format = "N" + Precision;
            return (!withUnit ? "" : Symbol + " ") + value.ToString(format);
        }
    }
}
