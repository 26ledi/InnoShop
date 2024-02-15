using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagement.BusinessLogic.DTO_s;
using UserManagement.BusinessLogic.Services.Interfaces;
using UserManagement.DataAccess.Entities;

namespace UserManagement.BusinessLogic.Services.Implementations
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UserAuthenticationService> _logger;
        private readonly IConfiguration _configuration;
        public UserAuthenticationService(UserManager<User> userManager, ILogger<UserAuthenticationService> logger, IConfiguration configuration)
        {
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<TokenDto> LoginAsync(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDto.Email);

            if (!await _userManager.CheckPasswordAsync(user, userLoginDto.Password))
            {
                _logger.LogError("There is no user with that email");
                throw new Exception("Wrong email, please try again or sign up");
            }
            
            return await GenerateTokenAsync(user);
        }
        private async Task<TokenDto> GenerateTokenAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.First();
            var claimsIdentity = new ClaimsIdentity(new Claim[] {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, role)
        });

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"])), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenDto = new TokenDto
            {
                Email = user.Email,
                Role = role,
                AccesToken = tokenHandler.WriteToken(token)
            };

            return tokenDto;
        }
    }
}
