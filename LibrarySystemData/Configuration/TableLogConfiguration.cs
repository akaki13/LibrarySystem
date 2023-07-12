using LibrarySystemModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace LibrarySystemData.Configuration
{
    public class TableLogConfiguration : IEntityTypeConfiguration<TableLog>
    {
        public void Configure(EntityTypeBuilder<TableLog> entity)
        {
            entity.ToTable("TableLogs");

            entity.Property(e => e.ChangeDate)
                .HasColumnType("date")
                .HasColumnName("Change_date");

            entity.Property(e => e.CreateDate)
                .HasColumnType("date")
                .HasColumnName("Create_date");

            entity.Property(e => e.DeleteDate)
                .HasColumnType("date")
                .HasColumnName("Delete_date");

            entity.Property(e => e.TableName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Table_name");

            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.User)
                .WithMany(p => p.TableLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_LogsUsers");
        }
    }
}
