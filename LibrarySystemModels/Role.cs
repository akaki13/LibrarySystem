using System;
using System.Collections.Generic;

namespace LibrarySystemModels
{
    public partial class Role
    {
        public Role()
        {
            RoleUsers = new HashSet<RoleUser>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int? LogsId { get; set; }

        public virtual TableLog Logs { get; set; }
        public virtual ICollection<RoleUser> RoleUsers { get; set; }
    }
}
