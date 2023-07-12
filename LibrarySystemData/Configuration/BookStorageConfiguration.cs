using LibrarySystemModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace LibrarySystemData.Configuration
{
    public class BookStorageConfiguration : IEntityTypeConfiguration<BookStorage>
    {
        public void Configure(EntityTypeBuilder<BookStorage> entity)
        {
            entity.ToTable("Book_Storage");

            entity.Property(e => e.BookId).HasColumnName("Book_id");


            entity.Property(e => e.StorageId).HasColumnName("Storage_id");

            entity.HasOne(d => d.Book)
                .WithMany(p => p.BookStorages)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Book_Stor__Book___4F7CD00D");



            entity.HasOne(d => d.Storage)
                .WithMany(p => p.BookStorages)
                .HasForeignKey(d => d.StorageId)
                .HasConstraintName("FK__Book_Stor__Stora__5070F446");
        }
    }
}
