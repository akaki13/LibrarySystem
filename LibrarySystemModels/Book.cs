using System;
using System.Collections.Generic;

namespace LibrarySystemModels
{
    public partial class Book
    {
        public Book()
        {
            AuthorBooks = new HashSet<AuthorBook>();
            BookGenres = new HashSet<BookGenre>();
            BookLanguages = new HashSet<BookLanguage>();
            BookPublishers = new HashSet<BookPublisher>();
            BookStorages = new HashSet<BookStorage>();
            Borrows = new HashSet<Borrow>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }
        public virtual ICollection<BookGenre> BookGenres { get; set; }
        public virtual ICollection<BookLanguage> BookLanguages { get; set; }
        public virtual ICollection<BookPublisher> BookPublishers { get; set; }
        public virtual ICollection<BookStorage> BookStorages { get; set; }
        public virtual ICollection<Borrow> Borrows { get; set; }
    }
}
