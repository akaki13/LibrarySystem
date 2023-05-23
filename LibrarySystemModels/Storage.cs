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
        public string Saction { get; set; }
        public string Row { get; set; }
        public string Shell { get; set; }
        public int? LogsId { get; set; }

        public virtual TableLog Logs { get; set; }
        public virtual ICollection<BookStorage> BookStorages { get; set; }
    }
}
