using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Configuration
{
    public class AuthorBookConfiguration : EntityTypeConfiguration<AuthorBook>
    {
        public AuthorBookConfiguration() {
            ToTable("Author_Book");

            Property(e => e.AutorId).HasColumnName("Autor_id");

            Property(e => e.BookId).HasColumnName("Book_id");
        }
    }
}
