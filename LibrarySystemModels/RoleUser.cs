using System;
using System.Collections.Generic;

namespace LibrarySystemModels
{
    public partial class RoleUser
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? UsersId { get; set; }
        public int LogsId { get; set; }

        public virtual TableLog Logs { get; set; }
        public virtual Role Role { get; set; }
        public virtual User Users { get; set; }
    }
}
