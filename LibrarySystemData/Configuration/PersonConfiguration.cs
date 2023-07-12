using LibrarySystemModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace LibrarySystemData.Configuration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> entity)
        {
            entity.ToTable("Person");

            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("Date_ofBirth");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.EmailIsConfiormed).HasColumnName("Email_isConfiormed");

            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .IsUnicode(false);


            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}

