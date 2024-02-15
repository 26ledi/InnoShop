using System.ComponentModel.DataAnnotations;

namespace UserManagement.API.Responses
{
    /// <summary>
    /// The register response which the user will receive
    /// </summary>
    public class UserRegisterResponse
    {
        public string Name { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
