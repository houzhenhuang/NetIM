using System.Threading.Tasks;
using UserCenter.DTO;

namespace UserCenter.IServices
{
    public interface IUserGroupService:IServiceTag
    {
        /// <summary>
        /// 根据id获取用户组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserGroupDto> GetByIdAsync(long id);
        /// <summary>
        /// 根据用户Id获取所在组
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserGroupDto[]> GetGroupsAsync(long userId);
        Task<UserDto[]> GetGroupUsersAsync(long userGroupId);
        Task AddUserToGroupAsync(long userGroupId, long userId);
        Task RemoveUserFromGroupAsync(long userGroupId, long userId);
    }
}
