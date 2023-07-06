using System;
using System.Collections.Generic;

namespace LibrarySystemModels
{
    public partial class BookPublisher
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int PublisherId { get; set; }
        public int? NumberOfBook { get; set; }

        public virtual Book Book { get; set; }
        public virtual Publisher Publisher { get; set; }
    }
}
