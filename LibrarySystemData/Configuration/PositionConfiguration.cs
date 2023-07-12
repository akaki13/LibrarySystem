using LibrarySystemModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace LibrarySystemData.Configuration
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> entity)
        {
            entity.ToTable("Position");

            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .IsUnicode(false);
            
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}
