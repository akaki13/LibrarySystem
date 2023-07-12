using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Configuration
{
    public class BookStorageConfiguration : EntityTypeConfiguration<BookStorage>
    {
        public BookStorageConfiguration() {
            ToTable("Book_Storage");

            Property(e => e.BookId).HasColumnName("Book_id");


            Property(e => e.StorageId).HasColumnName("Storage_id");
        }
    }
}
