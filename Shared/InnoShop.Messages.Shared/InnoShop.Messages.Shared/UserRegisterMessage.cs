﻿namespace InnoShop.Messages.Shared
{
    public class UserRegisterMessage
    {
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

    }
}
