using Angels.Packages.Cache.Interfaces;
using Angels.Packages.MongoDb.ApplicationContext;
using Angels.Packages.MongoDb.Repository;
using HealthHub.Domain.Interfaces.TokenizationService;
using HealthHub.Domain.Models.TokenizationService;
using Microsoft.Extensions.Logging;

namespace HealthHub.Persistence.Repositories.TokenizationService;

public class PasswordSecretRepository : BaseRepository<PasswordSecret>, IPasswordSecretRepository
{
    public PasswordSecretRepository(
        ILogger<BaseRepository<PasswordSecret>> logger,
        ApplicationDbContext dbContext,
        ICacheService cacheService) : base(logger, dbContext, cacheService) { }
}
