using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Configuration
{
    public class PositionConfiguration : EntityTypeConfiguration<Position>
    {
        public PositionConfiguration() {
            ToTable("Position");

            Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);



            Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
