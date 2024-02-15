using System.ComponentModel.DataAnnotations;

namespace UserManagement.API.Requests
{
    /// <summary>
    /// The login request which the user will send
    /// </summary>
    public class UserRequest
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
