using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Configuration
{
    public class AuthorConfiguration : EntityTypeConfiguration<Author>
    {
        public AuthorConfiguration() {
            ToTable("Author");

            Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
