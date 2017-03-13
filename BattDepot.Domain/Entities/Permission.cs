using System;
using System.ComponentModel.DataAnnotations.Schema;
using NS;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class Permission : EntityBase
    {
        public string Target { get; set; }
        public string Right { get; set; }
        public string Description { get; set; }

        [NotMapped]
        public string DisplayName { get; set; }
        // LINQ predicade
        public static Func<Permission, bool> HasRight(string target, string right)
        {
            return p => p.Target.ToLower() == target.ToLower() &&
                        p.Right.ToLower() == right.ToLower();
        }
        public static Func<Permission, bool> HasRight(string target, string right, string description)
        {
            return p => p.Target.ToLower() == target.ToLower() &&
                        p.Right.ToLower() == right.ToLower() &&
                        p.Description == description;
        }
    }

    public class PermissionType : Enumeration
    {
        public static readonly PermissionType SettingPermission = New<PermissionType>(1, "Setting Permission");
    }
}
