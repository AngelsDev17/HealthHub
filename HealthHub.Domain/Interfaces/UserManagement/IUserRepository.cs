using Angels.Packages.MongoDb.Enums;
using Angels.Packages.MongoDb.Interfaces;
using HealthHub.Domain.Enums.UserManagement;
using HealthHub.Domain.Models.UserManagement;

namespace HealthHub.Domain.Interfaces.UserManagement;

public interface IUserRepository : IBaseRepository<User>
{
    Task ActivateUserById(
        string id,
        DateTime activationDate,
        ActivationMethod activationMethod);

    Task UpdateUserById(User user, Status status = Status.Active);

    Task UpdateEmailByIdAndStatus(
        string userId,
        string email,
        Status status = Status.Active);
}
