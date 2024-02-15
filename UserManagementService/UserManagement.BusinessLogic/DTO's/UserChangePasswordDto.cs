namespace UserManagement.BusinessLogic.DTO_s
{
    public class UserChangePasswordDto
    {
        /// <summary>
        /// The email of the user 
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The current passwor of the user
        /// </summary>
        public string CurrentPassword { get; set; } = string.Empty;

        /// <summary>
        /// The new password of the user
        /// </summary>
        public string NewPassword { get; set; } = string.Empty;
    }
}
