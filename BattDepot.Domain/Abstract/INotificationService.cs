using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface INotificationService<T> where T : class
    {
        void NotifyAll(T entity);
        void NotifyUser(User user, T entity = null);
        void NotifyRole(Role role, T entity = null);
    }
}
