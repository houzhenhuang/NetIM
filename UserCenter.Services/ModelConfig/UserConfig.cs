using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCenter.Services.Entities;

namespace UserCenter.Services.ModelConfig
{
    public class UserConfig:EntityTypeConfiguration<UserEntity>
    {
        public UserConfig()
        {
            this.ToTable("T_Users");
            this.HasMany(u => u.Groups).WithMany(g => g.Users)
                .Map(m => m.ToTable("T_UserGroupRelation").MapLeftKey("UserId").MapRightKey("GroupId"));
            this.Property(u=>u.NickName).HasMaxLength(200).IsRequired();
            this.Property(u=>u.PasswordHash).HasMaxLength(100).IsRequired();
            this.Property(u=>u.PasswordSalt).HasMaxLength(20).IsRequired();
            this.Property(u=>u.PhoneNum).HasMaxLength(50).IsRequired();
        }
    }
}
