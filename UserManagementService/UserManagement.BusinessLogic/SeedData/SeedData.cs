using Microsoft.AspNetCore.Identity;
using UserManagement.BusinessLogic.Helpers;
using UserManagement.DataAccess.Entities;

namespace UserManagement.BusinessLogic.SeedData
{
    /// <summary>
    /// The class for the SeedData
    /// </summary>
    public class SeedData
    {
        private  readonly RoleManager<IdentityRole> _roleManager;
        private  readonly UserManager<User> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeedData"/> class.
        /// </summary>
        /// <param name="roleManager">The role manager.</param>
        public  SeedData(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        /// <summary>
        /// Initializes the predefined roles in the application if they do not already exist.
        /// </summary>
        /// 
        public async Task InitializeRolesAsync()
        {
            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);

                if (!roleExist)
                {
                    roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        /// <summary>
        /// Adding an administator
        /// </summary>
        /// <returns></returns>
        public async Task SeedAdminAsync()
        {
            if (!await _userManager.IsUserEmailExist("joyceledi26@gmail.com")) 
            {
                var user = new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "joyceledi26@gmail.com",
                    UserName = "silicon26",
                    Name = "Ledi",
                    Surname = "joyce",
                    PhoneNumber = "+375257716193",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    TwoFactorEnabled = false,
                    AccessFailedCount = 0,
                    SecurityStamp = "LUDXP2TE5RJPVHUCQMNQSOTFMPLHAFUH"
                };

                var password = "joyce2601";
                var passwordHash = new PasswordHasher<User>().HashPassword(user, password);
                user.PasswordHash = passwordHash;

                await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
