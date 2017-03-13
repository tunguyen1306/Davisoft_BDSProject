using System.Collections.Generic;
using System.Linq;
using NS.Mail;
using MIT.Domain.Entities;
using MIT.Domain.Enum;
using MIT.Web.Hubs;
using MIT.Web.Infrastructure.Utility;

namespace MIT.Web.Infrastructure.Notification
{
    public class ConfirmDocumentNotificationService : AbstractNotificationService<ConfirmDocument>
    {
        #region Override

        public override void NotifyAll(ConfirmDocument confirmDoc)
        {
            NotificationHub.Notify(confirmDoc);
            SendNotifyEmail(confirmDoc);
        }

        public override void NotifyUser(User user, ConfirmDocument confirmDoc = null)
        {
        }

        public override void NotifyRole(Role role, ConfirmDocument confirmDoc = null)
        {
        }

        #endregion

        private void SendNotifyEmail(ConfirmDocument confirmDoc)
        {
            if (confirmDoc == null) return;

            if (confirmDoc.StatusRecord.Status == LCStatus.Requested)
                SendEmail_ConfirmDocument_Updated(confirmDoc);
            else if (confirmDoc.StatusRecord.Status == LCStatus.Applied)
                SendEmail_ConfirmDocument_WaitForFinanceConfirm(confirmDoc);
            else if (confirmDoc.StatusRecord.Status == LCStatus.Denied)
                SendEmail_ConfirmDocument_Archived(confirmDoc);
            else if (confirmDoc.StatusRecord.Status == LCStatus.Opened)
                SendEmail_ConfirmDocument_Open(confirmDoc);
            //else if (confirmDoc.StatusRecord.Status == LCStatus.Paid)
            //    SendEmail_ConfirmDocument_Paid(confirmDoc);
        }

        private void SendEmail_ConfirmDocument_Updated(ConfirmDocument confirmDoc)
        {
            List<LCStatusRecord> statusRecords = IndentRepository.GetAllConfirmDocumentStatusRecords(confirmDoc.ID).ToList();
            string subject = "LC #" + confirmDoc.ID + " " + (statusRecords.Count() == 1 ? "created" : "updated");

            //IEnumerable<User> users = MembershipService.GetUsersInRole("Finance User");
            IEnumerable<User> users = MembershipService.GetAllUserOfEmailTemplate("ConfirmDocument", "Initial");
            foreach (User u in users.Where(f => !string.IsNullOrWhiteSpace(f.Email)))
            {
                var viewBag = new { User = u, StatusRecords = statusRecords };
                var message = new RazorMailMessage("ConfirmDocument/Initial", confirmDoc, viewBag).Render();
                SendEmail(u.Email, subject, message);
            }
        }

        private void SendEmail_ConfirmDocument_WaitForFinanceConfirm(ConfirmDocument confirmDoc)
        {
            const string subject = "LC approved";
            var message = new RazorMailMessage("ConfirmDocument/Confirmed", confirmDoc).Render();
            SendEmail("bank@localhost", subject, message);
        }

        private void SendEmail_ConfirmDocument_Archived(ConfirmDocument confirmDoc)
        {
            const string subject = "LC is denied";
            var message = new RazorMailMessage("ConfirmDocument/Denied", confirmDoc).Render();
            //IEnumerable<User> users = MembershipService.GetUsersInRole("General Manager");
            IEnumerable<User> users = MembershipService.GetAllUserOfEmailTemplate("ConfirmDocument", "Denied");
            foreach (User u in users.Where(f => !string.IsNullOrWhiteSpace(f.Email)))
            {
                SendEmail(u.Email, subject, message);
            }
        }

        private void SendEmail_ConfirmDocument_Open(ConfirmDocument confirmDoc)
        {
            const string subject = "LC Open";
            var message = new RazorMailMessage("ConfirmDocument/OpenLC", confirmDoc).Render();
            //IEnumerable<User> users = MembershipService.GetUsersInRole("General Manager");
            IEnumerable<User> users = MembershipService.GetAllUserOfEmailTemplate("ConfirmDocument", "OpenLC");
            foreach (User u in users.Where(f => !string.IsNullOrWhiteSpace(f.Email)))
            {
                SendEmail(u.Email, subject, message);
            }
            User requestUser = MembershipService.GetUser(confirmDoc.RequestUserID);
            SendEmail(requestUser.Email, subject, message);
        }
    }
}
