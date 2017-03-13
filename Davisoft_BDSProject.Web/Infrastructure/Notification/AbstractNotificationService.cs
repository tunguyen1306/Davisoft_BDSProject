using System.Web.Mvc;
using NS.Mail;
using MIT.Domain.Abstract;
using MIT.Domain.Entities;

namespace MIT.Web.Infrastructure.Notification
{
    public abstract class AbstractNotificationService
    {
        protected IIndentRepository IndentRepository
        {
            get { return DependencyResolver.Current.GetService<IIndentRepository>(); }
        }

        protected IMembershipService MembershipService
        {
            get { return DependencyResolver.Current.GetService<IMembershipService>(); }
        }

        protected void SendEmail(string from, string to, string subject, RazorMailMessage message)
        {
            Mailer.UseMessage(message)
                  .From(from)
                  .To(to)
                  .Subject(subject)
                  .SendAsync();
        }
        protected void SendEmail(string from, string to, string subject, string body)
        {
            Mailer.NewMessage()
                  .From(from)
                  .To(to)
                  .Subject(subject)
                  .Body(body)
                  .SendAsync();
        }
        protected void SendEmail(string to, string subject, RazorMailMessage message)
        {
            SendEmail("info@subaru-ioffice.com", to, subject, message);
        }
        protected void SendEmail(string to, string subject, string body)
        {
            SendEmail("info@subaru-ioffice.com", to, subject, body);
        }
    }

    public abstract class AbstractNotificationService<T> : AbstractNotificationService, INotificationService<T> where T : class
    {
        #region INotificationService<T> Members

        public abstract void NotifyAll(T entity);

        public abstract void NotifyUser(User user, T entity = null);

        public abstract void NotifyRole(Role role, T entity = null);

        #endregion
    }
}
