namespace UserManagement.API.Responses
{
    /// <summary>
    /// The login response which the user will receive
    /// </summary>
    public class UserResponse
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string AccesToken { get; set; } = string.Empty;
    }
}
