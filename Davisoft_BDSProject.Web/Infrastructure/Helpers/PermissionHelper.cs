using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Davisoft_BDSProject.Domain.Enum;
using Davisoft_BDSProject.Web.Infrastructure.Utility;

namespace Davisoft_BDSProject.Web.Infrastructure.Helpers
{
    public class PermissionHelper
    {
        public static int SalesmanId()
        {
            if (CurrentUser.Identity == null || CurrentUser.HasRoleLevel(RoleLevel.CompanyLevel) || CurrentUser.HasRoleLevel(RoleLevel.BranchLevel))
            {
                return 0;
            }
            return CurrentUser.Identity.ID;
        }
        public static int BranchId()
        {
            if (CurrentUser.HasRoleLevel(RoleLevel.CompanyLevel) || CurrentUser.Identity == null)
            {
                return 0;
            }
            if (CurrentUser.Identity != null)
            {
                return CurrentUser.Identity.BranchID ?? 0;
            }
            return 0;
        }
    }
}