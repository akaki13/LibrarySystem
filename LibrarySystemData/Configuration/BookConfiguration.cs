using LibrarySystemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace LibrarySystemData.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> entity)
        {
            entity.ToTable("Book");

            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .IsUnicode(false);


            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.ImagePath)
               .HasMaxLength(300)
               .IsUnicode(false);
        }
    }
}
