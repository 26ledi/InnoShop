namespace InnoShop.Messages.Shared
{
    public class UserResetPasswordMessage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
        public string ConfirmationCode { get; set; }
    }
}
