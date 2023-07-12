using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Configuration
{
    public class PersonPositionConfiguration : EntityTypeConfiguration<PersonPosition>
    {
        public PersonPositionConfiguration() {
            ToTable("Person_Position");


            Property(e => e.PersonId).HasColumnName("Person_id");

            Property(e => e.PositionId).HasColumnName("Position_id");


        }
    }
}
