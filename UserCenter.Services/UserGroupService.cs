using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCenter.DTO;
using UserCenter.IServices;
using UserCenter.Services.Entities;
using System.Data.Entity;

namespace UserCenter.Services
{
    public class UserGroupService : IUserGroupService
    {
        /// <summary>
        /// 将用户添加到组
        /// </summary>
        /// <param name="userGroupId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task AddUserToGroupAsync(long userGroupId, long userId)
        {
            using (UserCenterDbContext ctx = new UserCenterDbContext())
            {
                var group =await ctx.UserGroups.SingleOrDefaultAsync(g => g.Id == userGroupId);
                if (group == null)
                {
                    throw new AggregateException("userGroupId=" + userGroupId + "不存在");
                }
                var user =await ctx.Users.SingleOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                {
                    throw new AggregateException("userId=" + userId + "不存在");
                }
                group.Users.Add(user);
                user.Groups.Add(group);
                await ctx.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 根据组id获取组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserGroupDto> GetByIdAsync(long id)
        {
            using (UserCenterDbContext ctx = new UserCenterDbContext())
            {
                var group = await ctx.UserGroups.SingleOrDefaultAsync(g => g.Id == id);
                if (group == null)
                {
                    return null;
                }
                return ToUserGroupDto(group);
            }
        }
        /// <summary>
        /// 根据用户id获取组
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserGroupDto[]> GetGroupsAsync(long userId)
        {
            using (UserCenterDbContext ctx = new UserCenterDbContext())
            {
                var user = await ctx.Users.SingleOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                {
                    return null;
                }
                var groups = user.Groups;
                List<UserGroupDto> groupDtos = new List<UserGroupDto>();
                foreach (var item in groups)
                {
                    groupDtos.Add(ToUserGroupDto(item));
                }
                return groupDtos.ToArray();
            }
        }
        /// <summary>
        /// 获取某个组的所有用户
        /// </summary>
        /// <param name="userGroupId"></param>
        /// <returns></returns>
        public async Task<UserDto[]> GetGroupUsersAsync(long userGroupId)
        {
            using (UserCenterDbContext ctx = new UserCenterDbContext())
            {
                var group = await ctx.UserGroups.SingleOrDefaultAsync(g => g.Id == userGroupId);
                if (group == null)
                {
                    return null;
                }
                var users = group.Users;
                List<UserDto> userDtos = new List<UserDto>();
                foreach (var item in users)
                {
                    UserDto userDto = new UserDto();
                    userDto.Id = item.Id;
                    userDto.NickName = item.NickName;
                    userDto.PhoneNum = item.PhoneNum;
                    userDtos.Add(userDto);
                }
                return userDtos.ToArray();
            }
        }
        /// <summary>
        /// 删除组用户
        /// </summary>
        /// <param name="userGroupId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task RemoveUserFromGroupAsync(long userGroupId, long userId)
        {
            using (UserCenterDbContext ctx=new UserCenterDbContext ())
            {
                var group = await ctx.UserGroups.SingleOrDefaultAsync(g => g.Id == userGroupId);
                if (group == null)
                {
                    throw new AggregateException("userGroupId=" + userGroupId + "不存在");
                }
                var user = await ctx.Users.SingleOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                {
                    throw new AggregateException("userId=" + userId + "不存在");
                }
                group.Users.Remove(user);
                user.Groups.Remove(group);
                await ctx.SaveChangesAsync();
            }
        
        }
        private static UserGroupDto ToUserGroupDto(UserGroupEntity group)
        {
            UserGroupDto userGroupDto = new UserGroupDto();
            userGroupDto.Id = group.Id;
            userGroupDto.Name = group.Name;
            return userGroupDto;
        }
    }
}
