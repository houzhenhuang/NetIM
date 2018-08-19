using System.Data.Entity.ModelConfiguration;
using UserCenter.Services.Entities;

namespace UserCenter.Services.ModelConfig
{
    public class AppInfoConfig: EntityTypeConfiguration<AppInfoEntity>
    {
        public AppInfoConfig()
        {
            this.ToTable("T_AppInfos");
            this.Property(e => e.Name).HasMaxLength(100).IsRequired();
            this.Property(e => e.AppKey).HasMaxLength(100).IsRequired();
            this.Property(e => e.AppSecret).HasMaxLength(100).IsRequired();
            this.Property(e => e.IsEnabled).IsRequired();

        }
    }
}
