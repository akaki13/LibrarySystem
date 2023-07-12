using LibrarySystemModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace LibrarySystemData.Configuration
{
    public class RoleUserConfiguration : IEntityTypeConfiguration<RoleUser>
    {
        public void Configure(EntityTypeBuilder<RoleUser> entity)
        {
            entity.ToTable("Role_Users");


            entity.Property(e => e.RoleId).HasColumnName("Role_id");

            entity.Property(e => e.UsersId).HasColumnName("Users_id");

            entity.HasOne(d => d.Role)
                .WithMany(p => p.RoleUsers)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Role_User__Role___68487DD7");

            entity.HasOne(d => d.Users)
                .WithMany(p => p.RoleUsers)
                .HasForeignKey(d => d.UsersId)
                .HasConstraintName("FK__Role_User__Users__693CA210");
        }
    }
}
