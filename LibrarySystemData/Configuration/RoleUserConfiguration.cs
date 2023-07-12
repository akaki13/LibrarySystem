using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemData.Configuration
{
    public class RoleUserConfiguration : EntityTypeConfiguration<RoleUser>
    {
        public RoleUserConfiguration() {
            ToTable("Role_Users");


            Property(e => e.RoleId).HasColumnName("Role_id");

            Property(e => e.UsersId).HasColumnName("Users_id");


        }
    }
}
