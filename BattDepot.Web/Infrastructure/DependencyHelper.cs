using System.Web.Mvc;

namespace Davisoft_BDSProject.Web.Infrastructure
{
    public class DependencyHelper
    {
        public static T GetService<T>()
        {
            return DependencyResolver.Current.GetService<T>();
        }
    }
}
