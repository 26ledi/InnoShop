namespace UserManagement.API.Responses
{
    /// <summary>
    /// The login response which the user will receive
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        /// User's email
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// User's Password
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// User's Role
        /// </summary>
        public string Role { get; set; } = string.Empty;
        /// <summary>
        /// User's Token
        /// </summary>
        public string AccesToken { get; set; } = string.Empty;
    }
}
