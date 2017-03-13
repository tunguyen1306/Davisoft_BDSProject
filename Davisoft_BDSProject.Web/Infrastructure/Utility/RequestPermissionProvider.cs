using System;
using System.Collections.Generic;
using System.Linq;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Web.Infrastructure.Utility
{
    public class RequestPermissionProvider
    {
        public static readonly List<string> LoginRequired = new List<string> { "Home", "Report", "ExportToPdf", "Errors", "Account", "Api" };

        public static Dictionary<string, IList<Permission>> GetPermissions()
        {
            Dictionary<string, List<Tuple<string, string>>> actions = MvcHelper.GetControllerTree();
            var permissions = new Dictionary<string, IList<Permission>>();

            foreach (string controller in actions.Keys.Where(controller => !LoginRequired.Contains(controller)))
            {
                permissions.Add(controller, new List<Permission>());
                foreach (Tuple<string, string> action in actions[controller])
                {
                    permissions[controller].Add(new Permission { Target = controller, Right = action.Item1, DisplayName = action.Item2 });
                }
            }

            return permissions;
        }
    }
}
