using LibrarySystemModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace LibrarySystemData.Configuration
{
    public class BookGenreConfiguration : IEntityTypeConfiguration<BookGenre>
    {
        public void Configure(EntityTypeBuilder<BookGenre> entity)
        {
            entity.ToTable("Book_Genre");

            entity.Property(e => e.BookId).HasColumnName("Book_id");

            entity.Property(e => e.GenreId).HasColumnName("Genre_id");


            entity.HasOne(d => d.Book)
                .WithMany(p => p.BookGenres)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Book_Genr__Book___59063A47");

            entity.HasOne(d => d.Genre)
                .WithMany(p => p.BookGenres)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK__Book_Genr__Genre__59FA5E80");

        }
    }
}
