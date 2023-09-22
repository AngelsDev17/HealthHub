using Angels.Packages.MongoDb.Interfaces;
using HealthHub.Domain.Models.TokenizationService;

namespace HealthHub.Domain.Interfaces.TokenizationService;

public interface IPasswordSecretRepository : IBaseRepository<PasswordSecret>
{

}
