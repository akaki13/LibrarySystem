using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Configuration
{
    public class BookGenreConfiguration : EntityTypeConfiguration<BookGenre>
    {
        public BookGenreConfiguration() {
            ToTable("Book_Genre");

            Property(e => e.BookId).HasColumnName("Book_id");

            Property(e => e.GenreId).HasColumnName("Genre_id");
        }
    }
}
