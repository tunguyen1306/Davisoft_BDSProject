using System.Web.Mvc;
using MIT.Domain.Abstract;
using MIT.Domain.Entities;

namespace MIT.Web.Infrastructure.Notification
{
    public static class ConsolidateNotificator
    {
        private static INotificationService<Consolidate> NotificationService
        {
            get { return DependencyResolver.Current.GetService<INotificationService<Consolidate>>(); }
        }

        private static IIndentRepository IndentRepository
        {
            get { return DependencyResolver.Current.GetService<IIndentRepository>(); }
        }

        public static void Notify(int consolidateId)
        {
            Consolidate consolidate = IndentRepository.GetConsolidate(consolidateId);
            NotificationService.NotifyAll(consolidate);
        }

        public static void Notify(Consolidate consolidate)
        {
            NotificationService.NotifyAll(consolidate);
        }
    }
}
