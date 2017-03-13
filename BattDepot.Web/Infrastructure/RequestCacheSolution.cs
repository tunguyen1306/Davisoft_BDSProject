using System.Web;
using Davisoft_BDSProject.Domain;

namespace Davisoft_BDSProject.Web.Infrastructure
{
    public class RequestCacheSolution : ICacheStorageLocation
    {
        private const string PREFIX = "NS.RequestCacheSolution.";

        #region ICacheStorageLocation Members

        public T Get<T>(string name) where T : class
        {
            return HttpContext.Current.Items[PREFIX + name] as T;
        }

        public T Set<T>(string name, T data) where T : class
        {
            return (HttpContext.Current.Items[PREFIX + name] = data) as T;
        }

        public bool HasKey(string name)
        {
            return HttpContext.Current.Items[PREFIX + name] != null;
        }

        #endregion
    }
}
