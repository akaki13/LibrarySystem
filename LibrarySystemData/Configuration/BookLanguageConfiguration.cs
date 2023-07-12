using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Configuration
{
    public class BookLanguageConfiguration : EntityTypeConfiguration<BookLanguage>
    {
        public BookLanguageConfiguration() {
            ToTable("Book_Languages");

            Property(e => e.BookId).HasColumnName("Book_id");

            Property(e => e.LanguagesId).HasColumnName("Languages_id");


        }
    }
}
