using System;
using System.Collections.Generic;

namespace LibrarySystemModels
{
    public partial class TableLog
    {
        public TableLog()
        {
            AuthorBooks = new HashSet<AuthorBook>();
            Authors = new HashSet<Author>();
            BookGenres = new HashSet<BookGenre>();
            BookPublishers = new HashSet<BookPublisher>();
            BookStorages = new HashSet<BookStorage>();
            Books = new HashSet<Book>();
            Borrows = new HashSet<Borrow>();
            Genres = new HashSet<Genre>();
            People = new HashSet<Person>();
            PersonPositions = new HashSet<PersonPosition>();
            Positions = new HashSet<Position>();
            Publishers = new HashSet<Publisher>();
            RoleUsers = new HashSet<RoleUser>();
            Roles = new HashSet<Role>();
            Storages = new HashSet<Storage>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string TableName { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<BookGenre> BookGenres { get; set; }
        public virtual ICollection<BookPublisher> BookPublishers { get; set; }
        public virtual ICollection<BookStorage> BookStorages { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Borrow> Borrows { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Person> People { get; set; }
        public virtual ICollection<PersonPosition> PersonPositions { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
        public virtual ICollection<Publisher> Publishers { get; set; }
        public virtual ICollection<RoleUser> RoleUsers { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Storage> Storages { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
