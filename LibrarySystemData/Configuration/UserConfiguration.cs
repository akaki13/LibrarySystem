using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Configuration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration() {
            ToTable("Users");

            Property(e => e.Login)
                            .HasMaxLength(50)
                            .IsUnicode(false);

            Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);

            Property(e => e.PersonId).HasColumnName("Person_id");
        }
    }
}
