using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCenter.DTO;
using UserCenter.IServices;
using System.Data.Entity;

namespace UserCenter.Services
{
    public class AppInfoService : IAppInfoService
    {
        public async Task<AppInfoDto> GetByAppKeyAsync(string appKey)
        {
            using (UserCenterDbContext ctx=new UserCenterDbContext ())
            {
                var appInfo = await ctx.AppInfos.FirstOrDefaultAsync(a=>a.AppKey==appKey);
                if (appInfo==null)
                {
                    return null;
                }
                else
                {
                    AppInfoDto appInfoDto = new AppInfoDto();
                    appInfoDto.AppKey = appInfo.AppKey;
                    appInfoDto.AppSecret = appInfo.AppSecret;
                    appInfoDto.Name = appInfo.Name;
                    appInfoDto.IsEnabled = appInfo.IsEnabled;
                    appInfoDto.Id = appInfo.Id;
                    return appInfoDto;
                }
            }
        }
    }
}
