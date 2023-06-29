using System;
using System.Collections.Generic;

namespace LibrarySystemModels
{
    public partial class Genre
    {
        public Genre()
        {
            BookGenres = new HashSet<BookGenre>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BookGenre> BookGenres { get; set; }
    }
}
