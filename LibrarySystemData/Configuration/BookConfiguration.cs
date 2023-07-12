using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Configuration
{
    public class BookConfiguration : EntityTypeConfiguration<Book>
    {
        public BookConfiguration() {
            ToTable("Book");

            Property(e => e.Description)
                .HasMaxLength(150)
                .IsUnicode(false);


            Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

        }
    }
}
