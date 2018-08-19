using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCenter.DTO;
using UserCenter.IServices;
using UserCenter.Services.Entities;
using System.Data.Entity;
using UserCenter.Common;

namespace UserCenter.Services
{
    public class UserService : IUserService
    {
        public async Task<long> AddNewAsync(string phoneNum, string nickName, string password)
        {
            using (UserCenterDbContext ctx = new UserCenterDbContext())
            {
                if (await ctx.Users.AnyAsync(u => u.PhoneNum == phoneNum))
                {
                    throw new ApplicationException("手机号：" + phoneNum + "已经存在");
                }
                UserEntity userEntity = new UserEntity();
                userEntity.NickName = nickName;
                userEntity.PhoneNum = phoneNum;
                string salt = new Random().Next(10000, 99999).ToString();
                string hash = MD5Helper.CalcMD5(password + salt);
                userEntity.PasswordSalt = salt;
                userEntity.PasswordHash = hash;
                ctx.Users.Add(userEntity);
                await ctx.SaveChangesAsync();
                return userEntity.Id;
            }
        }

        public async Task<bool> CheckLoginAsync(string phoneNum, string password)
        {
            using (UserCenterDbContext ctx = new UserCenterDbContext())
            {
                var user = await ctx.Users.SingleOrDefaultAsync(u => u.PhoneNum == phoneNum);
                if (user == null)
                {
                    return false;
                }
                string inputHash = MD5Helper.CalcMD5(password + user.PasswordSalt);
                return user.PasswordHash == inputHash;
            }
        }

        public async Task<UserDto> GetByIdAsync(long id)
        {
            using (UserCenterDbContext ctx = new UserCenterDbContext())
            {
                var user = await ctx.Users.SingleOrDefaultAsync(u => u.Id == id);
                if (user == null)
                {
                    return null;
                }
                return ToUserDto(user);
            }
        }

        public async Task<UserDto> GetByPhoneNumAsync(string phoneNum)
        {
            using (UserCenterDbContext ctx = new UserCenterDbContext())
            {
                var user = await ctx.Users.SingleOrDefaultAsync(u => u.PhoneNum == phoneNum);
                if (user == null)
                {
                    return null;
                }
                return ToUserDto(user);
            }
        }

        public async Task<bool> UserExistsAsync(string phoneNum)
        {
            using (UserCenterDbContext ctx = new UserCenterDbContext())
            {
                return await ctx.Users.AnyAsync(u => u.PhoneNum == phoneNum);
            }
        }
        private static UserDto ToUserDto(UserEntity user)
        {
            UserDto userDto = new UserDto();
            userDto.NickName = user.NickName;
            userDto.Id = user.Id;
            userDto.PhoneNum = user.PhoneNum;
            return userDto;
        }
    }
}
