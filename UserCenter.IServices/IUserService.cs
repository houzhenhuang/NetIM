using System.Threading.Tasks;
using UserCenter.DTO;

namespace UserCenter.IServices
{
    public interface IUserService:IServiceTag
    {
        Task<long> AddNewAsync(string phoneNum, string nickName, string password); Task<bool> UserExistsAsync(string phoneNum);
        Task<bool> CheckLoginAsync(string phoneNum, string password);
        Task<UserDto> GetByIdAsync(long id);
        Task<UserDto> GetByPhoneNumAsync(string phoneNum);
    }
}
