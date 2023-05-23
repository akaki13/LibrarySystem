using System;
using System.Collections.Generic;

namespace LibrarySystemModels
{
    public partial class BookGenre
    {
        public int Id { get; set; }
        public int? BookId { get; set; }
        public int? GenreId { get; set; }
        public int LogsId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual TableLog Logs { get; set; }
    }
}
