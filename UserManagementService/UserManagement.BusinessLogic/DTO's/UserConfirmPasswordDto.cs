namespace UserManagement.BusinessLogic.DTO_s
{
    public class UserConfirmPasswordDto
    {
        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The confirmation number of the password reset
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// The new password of the user
        /// </summary>
        public string NewPassword { get; set; } = string.Empty;
    }
}
