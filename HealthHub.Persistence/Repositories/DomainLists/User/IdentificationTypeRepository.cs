using Angels.Packages.Cache.Interfaces;
using Angels.Packages.MongoDb.ApplicationContext;
using Angels.Packages.MongoDb.Repository;
using HealthHub.Domain.DomainLists.User;
using HealthHub.Domain.Interfaces.AuthService;
using Microsoft.Extensions.Logging;

namespace HealthHub.Persistence.Repositories.DomainLists.User;

public class IdentificationTypeRepository : DomainList<IdentificationType>, IIdentificationTypeRepository
{
    public IdentificationTypeRepository(
        ILogger<CoreRepository<IdentificationType>> logger,
        ApplicationDbContext applicationDbContext,
        ICacheService cacheService) : base(logger, applicationDbContext, cacheService) { }
}
