using System;
using System.Collections.Generic;

#nullable disable

namespace RoleUser.Models
{
    public partial class UserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool? Status { get; set; }

        public virtual Role Role { get; set; }
        public virtual UserName User { get; set; }
    }
}
