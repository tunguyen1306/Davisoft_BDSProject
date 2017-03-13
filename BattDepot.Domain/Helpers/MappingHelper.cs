using AutoMapper;
using NS;

namespace Davisoft_BDSProject.Domain.Helpers
{
    public class EnumerationTypeConverter : ITypeConverter<Enumeration, int>
    {
        #region ITypeConverter<Enumeration,int> Members

        public int Convert(ResolutionContext context)
        {
            return int.Parse(((Enumeration) context.SourceValue).Value);
        }

        #endregion
    }
}
