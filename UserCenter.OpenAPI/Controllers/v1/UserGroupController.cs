using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UserCenter.DTO;
using UserCenter.IServices;

namespace UserCenter.OpenAPI.Controllers.v1
{
    public class UserGroupController : ApiController
    {
        public IUserGroupService UserGroupService { get; set; }

        [HttpGet]
        public async Task<UserGroupDto> GetById(long id)
        {
            return await UserGroupService.GetByIdAsync(id);
        }

        [HttpGet]
        public async Task<UserGroupDto[]> GetGroups(long userId)
        {
            return await UserGroupService.GetGroupsAsync(userId);
        }
        [HttpGet]
        public async Task<UserDto[]> GetGroupUsers(long userGroupId)
        {
            return await UserGroupService.GetGroupUsersAsync(userGroupId);
        }
        [HttpGet]
        public async Task AddUserToGroup(long userGroupId,long userId)
        {
             await UserGroupService.AddUserToGroupAsync(userGroupId,userId);
        }
        [HttpGet]
        public async Task RemoveUserFromGroup(long userGroupId, long userId)
        {
            await UserGroupService.RemoveUserFromGroupAsync(userGroupId, userId);
        }

    }
}
