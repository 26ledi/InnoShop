using UserManagement.BusinessLogic.DTO_s;

namespace UserManagement.BusinessLogic.Services.Interfaces
{
    public interface IUserAuthenticationService
    {
        /// <summary>
        /// Method for signing up
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns></returns>
        Task<TokenDto> LoginAsync(UserLoginDto userLoginDto);
    }
}
