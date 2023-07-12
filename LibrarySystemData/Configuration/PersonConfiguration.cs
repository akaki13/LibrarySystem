using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Configuration
{
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration() {
            ToTable("Person");

            Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);

            Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("Date_ofBirth");

            Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);

            Property(e => e.EmailIsConfiormed).HasColumnName("Email_isConfiormed");

            Property(e => e.Firstname)
                .HasMaxLength(50)
                .IsUnicode(false);

            Property(e => e.Lastname)
                .HasMaxLength(50)
                .IsUnicode(false);


            Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
