using LibrarySystemModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystemData.Configuration
{
    public class AuthorBookConfiguration : IEntityTypeConfiguration<AuthorBook>
    {
        public void Configure(EntityTypeBuilder<AuthorBook> entity)
        {
            entity.ToTable("Author_Book");

            entity.Property(e => e.AutorId).HasColumnName("Autor_id");

            entity.Property(e => e.BookId).HasColumnName("Book_id");


            entity.HasOne(d => d.Autor)
                .WithMany(p => p.AuthorBooks)
                .HasForeignKey(d => d.AutorId)
                .HasConstraintName("FK__Author_Bo__Autor__5EBF139D");

            entity.HasOne(d => d.Book)
                .WithMany(p => p.AuthorBooks)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Author_Bo__Book___5DCAEF64");

        }
    }
}
