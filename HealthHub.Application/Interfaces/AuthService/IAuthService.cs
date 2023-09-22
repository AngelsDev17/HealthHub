using HealthHub.Application.Dtos.AuthService;
using HealthHub.Application.Dtos.Commons;

namespace HealthHub.Application.Interfaces.AuthService;

public interface IAuthService
{
    Task RegisterUser(UserToRegisterDto userToRegister);
    Task<string> UserActivation(UserActivationDto userActivation);

    Task<string> SignInUser(UserToAuthDto userToAuth);

    Task ResetPassword(IdentificationDto identification);
    Task ConfirmResetPassword(ResetPasswordDto resetPassword);

    Task UpdateEmail(UserToUpdateEmailDto userToUpdateEmail);
    Task ConfirmUpdateEmail(UpdateEmailDto resetPassword);
    Task UpdatePassword(UserToUpdatePasswordDto userToUpdatePassword);
}
