using BVBM.API.Dto;

namespace BVBM.API.Interface
{
    public interface IAuthService
    {
        Task<bool> Login(UserDto userDto);
        Task<string> GenerateTokenString(UserDto userDto);
        Task<bool> IsLoggedIn();
    }
}
