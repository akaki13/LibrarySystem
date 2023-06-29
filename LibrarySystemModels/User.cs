using System;
using System.Collections.Generic;

namespace LibrarySystemModels
{
    public partial class User
    {
        public User()
        {
            RoleUsers = new HashSet<RoleUser>();
            TableLogs = new HashSet<TableLog>();
        }

        public int Id { get; set; }
        public int? PersonId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual Person Person { get; set; }
        public virtual ICollection<RoleUser> RoleUsers { get; set; }
        public virtual ICollection<TableLog> TableLogs { get; set; }
    }
}
