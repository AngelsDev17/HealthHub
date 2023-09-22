using Angels.Packages.MongoDb.Enums;
using Angels.Packages.MongoDb.Interfaces;
using HealthHub.Domain.Models.AuthService;

namespace HealthHub.Domain.Interfaces.AuthService;

public interface IAuthUserRepository : IBaseRepository<AuthUser>
{
    Task<AuthUser> FindOneByIdOrEmail(
        string id,
        string email);

    Task<AuthUser> FindOneByEmailAndStatus(
        string email,
        Status status = Status.Active);

    Task<AuthUser> FindOneByUserIdAndStatus(
        string userId,
        Status status = Status.Active);

    Task<AuthUser> FindOneByIdAndActivationIdAndActivationCodeAndStatus(
        string id,
        string activationId,
        int activationCode,
        Status status = Status.Inactive);

    Task<AuthUser> FindOneByIdAndResetPasswordIdAndCodeAndStatus(
        string id,
        string resetPasswordId,
        int resetPasswordCode,
        Status status = Status.Active);

    Task<AuthUser> FindOneByIdAndUpdateEmailIdAndCodeAndEmailAndDateAndStatus(
        string id,
        string newEmail,
        string updateEmailId,
        int updateEmailCode,
        Status status = Status.Active);

    Task ActivateUserByIdAndStatus(
        string id,
        Status status = Status.Inactive);

    Task UpdateResetPasswordByIdAndStatus(
        string id,
        ResetPassword resetPasswordRecord,
        Status status = Status.Active);

    Task UpdateEmailIdAndCodeAndEmailByIdAndStatus(
        string id,
        UpdateEmail updateEmail,
        Status status = Status.Active);

    Task UpdatePasswordAndSecretIdByIdAndStatus(
        string id,
        string newPassword,
        string secretId,
        Status status = Status.Active);

    Task UpdateEmailByIdAndStatus(
        string id,
        string email,
        Status status = Status.Active);
}
