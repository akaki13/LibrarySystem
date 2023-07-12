using LibrarySystemModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace LibrarySystemData.Configuration
{
    public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> entity)
        {
            entity.ToTable("Publisher");

            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}
