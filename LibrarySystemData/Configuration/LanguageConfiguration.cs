using LibrarySystemModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace LibrarySystemData.Configuration
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> entity)
        {
            entity.ToTable("Languages");

            entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);
        }
    }
}

