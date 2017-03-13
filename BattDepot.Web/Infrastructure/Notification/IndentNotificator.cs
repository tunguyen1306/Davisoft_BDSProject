using System.Web.Mvc;
using MIT.Domain.Abstract;
using MIT.Domain.Entities;

namespace MIT.Web.Infrastructure.Notification
{
    public static class IndentNotificator
    {
        private static INotificationService<Indent> NotificationService
        {
            get { return DependencyResolver.Current.GetService<INotificationService<Indent>>(); }
        }

        private static IIndentRepository IndentRepository
        {
            get { return DependencyResolver.Current.GetService<IIndentRepository>(); }
        }

        public static void Notify(int indentId)
        {
            Indent indent = IndentRepository.GetIndent(indentId);
            NotificationService.NotifyAll(indent);
        }
    }
}
