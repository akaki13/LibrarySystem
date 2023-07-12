using LibrarySystemModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace LibrarySystemData.Configuration
{
    public class StorageConfiguration : IEntityTypeConfiguration<Storage>
    {
        public void Configure(EntityTypeBuilder<Storage> entity)
        {
            entity.ToTable("Storage");

            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);


            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
