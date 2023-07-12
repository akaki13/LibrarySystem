using LibrarySystemModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace LibrarySystemData.Configuration
{
    public class BookLanguageConfiguration : IEntityTypeConfiguration<BookLanguage>
    {
        public void Configure(EntityTypeBuilder<BookLanguage> entity)
        {
            entity.ToTable("Book_Languages");

            entity.Property(e => e.BookId).HasColumnName("Book_id");

            entity.Property(e => e.LanguagesId).HasColumnName("Languages_id");


            entity.HasOne(d => d.Book)
                .WithMany(p => p.BookLanguages)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Book_Lang__Book___19DFD96B");

            entity.HasOne(d => d.Languages)
                .WithMany(p => p.BookLanguages)
                .HasForeignKey(d => d.LanguagesId)
                .HasConstraintName("FK__Book_Lang__Langu__1AD3FDA4");
        }
    }
}
