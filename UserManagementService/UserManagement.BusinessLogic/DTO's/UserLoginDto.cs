namespace UserManagement.BusinessLogic.DTO_s
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for a user's login.
    /// </summary>
    public class UserLoginDto
    {
        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the password
        /// </summary>
        public string Password { get; set; }
    }
}
