using System;
using System.Collections.Generic;

namespace LibrarySystemModels
{
    public partial class BookStorage
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int StorageId { get; set; }
        public int? NumberOfBook { get; set; }

        public virtual Book Book { get; set; }
        public virtual Storage Storage { get; set; }
    }
}
