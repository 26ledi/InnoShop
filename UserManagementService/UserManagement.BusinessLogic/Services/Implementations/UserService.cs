using AutoMapper;
using InnoShop.Exceptions.Shared.Exceptions;
using InnoShop.Messages.Shared;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UserManagement.BusinessLogic.DTO_s;
using UserManagement.BusinessLogic.Helpers;
using UserManagement.BusinessLogic.Services.Interfaces;
using UserManagement.DataAccess.Entities;

namespace UserManagement.BusinessLogic.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<UserService> _logger;
        /// <summary>
        /// Initializes an new instance cref of< see cref="UserService"/>
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="publishEndpoint"></param>
        public UserService(UserManager<User> userManager, IMapper mapper, IPublishEndpoint publishEndpoint, ILogger<UserService> logger)
        {
            _userManager = userManager;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }
        /// <summary>
        /// Function for adding user
        /// </summary>
        /// <param name="userDto">The user details.</param>
        /// <returns></returns>
        /// <exception cref="AlreadyExistsException"></exception>
        public async Task<UserDto> AddAsync(UserDto userDto)
        {

            if (await _userManager.IsUserEmailExist(userDto.Email))
            {
                _logger.LogError("This user with {email} already exist", userDto.Email);
                throw new AlreadyExistException("This user already exist");
            }
            var user = _mapper.Map<User>(userDto);
            user.Id = Guid.NewGuid().ToString();
            var result = await _userManager.CreateAsync(user, userDto.Password);
            result.ThrowExceptionIfResultDoNotSucceed(_logger);
            await _userManager.AddRoleToUserAsync("User", user, _logger);
            await _publishEndpoint.Publish(_mapper.Map<UserRegisterMessage>(userDto));

            return _mapper.Map<UserDto>(user);
        }
        /// <summary>
        /// Function for deleting user
        /// </summary>
        /// <param name="email">The deleted user's email.</param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<UserDto> DeleteAsync(string email)
        {
            var userLooked = await _userManager.FindByEmailAsync(email);

            if (!await _userManager.IsUserEmailExist(email))
            {
                _logger.LogError("This user with {email} does not exist", email);
                throw new NotFoundException("This user does not exist");
            }
            var user = _mapper.Map<User>(userLooked);
            var result = await _userManager.DeleteAsync(user);
            result.ThrowExceptionIfResultDoNotSucceed(_logger);

            return _mapper.Map<UserDto>(user);
        }
        /// <summary>
        /// Function for getting user by Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (!await _userManager.IsUserEmailExist(email))
            {
                _logger.LogError("This {email} does not exist", email);
                throw new NotFoundException($"This {email} does not exist");
            }

            return _mapper.Map<UserDto>(user);
        }
        /// <summary>
        /// Function for updating user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<UserDto> UpdateAsync(UserDto userDto)
        {
            var user = await _userManager.FindByEmailAsync(userDto.Email);

            if (!await _userManager.IsUserEmailExist(userDto.Email))
            {
                throw new NotFoundException("This user does not exist");
            }
            _mapper.Map(userDto, user);
            var result = await _userManager.UpdateAsync(user);
            result.ThrowExceptionIfResultDoNotSucceed(_logger);
            return _mapper.Map<UserDto>(user);
        }
        /// <summary>
        /// Function for editing user's password 
        /// </summary>
        /// <param name="email">The user's email for whom the password will be changed.</param>
        /// <param name="currentPassword">The user's current password.</param>
        /// <param name="newPassword">The new password for the user.</param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<UserDto> ChangePasswordAsync(UserChangePasswordDto userChangePasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(userChangePasswordDto.Email);

            if (!await _userManager.IsUserEmailExist(userChangePasswordDto.Email))
            {
                _logger.LogError("This user with {email} does not exist", userChangePasswordDto.Email);

                throw new NotFoundException("This user does not exist");
            }
            var result = await _userManager.ChangePasswordAsync(user, userChangePasswordDto.CurrentPassword, userChangePasswordDto.NewPassword);
            result.ThrowExceptionIfResultDoNotSucceed(_logger);

            return _mapper.Map<UserDto>(user);
        }
        /// <summary>
        /// Function for sending a  reset password by token
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<UserDto> SendResetPasswordTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (!await _userManager.IsUserEmailExist(email))
            {
                _logger.LogError("This user with {email} does not exist", email);

                throw new NotFoundException("This user does not exist");
            }
            var confirmationCode = await _userManager.GeneratePasswordResetTokenAsync(user);
            var userResetPasswordMessage = _mapper.Map<UserResetPasswordMessage>(user);
            userResetPasswordMessage.ConfirmationCode = confirmationCode;
            await _publishEndpoint.Publish(userResetPasswordMessage);

            return _mapper.Map<UserDto>(user);
        }
        /// <summary>
        ///  Function for validating the reset password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<UserDto> ConfirmPasswordAsync(UserConfirmPasswordDto userConfirmPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(userConfirmPasswordDto.Email);

            if (!await _userManager.IsUserEmailExist(userConfirmPasswordDto.Email))
            {
                _logger.LogError("This user with {email} does not exist", userConfirmPasswordDto.Email);

                throw new NotFoundException("This user does not exist");
            }
            var result = await _userManager.ResetPasswordAsync(user, userConfirmPasswordDto.Token, userConfirmPasswordDto.NewPassword);

            return _mapper.Map<UserDto>(result);
        }
        /// <summary>
        /// Function for validating the Email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<UserDto> ConfirmEmailAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (!await _userManager.IsUserEmailExist(email))
            {
                _logger.LogError("This user with {email} does not exist", email);

                throw new NotFoundException("This user does not exist");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            return _mapper.Map<UserDto>(result);
        }

    }
}
