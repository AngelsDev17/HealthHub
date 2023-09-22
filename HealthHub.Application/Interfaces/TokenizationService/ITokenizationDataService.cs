using HealthHub.Application.Dtos.TokenizationService;

namespace HealthHub.Application.Interfaces.TokenizationService;

public interface ITokenizationDataService
{
    Task<string> TokenizeSystemValue(string value);
    Task<TokenizedValueDto> TokenizePassword(string value);
    Task<string> TokenizeExistingPassword(TokenizedValueDto tokenizedValue);
}
