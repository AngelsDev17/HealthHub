using Angels.Packages.Cache.Interfaces;
using Angels.Packages.MongoDb.ApplicationContext;
using Angels.Packages.MongoDb.Enums;
using Angels.Packages.MongoDb.Repository;
using HealthHub.Domain.Enums.UserManagement;
using HealthHub.Domain.Interfaces.UserManagement;
using HealthHub.Domain.Models.UserManagement;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace HealthHub.Persistence.Repositories.UserManagement;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(
        ILogger<BaseRepository<User>> logger,
        ApplicationDbContext dbContext,
        ICacheService cacheService) : base(logger, dbContext, cacheService) { }


    public Task ActivateUserById(
        string id,
        DateTime activationDate,
        ActivationMethod activationMethod)
    {
        var filterDefinition = _filterDefinitionBuilder.Where(item => item.Id.Equals(id) &&
                                                                      !item.Activation.IsActivated);

        var updateDefinition = _updateDefinitionBuilder.Set(item => item.Activation.IsActivated, true)
                                                       .Set(item => item.Activation.ActivationDate, activationDate)
                                                       .Set(item => item.Activation.ActivationMethod, activationMethod);

        return UpdateOneAsync(
            filterDefinition: filterDefinition,
            updateDefinition: updateDefinition);
    }

    public Task UpdateUserById(User user, Status status = Status.Active)
    {
        var filterDefinition = _filterDefinitionBuilder.Where(item => item.Id.Equals(user.Id) &&
                                                                      item.Status == status);

        var updateDefinition = _updateDefinitionBuilder.Set(item => item.Name, user.Name)
                                                       .Set(item => item.Surname, user.Surname)
                                                       .Set(item => item.Age, user.Age)
                                                       .Set(item => item.Gender, user.Gender)
                                                       .Set(item => item.PhoneNumber, user.PhoneNumber)
                                                       .Set(item => item.City, user.City)
                                                       .Set(item => item.Locality, user.Locality)
                                                       .Set(item => item.Address, user.Address);

        return UpdateOneAsync(
            filterDefinition: filterDefinition,
            updateDefinition: updateDefinition);
    }

    public Task UpdateEmailByIdAndStatus(
        string userId,
        string email,
        Status status = Status.Active)
    {
        var filterDefinition = _filterDefinitionBuilder.Where(item => item.Id.Equals(userId) &&
                                                                      item.Status == status);

        var updateDefinition = _updateDefinitionBuilder.Set(item => item.Email, email);

        return UpdateOneAsync(
            filterDefinition: filterDefinition,
            updateDefinition: updateDefinition);
    }
}
