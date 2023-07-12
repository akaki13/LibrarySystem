using LibrarySystemModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace LibrarySystemData.Configuration
{
    public class SliderConfiguration : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> entity)
        {
            entity.ToTable("Slider");

            entity.Property(e => e.Path)
                    .HasMaxLength(200)
                    .IsUnicode(false);

            entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);

            entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

        }
    }
}
