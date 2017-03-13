using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NS;

namespace Davisoft_BDSProject.Domain.Enum
{
    public class RoleLevel : Enumeration
    {
        public static readonly RoleLevel None = New<RoleLevel>("", "None");
        public static readonly RoleLevel CompanyLevel = New<RoleLevel>(1, "Company Level");
        public static readonly RoleLevel BranchLevel = New<RoleLevel>(2, "Branch Level");
        public static readonly RoleLevel PersonalLevel = New<RoleLevel>(3, "Personal Level");
    }
}
