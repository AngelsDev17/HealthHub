using Angels.Packages.MongoDb.ApplicationContext;
using Angels.Packages.MongoDb.ExtensionMethods;
using Angels.Packages.MongoDb.Models;
using HealthHub.Domain.Interfaces.AuthService;
using HealthHub.Domain.Interfaces.TokenizationService;
using HealthHub.Domain.Interfaces.UserManagement;
using HealthHub.Persistence.Repositories.AuthService;
using HealthHub.Persistence.Repositories.TokenizationService;
using HealthHub.Persistence.Repositories.UserManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthHub.Persistence.ExtensionMethods;

public static class PersistenceLayerExtensions
{
    public static void AddPersistenceLayerExtensions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // MongoDb
        services.AddMongoDbExtensions<ApplicationDbContext>(options =>
            options.GetDbSettings(
                configuration.GetSection(
                    $"{nameof(DbSettings)}").Get<DbSettings>()));


        // AuthService
        services.AddSingleton<IAuthUserRepository, AuthUserRepository>();

        // TokenizationService
        services.AddSingleton<ISystemSecretRepository, SystemSecretRepository>();
        services.AddSingleton<IPasswordSecretRepository, PasswordSecretRepository>();

        // UserManagement
        services.AddSingleton<IUserRepository, UserRepository>();
    }
}
