using FluentValidation;
using UserManagement.API.Requests;

namespace UserManagement.API.Validators
{
    /// <summary>
    /// Validator class for validating user Login requests.
    /// </summary>
    public class UserLoginRequestValidator:AbstractValidator<UserLoginRequest>
    {
        /// <summary>
        /// Initializes a new instance of the UserLogInRequestValidator
        /// class and defines validation rules for email and password.
        /// </summary>
        public UserLoginRequestValidator()
        {
            RuleFor(u => u.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress();

            RuleFor(u => u.Password)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8)
                    .WithMessage("Password must be at least 8 characters")
                .Matches("[A-Z]")
                    .WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]")
                    .WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]")
                    .WithMessage("Password must contain at least one digit");
        }
    }
}
