namespace HealthHub.Application.Interfaces.TokenizationService;

public interface ITokenizationManagementService
{
    Task<byte[]> GetCurrentSystemSecret();
    Task<(string secretId, byte[] secret)> GenerateNewPasswordSecret();
}
