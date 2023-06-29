using System;
using System.Collections.Generic;

namespace LibrarySystemModels
{
    public partial class Language
    {
        public Language()
        {
            BookLanguages = new HashSet<BookLanguage>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<BookLanguage> BookLanguages { get; set; }
    }
}
