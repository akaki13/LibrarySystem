using LibrarySystemModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystemData.Configuration
{

    public class BorrowConfiguration : IEntityTypeConfiguration<Borrow>
    {
        public void Configure(EntityTypeBuilder<Borrow> entity)
        {
            entity.ToTable("Borrows");

            entity.Property(e => e.ActualReturnedTime)
                    .HasColumnType("date")
                    .HasColumnName("Actual_returned_time");

            entity.Property(e => e.BookId).HasColumnName("Book_id");


            entity.Property(e => e.PersonId).HasColumnName("Person_id");

            entity.Property(e => e.Comment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

            entity.Property(e => e.ReturnedTime)
                .HasColumnType("date")
                .HasColumnName("Returned_time");

            entity.Property(e => e.TakeTime)
                .HasColumnType("date")
                .HasColumnName("Take_time");

            entity.HasOne(d => d.Book)
                .WithMany(p => p.Borrows)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Borrows__Book_id__72C60C4A");



            entity.HasOne(d => d.Person)
                .WithMany(p => p.Borrows)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK__Borrows__Person___71D1E811");
        }
    }
}
