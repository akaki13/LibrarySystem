using System;
using System.Collections.Generic;

namespace LibrarySystemModels
{
    public partial class BookLanguage
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int LanguagesId { get; set; }
        public int? NumberOfBook { get; set; }

        public virtual Book Book { get; set; }
        public virtual Language Languages { get; set; }
    }
}
