using System;
using System.Collections.Generic;

#nullable disable

namespace RoleUser.Models
{
    public partial class Group
    {
        public Group()
        {
            UserNames = new HashSet<UserName>();
        }

        public int Id { get; set; }
        public string GroupName { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<UserName> UserNames { get; set; }
    }
}
