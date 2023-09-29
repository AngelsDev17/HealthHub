using Angels.Packages.Cache.Interfaces;
using Angels.Packages.MongoDb.ApplicationContext;
using Angels.Packages.MongoDb.Repository;
using HealthHub.Domain.Interfaces.AuthService;
using HealthHub.Domain.Models.DomainLists.User;
using Microsoft.Extensions.Logging;

namespace HealthHub.Persistence.Repositories.DomainLists.User;

public class GenderRepository : DomainList<Gender>, IGenderRepository
{
    public GenderRepository(
        ILogger<CoreRepository<Gender>> logger,
        ApplicationDbContext applicationDbContext,
        ICacheService cacheService) : base(logger, applicationDbContext, cacheService) { }
}
