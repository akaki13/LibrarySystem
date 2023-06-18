using System;
using System.Collections.Generic;

namespace LibrarySystemModels
{
    public partial class Storage
    {
        public Storage()
        {
            BookStorages = new HashSet<BookStorage>();
        }

        public int Id { get; set; }
        public int LogsId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }

        public virtual TableLog Logs { get; set; }
        public virtual ICollection<BookStorage> BookStorages { get; set; }
    }
}
