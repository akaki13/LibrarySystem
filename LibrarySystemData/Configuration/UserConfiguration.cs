using LibrarySystemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySystemData.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("Users");

            entity.Property(e => e.Login)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.PersonId)
                .HasColumnName("Person_id");


            entity.HasOne(d => d.Person)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK__Users__Person_id__3D5E1FD2");
        }
    }
}
