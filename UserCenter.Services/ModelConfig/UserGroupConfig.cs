using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCenter.Services.Entities;

namespace UserCenter.Services.ModelConfig
{
    public class UserGroupConfig:EntityTypeConfiguration<UserGroupEntity>
    {
        public UserGroupConfig()
        {
            this.ToTable("T_UserGroups");
            this.Property(g => g.Name).HasMaxLength(200).IsRequired();
        }
    }
}
