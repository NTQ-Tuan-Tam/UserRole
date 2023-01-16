using System;
using System.Collections.Generic;

#nullable disable

namespace RoleUser.Models
{
    public partial class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool? Action { get; set; }
        public string Controller { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
