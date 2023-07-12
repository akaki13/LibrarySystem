using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Configuration
{
    public class BorrowConfiguration : EntityTypeConfiguration<Borrow>
    {
        public BorrowConfiguration() {
            ToTable("Borrows");

            Property(e => e.ActualReturnedTime)
                             .HasColumnType("date")
                             .HasColumnName("Actual_returned_time");

            Property(e => e.BookId).HasColumnName("Book_id");


            Property(e => e.PersonId).HasColumnName("Person_id");

            Property(e => e.ReturnedTime)
                .HasColumnType("date")
                .HasColumnName("Returned_time");

            Property(e => e.TakeTime)
                .HasColumnType("date")
                .HasColumnName("Take_time");
        }
    }
}
