using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Configuration
{
    public class SliderConfiguration : EntityTypeConfiguration<Slider>
    {
        public SliderConfiguration() {
            ToTable("Slider");

            Property(e => e.Path)
                    .HasMaxLength(500)
                    .IsUnicode(false);

            Property(e => e.Title)
                    .HasMaxLength(200)
                    .IsUnicode(false);

            Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);
        }
    }
}
