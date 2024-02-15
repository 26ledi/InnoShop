using System.ComponentModel.DataAnnotations;

namespace UserManagement.API.Requests
{
    public class UserRegisterRequest
    {
        /// <summary>
        /// The register request which the user will send
        /// </summary>
        [Required(ErrorMessage = "The user's name field is required")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
