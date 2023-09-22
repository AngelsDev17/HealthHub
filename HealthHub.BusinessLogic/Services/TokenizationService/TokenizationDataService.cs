using Angels.Packages.Logger.Services;
using AutoMapper;
using HealthHub.Application.Dtos.TokenizationService;
using HealthHub.Application.Interfaces.TokenizationService;
using HealthHub.Domain.Interfaces.TokenizationService;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace HealthHub.BusinessLogic.Services.TokenizationService
{
    public class TokenizationDataService : ITokenizationDataService
    {
        private readonly ILogger<TokenizationDataService> _logger;
        private readonly ITokenizationManagementService _tokenizationManagementService;
        private readonly IPasswordSecretRepository _passwordSecretRepository;
        private readonly IMapper _mapper;

        public TokenizationDataService(
            ILogger<TokenizationDataService> logger,
            ITokenizationManagementService tokenizationManagementService,
            IPasswordSecretRepository passwordSecretRepository,
            IMapper mapper)
        {
            _logger = logger;
            _tokenizationManagementService = tokenizationManagementService;
            _passwordSecretRepository = passwordSecretRepository;
            _mapper = mapper;
        }


        public async Task<string> TokenizeSystemValue(string value)
        {
            var systemSecret = await _tokenizationManagementService.GetCurrentSystemSecret();

            _logger.Debug("Se obtiene el secreto de sistema.");

            return await GenerateFinalValue(
                secret: systemSecret,
                value: value);
        }

        public async Task<TokenizedValueDto> TokenizePassword(string value)
        {
            var currentSecret = await _passwordSecretRepository.FindOneByStatusAsync();

            if (currentSecret is null || currentSecret.ExpirationDate < DateTime.Now)
            {
                var (secretId, secret) = await _tokenizationManagementService.GenerateNewPasswordSecret();

                currentSecret.Id = secretId;
                currentSecret.Secret = secret;
            }

            _logger.Debug("Se obtiene el secreto de password.");

            var tokenizedValue = await GenerateFinalValue(
                secret: currentSecret.Secret,
                value: value);

            return new()
            {
                TokenizedValue = tokenizedValue,
                SecretId = currentSecret.Id,
            };
        }
        public async Task<string> TokenizeExistingPassword(TokenizedValueDto tokenizedValue)
        {
            var secret = await _passwordSecretRepository.FindOneByIdAsync(entityId: tokenizedValue.SecretId);

            if (secret is null)
                _logger.Warning(
                    message: $"El secreto {tokenizedValue.SecretId} no existe.",
                    exception: new InvalidDataException("El secreto no existe."));

            _logger.Debug("Se obtiene el secreto de password.");

            return await GenerateFinalValue(
                secret: secret.Secret,
                value: tokenizedValue.TokenizedValue);
        }

        private static Task<string> GenerateFinalValue(byte[] secret, string value)
        {
            using var hmac256 = new HMACSHA256(secret);
            using var stream = new MemoryStream(Encoding.ASCII.GetBytes(value));

            var tokenizedValue = hmac256.ComputeHash(stream)
                                        .Aggregate("", (s, e) => s + string.Format("{0:x2}", e), s => s);

            return Task.FromResult(tokenizedValue);
        }
    }
}
