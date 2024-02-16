using FluentValidation;
using FluentValidation.AspNetCore;
using UserManagement.API.Validators;

namespace UserManagement.API.Extensions
{
    /// <summary>
    /// Configurations for the FluentValidation
    /// </summary>
    public static partial class ApplicationDependanciesConfiguration
    {
        /// <summary>
        /// configurations for fluent validation
        /// </summary>
        /// <param name="services"></param>
        public static void AddFluentValidationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<UserRegisterRequestValidator>()
                .AddValidatorsFromAssemblyContaining<UserLoginRequestValidator>()
                .AddFluentValidationAutoValidation();
        }
    }
}
