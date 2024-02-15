using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagement.API.Requests;
using UserManagement.API.Responses;
using UserManagement.BusinessLogic.DTO_s;
using UserManagement.BusinessLogic.Services.Interfaces;
using UserManagement.DataAccess.Entities;

namespace UserManagement.API.Controllers
{
    [ApiController]
    [Authorize(Roles = ("Admin, User"))]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(UserManager<User> userManager, IMapper mapper, IUserService userService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateAsync([FromBody] UserRegisterRequest userRegisterRequest)
        {
            var user = await _userManager.FindByEmailAsync(userRegisterRequest.Email);
            _mapper.Map(userRegisterRequest, user);
            var response = await _userService.UpdateAsync(_mapper.Map<UserDto>(user));

            return Ok(_mapper.Map<UserResponse>(response));
        }
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var response = await _userService.DeleteAsync(user.Email);

            return Ok(_mapper.Map<UserResponse>(response));
        }

        [HttpPost("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] UserChangePasswordRequest userChangePasswordRequest)
        {
            var user = await _userService.ChangePasswordAsync(_mapper.Map<UserChangePasswordDto>(userChangePasswordRequest));
            var response = _mapper.Map<UserChangePasswordResponse>(user);

            return Ok(_mapper.Map<UserResponse>(response));
        }

        [HttpGet("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendResetPasswordTokenAsync(string email)
        {
            var user = await _userService.SendResetPasswordTokenAsync(email);

            return Ok(_mapper.Map<UserResponse>(user));
        }

        [HttpPost("password-confirmation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConfirmPasswordAsync([FromBody] UserConfirmPasswordRequest userConfirmPasswordRequest)
        {
            var user = await _userService.ConfirmPasswordAsync(_mapper.Map<UserConfirmPasswordDto>(userConfirmPasswordRequest));

            return Ok(_mapper.Map<UserResponse>(user));
        }

        [HttpPost("email-confirmation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string email, [FromQuery] string token)
        {
            var user = await _userService.ConfirmEmailAsync(email, token);

            return Ok(_mapper.Map<UserResponse>(user));
        }
    }
}
