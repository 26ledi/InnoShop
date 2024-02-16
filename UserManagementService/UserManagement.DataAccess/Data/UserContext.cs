using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagement.DataAccess.Entities;

namespace UserManagement.DataAccess.Data
{
    /// <summary>
    /// The context for application
    /// </summary>
    public class UserContext : IdentityDbContext<User, IdentityRole, string>
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(UserContext).Assembly);
        }

    }
}
