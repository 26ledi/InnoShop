using UserManagement.BusinessLogic.DTO_s;

namespace UserManagement.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Adding user.
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        Task<UserDto> AddAsync(UserDto userDto);
        /// <summary>
        /// Updates the details of an existing user.
        /// </summary>
        /// <param name="userDto">The updated user details.</param>
        /// <returns></returns>
        Task<UserDto> UpdateAsync(UserDto userDto);
        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="email">The user's email which will be deleted.</param>
        /// <returns></returns>
        Task<UserDto> DeleteAsync(string email);
        /// <summary>
        /// Changes the password of a user.
        /// </summary>
        /// <param name="userChangePasswordDto">.</param>
        /// <returns></returns>
        Task<UserDto> ChangePasswordAsync(UserChangePasswordDto userChangePasswordDto);

        /// <summary>
        /// Function for sending a reset password by token
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<UserDto> SendResetPasswordTokenAsync(string email);
        /// <summary>
        /// Function of validating a reset password
        /// </summary>
        /// <param name="userConfirmPasswordDto"></param>
        /// <returns></returns>
        Task<UserDto> ConfirmPasswordAsync(UserConfirmPasswordDto userConfirmPasswordDto);
        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <returns></returns>
        Task<UserDto> GetByEmailAsync(string email);
        /// <summary>
        /// Function for validating the Email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        Task<UserDto> ConfirmEmailAsync(string email, string token);
    }
}
