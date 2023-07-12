using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Configuration
{
    public class PublisherConfiguration : EntityTypeConfiguration<Publisher>
    {
        public PublisherConfiguration() {
            ToTable("Publisher");

            Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);


            Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}
