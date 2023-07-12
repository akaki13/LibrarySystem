using LibrarySystemModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace LibrarySystemData.Configuration
{
    public class BookPublisherConfiguration : IEntityTypeConfiguration<BookPublisher>
    {
        public void Configure(EntityTypeBuilder<BookPublisher> entity)
        {
            entity.ToTable("Book_Publisher");

            entity.Property(e => e.BookId).HasColumnName("Book_id");


            entity.Property(e => e.PublisherId).HasColumnName("Publisher_id");

            entity.HasOne(d => d.Book)
                .WithMany(p => p.BookPublishers)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Book_Publ__Book___5441852A");



            entity.HasOne(d => d.Publisher)
                .WithMany(p => p.BookPublishers)
                .HasForeignKey(d => d.PublisherId)
                .HasConstraintName("FK__Book_Publ__Publi__5535A963");
        }
    }
}
