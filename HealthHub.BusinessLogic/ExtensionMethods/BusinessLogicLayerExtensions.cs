using HealthHub.Application.Interfaces.AuthService;
using HealthHub.Application.Interfaces.TokenizationService;
using HealthHub.Application.Interfaces.UserManagement;
using HealthHub.BusinessLogic.Services.AuthService;
using HealthHub.BusinessLogic.Services.TokenizationService;
using HealthHub.BusinessLogic.Services.UserManagement;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HealthHub.BusinessLogic.ExtensionMethods;

public static class BusinessLogicLayerExtensions
{
    public static void AddBusinessLogicLayerExtensions(this IServiceCollection services)
    {
        // Global
        services.AddAutoMapper(assemblies: Assembly.GetExecutingAssembly());

        // AuthService
        services.AddSingleton<IAuthService, AuthService>();

        // TokenizationService
        services.AddSingleton<ITokenizationDataService, TokenizationDataService>();
        services.AddSingleton<ITokenizationManagementService, TokenizationManagementService>();

        // UserManagement
        services.AddSingleton<IUserManagementService, UserManagementService>();
    }
}
