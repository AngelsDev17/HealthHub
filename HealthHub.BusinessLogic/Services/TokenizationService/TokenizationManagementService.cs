using Angels.Packages.Logger.Services;
using Angels.Packages.MongoDb.Enums;
using HealthHub.Application.Constants;
using HealthHub.Application.Interfaces.TokenizationService;
using HealthHub.Domain.Interfaces.TokenizationService;
using HealthHub.Domain.Models.TokenizationService;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace HealthHub.BusinessLogic.Services.TokenizationService;

public class TokenizationManagementService : ITokenizationManagementService
{
    private readonly ILogger<TokenizationManagementService> _logger;
    private readonly ISystemSecretRepository _systemSecretRepository;
    private readonly IPasswordSecretRepository _passwordSecretRepository;

    public TokenizationManagementService(
        ILogger<TokenizationManagementService> logger,
        ISystemSecretRepository systemSecretRepository,
        IPasswordSecretRepository passwordSecretRepository)
    {
        _logger = logger;
        _systemSecretRepository = systemSecretRepository;
        _passwordSecretRepository = passwordSecretRepository;
    }


    public async Task<byte[]> GetCurrentSystemSecret()
    {
        using var cryptoService = TripleDES.Create();

        var currentSystemSecret = await _systemSecretRepository.FindOneByStatusAsync();

        if (currentSystemSecret is not null) return currentSystemSecret.Secret;

        var systemSecret = new SystemSecret()
        {
            Secret = cryptoService.Key,
            Status = Status.Active
        };

        await _systemSecretRepository.InsertOneAsync(entity: systemSecret);

        _logger.Information("Se genera el secreto de sistema exitosamente.");

        return systemSecret.Secret;
    }

    public async Task<(string secretId, byte[] secret)> GenerateNewPasswordSecret()
    {
        using var cryptoService = TripleDES.Create();

        await _passwordSecretRepository.UpdateStatusByCurrentStatusAsync(
            currentStatus: Status.Active,
            newStatus: Status.Inactive);

        var passwordSecret = new PasswordSecret()
        {
            Secret = cryptoService.Key,
            ExpirationDate = DateTime.Now.AddDays(value: EnvironmentVariables.EXPIRATION_DATE_DAYS),
            Status = Status.Active,
        };

        var secretId = await _passwordSecretRepository.InsertOneAsync(entity: passwordSecret);

        _logger.Information("Se genera el secreto de password exitosamente.");

        return (secretId, passwordSecret.Secret);
    }
}
