using LibrarySystemModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace LibrarySystemData.Configuration
{
    public class PersonPositionConfiguration : IEntityTypeConfiguration<PersonPosition>
    {
        public void Configure(EntityTypeBuilder<PersonPosition> entity)
        {

            entity.ToTable("Person_Position");

            entity.Property(e => e.PersonId).HasColumnName("Person_id");

            entity.Property(e => e.PositionId).HasColumnName("Position_id");

            entity.HasOne(d => d.Person)
                .WithMany(p => p.PersonPositions)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK__Person_Po__Perso__6E01572D");

            entity.HasOne(d => d.Position)
                .WithMany(p => p.PersonPositions)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK__Person_Po__Posit__6D0D32F4");
        }
    }
}

