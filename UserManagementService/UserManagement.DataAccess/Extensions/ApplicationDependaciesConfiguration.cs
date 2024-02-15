using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.DataAccess.Data;
using UserManagement.DataAccess.Entities;

namespace UserManagement.DataAccess.Extensions
{
    /// <summary>
    /// The Application configuration to configure the service of the database comming from the Data access
    /// </summary>
    public static class ApplicationDependaciesConfiguration
    {
        /// <summary>
        /// Add the identity service database and all the dependencies
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="options">The options builder to be passed to the function</param>
        /// <returns>A <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddIdentityDatabase(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContextPool<UserContext>(options)
           .AddIdentity<User, IdentityRole>(optionsIdentity =>
           {
               optionsIdentity.User.RequireUniqueEmail = true;
               optionsIdentity.Password.RequireNonAlphanumeric = true;
               optionsIdentity.Password.RequireLowercase = false;
               optionsIdentity.Password.RequireUppercase = true;
               optionsIdentity.Password.RequireDigit = true;
               optionsIdentity.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
               optionsIdentity.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
           })
           .AddEntityFrameworkStores<UserContext>()
           .AddDefaultTokenProviders();
            return services;
        }
    }
}
