using FluentValidation;
using UserManagement.API.Requests;

namespace UserManagement.API.Validators
{
    /// <summary>
    /// Validator class for validating user Registration requests.
    /// </summary>
    public class UserRegisterRequestValidator:AbstractValidator<UserRegisterRequest>
    {
        /// <summary>
        /// Initializes a new instance of the UserRegistrationRequestValidator
        /// class and defines validation rules for UserName, password and Email.
        /// </summary>
        public UserRegisterRequestValidator() 
        {
            RuleFor(u => u.UserName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty();

            RuleFor(u => u.Password)
           .Cascade(CascadeMode.Stop)
           .NotNull()
           .NotEmpty().WithMessage("Password is required.")
           .MinimumLength(8)
               .WithMessage("Password must be at least 8 characters")
           .Matches("[A-Z]")
               .WithMessage("Password must contain at least one uppercase letter")
           .Matches("[a-z]")
               .WithMessage("Password must contain at least one lowercase letter")
           .Matches("[0-9]")
               .WithMessage("Password must contain at least one digit");

            RuleFor(u => u.ConfirmPassword)
                .Cascade(CascadeMode.Stop)
                .Equal(u => u.Password)
                .WithMessage("Password confirmed must be equal to the password");

            RuleFor(u => u.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress();

        }
    }
}
