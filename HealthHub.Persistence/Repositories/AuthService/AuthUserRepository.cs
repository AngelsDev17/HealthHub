using Angels.Packages.Cache.Interfaces;
using Angels.Packages.MongoDb.ApplicationContext;
using Angels.Packages.MongoDb.Enums;
using Angels.Packages.MongoDb.Repository;
using HealthHub.Domain.Interfaces.AuthService;
using HealthHub.Domain.Models.AuthService;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace HealthHub.Persistence.Repositories.AuthService;

public class AuthUserRepository : BaseRepository<AuthUser>, IAuthUserRepository
{
    public AuthUserRepository(
        ILogger<BaseRepository<AuthUser>> logger,
        ApplicationDbContext dbContext,
        ICacheService cacheService) : base(logger, dbContext, cacheService) { }


    public Task<AuthUser> FindOneByIdOrEmail(
        string userId,
        string email)
    {
        var filterDefinition = _filterDefinitionBuilder.Where(item => item.Id.Equals(userId) ||
                                                                      item.Email.Equals(email));

        return FindOneAsync(filterDefinition: filterDefinition);
    }

    public Task<AuthUser> FindOneByEmailAndStatus(
        string email,
        Status status = Status.Active)
    {
        var filterDefinition = _filterDefinitionBuilder.Where(item => item.Email.Equals(email) &&
                                                                      item.Status == status);

        return FindOneAsync(filterDefinition: filterDefinition);
    }

    public Task<AuthUser> FindOneByUserIdAndStatus(
        string userId,
        Status status = Status.Active)
    {
        var filterDefinition = _filterDefinitionBuilder.Where(item => item.UserId.Equals(userId) &&
                                                                      item.Status == status);

        return FindOneAsync(filterDefinition: filterDefinition);
    }

    public Task<AuthUser> FindOneByIdAndActivationIdAndActivationCodeAndStatus(
        string id,
        string activationId,
        int activationCode,
        Status status = Status.Inactive)
    {
        var filterDefinition = _filterDefinitionBuilder.Where(item => item.Id.Equals(id) &&
                                                                      item.Activation.ActivationId.Equals(activationId) &&
                                                                      item.Activation.ActivationCode.Equals(activationCode) &&
                                                                      item.Status == status);

        return FindOneAsync(filterDefinition: filterDefinition);
    }

    public Task<AuthUser> FindOneByIdAndResetPasswordIdAndCodeAndStatus(
        string id,
        string resetPasswordId,
        int resetPasswordCode,
        Status status = Status.Active)
    {
        var filterDefinition = _filterDefinitionBuilder.Where(item => item.Id.Equals(id) &&
                                                                      item.ResetPassword.ResetPasswordId.Equals(resetPasswordId) &&
                                                                      item.ResetPassword.ResetPasswordCode.Equals(resetPasswordCode) &&
                                                                      item.Status == status);

        return FindOneAsync(filterDefinition: filterDefinition);
    }

    public Task<AuthUser> FindOneByIdAndUpdateEmailIdAndCodeAndEmailAndDateAndStatus(
        string id,
        string newEmail,
        string updateEmailId,
        int updateEmailCode,
        Status status = Status.Active)
    {
        var filterDefinition = _filterDefinitionBuilder.Where(item => item.Id.Equals(id) &&
                                                                      item.UpdateEmail.NewEmail.Equals(newEmail) &&
                                                                      item.UpdateEmail.UpdateEmailId.Equals(updateEmailId) &&
                                                                      item.UpdateEmail.UpdateEmailCode.Equals(updateEmailCode) &&
                                                                      item.Status == status);

        return FindOneAsync(filterDefinition: filterDefinition);
    }

    public Task ActivateUserByIdAndStatus(
        string id,
        Status status = Status.Inactive)
    {
        var filterDefinition = _filterDefinitionBuilder.Where(item => item.Id.Equals(id) &&
                                                                      item.Status == status);

        var updateDefinition = _updateDefinitionBuilder.Set(item => item.Activation.ActivationId, null)
                                                       .Set(item => item.Activation.ActivationCode, null)
                                                       .Set(item => item.Status, Status.Active);

        return UpdateOneAsync(
            filterDefinition: filterDefinition,
            updateDefinition: updateDefinition);
    }

    public Task UpdateResetPasswordByIdAndStatus(
        string id,
        ResetPassword resetPasswordRecord,
        Status status = Status.Active)
    {
        var filterDefinition = _filterDefinitionBuilder.Where(item => item.Id.Equals(id) &&
                                                                      item.Status == status);

        var updateDefinition = _updateDefinitionBuilder.Set(item => item.ResetPassword.ResetPasswordId, resetPasswordRecord.ResetPasswordId)
                                                       .Set(item => item.ResetPassword.ResetPasswordCode, resetPasswordRecord.ResetPasswordCode)
                                                       .Set(item => item.ResetPassword.ExpirationDate, resetPasswordRecord.ExpirationDate);

        return UpdateOneAsync(
            filterDefinition: filterDefinition,
            updateDefinition: updateDefinition);
    }

    public Task UpdateEmailIdAndCodeAndEmailByIdAndStatus(
        string id,
        UpdateEmail updateEmail,
        Status status = Status.Active)
    {
        var filterDefinition = _filterDefinitionBuilder.Where(item => item.Id.Equals(id) &&
                                                                      item.Status == status);

        var updateDefinition = _updateDefinitionBuilder.Set(item => item.UpdateEmail.NewEmail, updateEmail.NewEmail)
                                                       .Set(item => item.UpdateEmail.UpdateEmailId, updateEmail.UpdateEmailId)
                                                       .Set(item => item.UpdateEmail.UpdateEmailCode, updateEmail.UpdateEmailCode)
                                                       .Set(item => item.UpdateEmail.ExpirationDate, updateEmail.ExpirationDate);

        return UpdateOneAsync(
            filterDefinition: filterDefinition,
            updateDefinition: updateDefinition);
    }

    public Task UpdatePasswordAndSecretIdByIdAndStatus(
        string id,
        string newPassword,
        string secretId,
        Status status = Status.Active)
    {
        var filterDefinition = _filterDefinitionBuilder.Where(item => item.Id.Equals(id) &&
                                                                      item.Status == status);

        var updateDefinition = _updateDefinitionBuilder.Set(item => item.ResetPassword.ResetPasswordId, null)
                                                       .Set(item => item.ResetPassword.ResetPasswordCode, null)
                                                       .Set(item => item.ResetPassword.ExpirationDate, null)
                                                       .Set(item => item.Password, newPassword)
                                                       .Set(item => item.SecretId, secretId);

        return UpdateOneAsync(
            filterDefinition: filterDefinition,
            updateDefinition: updateDefinition);
    }

    public Task UpdateEmailByIdAndStatus(
        string id,
        string email,
        Status status = Status.Active)
    {
        var filterDefinition = _filterDefinitionBuilder.Where(item => item.Id.Equals(id) &&
                                                                      item.Status == status);

        var updateDefinition = _updateDefinitionBuilder.Set(item => item.UpdateEmail.NewEmail, null)
                                                       .Set(item => item.UpdateEmail.UpdateEmailId, null)
                                                       .Set(item => item.UpdateEmail.UpdateEmailCode, null)
                                                       .Set(item => item.UpdateEmail.ExpirationDate, null)
                                                       .Set(item => item.Email, email);

        return UpdateOneAsync(
            filterDefinition: filterDefinition,
            updateDefinition: updateDefinition);
    }
}
