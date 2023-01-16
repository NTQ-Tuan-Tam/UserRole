using System;
using System.Collections.Generic;

#nullable disable

namespace RoleUser.Models
{
    public partial class User
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? GroupId { get; set; }
        public bool? Status { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
