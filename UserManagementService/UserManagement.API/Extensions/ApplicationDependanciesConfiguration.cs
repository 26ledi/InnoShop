using InnoShop.MessageBrokers.Shared;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;
using UserManagement.BusinessLogic.SeedData;
using UserManagement.BusinessLogic.Services.Implementations;
using UserManagement.BusinessLogic.Services.Interfaces;
using UserManagement.DataAccess.Extensions;

namespace UserManagement.API.Extensions
{
    public static partial class ApplicationDependanciesConfiguration
    {
        /// <summary>
        /// Add the identity service database and all the dependencies
        /// </summary>
        /// <param name="builder">The builder</param>
        /// <returns>A <see cref="IServiceCollection"/></returns>
        public  static IServiceCollection ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentityDatabase(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            builder.Services.ConfigureMassTransit(builder.Configuration);
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            builder.Services.AddScoped<SeedData>();
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            builder.Services.AddFluentValidationServices();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Please Enter token",
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    BearerFormat = "Jwt",
                    Scheme = "bearer"

                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });

            }
               )
               .AddEndpointsApiExplorer();
            builder.AddJwtToken();

            return builder.Services;
        }
        /// <summary>
        /// Add all the middleware of the api
        /// </summary>
        /// <param name="application">The webapplication</param>
        /// <returns>A <see cref="Task"/></returns>
        public async static Task Configure(this WebApplication application)
        {
            application.UseExceptionHandler();
            await application.UseMigration();

            application.UseAuthentication();
        }
        /// <summary>
        /// Adds massTransit and RabbitMQ configuration.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<RabbitMQConfigurations>().Bind(configuration.GetSection("RabbitMQ"));

            services.AddMassTransit(_busRegistration =>
            {
                _busRegistration.UsingRabbitMq((context, cfg) =>
                {
                    var options = context.GetRequiredService<IOptions<RabbitMQConfigurations>>().Value;

                    cfg.Host(options.Host, h =>
                    {
                        h.Username(options.Username);
                        h.Password(options.Password);
                    });
                });
            });

            return services;
        }


    }
}
