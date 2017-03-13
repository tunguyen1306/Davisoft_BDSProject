using System.Web;
using Davisoft_BDSProject.Domain;

namespace Davisoft_BDSProject.Web.Infrastructure
{
    public class SessionCacheSolution : ICacheStorageLocation
    {
        private const string PREFIX = "NS.SessionCacheSolution.";

        #region ICacheStorageLocation Members

        public T Get<T>(string name) where T : class
        {
            return HttpContext.Current.Session[PREFIX + name] as T;
        }

        public T Set<T>(string name, T data) where T : class
        {
            return (HttpContext.Current.Session[PREFIX + name] = data) as T;
        }

        public bool HasKey(string name)
        {
            return HttpContext.Current.Session[PREFIX + name] != null;
        }

        #endregion
    }
}
