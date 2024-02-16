namespace UserManagement.API.Responses
{
    /// <summary>
    /// The register response which the user will receive
    /// </summary>
    public class UserRegisterResponse
    {
        /// <summary>
        /// The user's register response name
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The user's register response email
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }
}
