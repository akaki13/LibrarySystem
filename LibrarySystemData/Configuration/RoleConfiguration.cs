using LibrarySystemModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystemData.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entity)
        {
            entity.ToTable("Role");


            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);

        }
    }
}
