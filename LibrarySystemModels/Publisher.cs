using System;
using System.Collections.Generic;

namespace LibrarySystemModels
{
    public partial class Publisher
    {
        public Publisher()
        {
            BookPublishers = new HashSet<BookPublisher>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? LogsId { get; set; }

        public virtual TableLog Logs { get; set; }
        public virtual ICollection<BookPublisher> BookPublishers { get; set; }
    }
}
