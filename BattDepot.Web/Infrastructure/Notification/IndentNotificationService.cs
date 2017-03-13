using System.Collections.Generic;
using System.Linq;
using NS.Mail;
using MIT.Domain.Entities;
using MIT.Domain.Enum;
using MIT.Web.Hubs;
using MIT.Web.Infrastructure.Utility;

namespace MIT.Web.Infrastructure.Notification
{
    public class IndentNotificationService : AbstractNotificationService<Indent>
    {
        #region Override

        public override void NotifyAll(Indent indent)
        {
            NotificationHub.Notify(indent);
            SendNotifyEmail(indent);
        }

        public override void NotifyUser(User user, Indent indent = null)
        {
        }

        public override void NotifyRole(Role role, Indent indent = null)
        {
        }

        #endregion

        private void SendNotifyEmail(Indent indent)
        {
            if (indent.StatusRecord.Status == IndentStatus.Initial)
                SendEmail_IndentUpdate(indent);
            else if (indent.StatusRecord.Status == IndentStatus.Denied)
                SendEmail_IndentDenied(indent);
            else if (indent.StatusRecord.Status == IndentStatus.Cancelled)
                SendEmail_IndentCancelled(indent);
            else if (indent.StatusRecord.Status == IndentStatus.Confirm)
                SendEmail_IndentConfirmed(indent);
        }

        private void SendEmail_IndentConfirmed(Indent indent)
        {
            string subject = "Indent no " + indent.IndentNo + " has been confirmed";
            var message = new RazorMailMessage("Indent/Confirmed", indent).Render();
            //IEnumerable<User> users = MembershipService.GetUsersInRole("Indent Manager");
            IEnumerable<User> users = MembershipService.GetAllUserOfEmailTemplate("Indent", "Confirmed");
            foreach (User u in users.Where(m => m.Email != null))
            {
                SendEmail(u.Email, subject, message);
            }
        }

        private void SendEmail_IndentUpdate(Indent indent)
        {
            List<IndentStatusRecord> statusRecords = IndentRepository.GetIndentStatusRecords(indent.ID).ToList();

            string subject = "Indent no " + indent.IndentNo + " has been " + (statusRecords.Count() == 1 ? "created" : "updated");

            //IEnumerable<User> users = MembershipService.GetUsersInRole("Indent Manager");
            IEnumerable<User> users = MembershipService.GetAllUserOfEmailTemplate("Indent", "Initial");
            foreach (User u in users.Where(m => m.Email != null))
            {
                var viewBag = new { User = u, StatusRecords = statusRecords };
                var message = new RazorMailMessage("Indent/Initial", indent, viewBag).Render();
                SendEmail(u.Email, subject, message);
            }
        }

        private void SendEmail_IndentDenied(Indent indent)
        {
            string subject = "Indent no " + indent.IndentNo + " has been denied";
            var message = new RazorMailMessage("Indent/Denied", indent).Render();
            //IEnumerable<User> users = MembershipService.GetUsersInRole("Indent User");
            IEnumerable<User> users = MembershipService.GetAllUserOfEmailTemplate("Indent", "Denied");
            foreach (User u in users.Where(m => m.Email != null))
            {
                SendEmail(u.Email, subject, message);
            }
        }

        private void SendEmail_IndentCancelled(Indent indent)
        {
            string subject = "Indent no " + indent.IndentNo + " has been cancelled";
            var message = new RazorMailMessage("Indent/Cancelled", indent).Render();
            //IEnumerable<User> users = MembershipService.GetUsersInRole("Indent Manager");
            IEnumerable<User> users = MembershipService.GetAllUserOfEmailTemplate("Indent", "Cancelled");
            foreach (User u in users.Where(m => m.Email != null))
            {
                SendEmail(u.Email, subject, message);
            }
        }
    }
}
