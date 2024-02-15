using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.DataAccess.Entities;

namespace UserManagement.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="builder">The builder</param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);
            builder.Property(user => user.UserName).IsRequired();
            builder.ToTable("Users");
        }
    }
}
