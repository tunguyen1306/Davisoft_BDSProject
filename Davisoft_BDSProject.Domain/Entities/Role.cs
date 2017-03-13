using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Validators;
using Davisoft_BDSProject.Domain.Enum;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class Role : EntityBase
    {
        public Role()
        {
            Permissions = new HashSet<Permission>();
        }

        // Primitive properties
        public string Name { get; set; }
        public string Description { get; set; }

        public RoleLevel RoleLevel { get; set; }

        // Navigation properties
        public virtual ICollection<Permission> Permissions { get; set; }

        // LINQ expression methods
        public static Func<Role, bool> HasName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return r => false;

            return r => r.Name.Trim().ToLower() == name.Trim().ToLower();
        }
    }
}
