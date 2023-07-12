using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Configuration
{
    public class BookPublisherConfiguration : EntityTypeConfiguration<BookPublisher>
    {
        public BookPublisherConfiguration() {
            ToTable("Book_Publisher");

            Property(e => e.BookId).HasColumnName("Book_id");


            Property(e => e.PublisherId).HasColumnName("Publisher_id");

        }
    }
}
