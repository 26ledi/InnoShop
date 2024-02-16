using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserManagement.API.Requests;
using UserManagement.API.Responses;
using UserManagement.BusinessLogic.DTO_s;
using UserManagement.BusinessLogic.Services.Interfaces;

namespace UserManagement.API.Controllers
{
    [Route("auth")]
    [ApiController]
    public class UserAuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly IMapper _mapper;
        public UserAuthenticationController(IMapper mapper, IUserService userService, IUserAuthenticationService userAuthenticationService)
        {
            _mapper = mapper;
            _userService = userService;
            _userAuthenticationService = userAuthenticationService;
        }

        [HttpPost("registration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest userRegisterRequest)
        {
            var response = await _userService.AddAsync(_mapper.Map<UserDto>(userRegisterRequest));

            return Ok(_mapper.Map<UserRegisterResponse>(response));
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest userLoginRequest)
        {
            var response = await _userAuthenticationService.LoginAsync(_mapper.Map<UserLoginDto>(userLoginRequest));

            return Ok(_mapper.Map<UserResponse>(response));
        }
    }
}
