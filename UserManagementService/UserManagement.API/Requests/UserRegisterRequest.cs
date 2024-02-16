namespace UserManagement.API.Requests
{
    /// <summary>
    /// The register request which the user will send
    /// </summary>
    public class UserRegisterRequest
    {
        /// <summary>
        /// The user's register name
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The user's register email
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// The user's register Username
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// The user's register password 
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// The user's register password confirmation
        /// </summary>
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
