namespace UserManagement.BusinessLogic.DTO_s
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for a user.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Gets or sets the user's identifier.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        /// 
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the email of the user
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the password of the user
        /// </summary>
        public string Password { get; set; }
    }
}
