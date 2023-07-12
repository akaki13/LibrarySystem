using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Configuration
{
    public class StorageConfiguration : EntityTypeConfiguration<Storage>
    {
        public StorageConfiguration() {
            ToTable("Storage");

            Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);


            Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
