namespace UserManagement.BusinessLogic.DTO_s
{
    /// <summary>
    /// Represent the Data Transfer Object of the token
    /// </summary>
    public class TokenDto
    {
        /// <summary>
        /// Gets or sets the role of the user
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// Gets or sets the email of the user
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the Token
        /// </summary>
        public string AccesToken { get; set; }
    }
}
