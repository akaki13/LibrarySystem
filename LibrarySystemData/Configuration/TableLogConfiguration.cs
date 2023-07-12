using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Configuration
{
    public class TableLogConfiguration : EntityTypeConfiguration<TableLog>
    {
        public TableLogConfiguration() {
            ToTable("TableLogs");

            Property(e => e.ChangeDate)
                             .HasColumnType("date")
                             .HasColumnName("Change_date");

            Property(e => e.CreateDate)
                .HasColumnType("date")
                .HasColumnName("Create_date");

            Property(e => e.DeleteDate)
                .HasColumnType("date")
                .HasColumnName("Delete_date");

            Property(e => e.TableName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Table_name");

            Property(e => e.UserId).HasColumnName("User_id");

            Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
        }
    }
}
