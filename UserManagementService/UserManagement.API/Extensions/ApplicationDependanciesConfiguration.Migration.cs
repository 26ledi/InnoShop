using Microsoft.EntityFrameworkCore;
using UserManagement.BusinessLogic.SeedData;
using UserManagement.DataAccess.Data;

namespace UserManagement.API.Extensions
{
    /// <summary>
    /// Configurations of the migration of the user
    /// </summary>
    public static partial class ApplicationDependanciesConfigurations
    {
        /// <summary>
        /// Add the migration to the database
        /// </summary>
        /// <param name="application">The application builder</param>
        /// <returns>A <see cref="Task"/></returns>
        public async static Task UseMigration(this WebApplication application)
        {
            var serviceScopeFactory = application.Services.GetService<IServiceScopeFactory>();
            using var scope = serviceScopeFactory.CreateScope();

            var handler = scope.ServiceProvider.GetRequiredService<UserContext>();
            var seedDataHandler = scope.ServiceProvider.GetRequiredService<SeedData>();
            await handler.Database.MigrateAsync();
            await seedDataHandler.InitializeRolesAsync();
            await seedDataHandler.SeedAdminAsync();
        }
    }
}
