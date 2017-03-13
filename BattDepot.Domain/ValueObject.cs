using System.Linq;
using System.Reflection;

namespace Davisoft_BDSProject.Domain
{
    //public abstract class ValueObject<T> where T : class
    //{
    //    protected ValueObject(T source)
    //    {
    //        RetrieveValue(source);
    //    }

    //    protected ValueObject()
    //    {
    //    }

    //    public ValueObject<T> RetrieveValue(T source)
    //    {
    //        Map(source, this);
    //        return this;
    //    }

    //    public ValueObject<T> PopulateValue(T desc)
    //    {
    //        Map(this, desc);
    //        return this;
    //    }

    //    protected static void Map(object source, object desc)
    //    {
    //        const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
    //        PropertyInfo[] descProperties = desc.GetType().GetProperties(bindingFlags);
    //        PropertyInfo[] sourceProperties = source.GetType().GetProperties(bindingFlags);

    //        foreach (PropertyInfo descProperty in descProperties)
    //        {
    //            PropertyInfo sourceProperty = sourceProperties.FirstOrDefault(p => p.Name == descProperty.Name);
    //            if (sourceProperty != null)
    //                descProperty.SetValue(desc, sourceProperty.GetValue(source, null), null);
    //        }
    //    }
    //}

    public abstract class ValueObject<T> : NS.ValueObject<T>where T:class 
    {
        protected ValueObject(T source)
            :base (source)
        {
        }

        protected ValueObject()
        {
        }

    }
}
