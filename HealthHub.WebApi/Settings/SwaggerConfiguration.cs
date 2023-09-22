using HealthHub.Application.Dtos.AuthService;
using HealthHub.Application.Dtos.Commons;
using HealthHub.Application.Dtos.UserManagement;
using HealthHub.Application.Enums.AuthService;
using Swashbuckle.AspNetCore.Filters;

namespace HealthHub.WebApi.Settings;

// AuthController
public class UserToRegisterExample : IExamplesProvider<UserToRegisterDto>
{
    public UserToRegisterDto GetExamples() => new()
    {
        Name = "Miguel Angel",
        Surname = "Cuellar Arias",
        Identification = new()
        {
            Value = "1021663015",
            IdentificationType = IdentificationType.CC,
        },
        Email = "miguelangels2024@gmail.com",
        PhoneNumber = "3168580636",
        Address = "Cll 43a sur #11c-26 este",
        Password = "Pass@2468**",
        Role = Role.User,
    };
}
public class UserActivationExample : IExamplesProvider<UserActivationDto>
{
    public UserActivationDto GetExamples() => new()
    {
        Id = Guid.NewGuid().ToString(),
        ActivationId = Guid.NewGuid().ToString(),
        ActivationCode = 562485,
        ActivationMethod = ActivationMethod.EmailActivation,
    };
}
public class UserToAuthExample : IExamplesProvider<UserToAuthDto>
{
    public UserToAuthDto GetExamples() => new()
    {
        Email = "miguelangels2024@gmail.com",
        Password = "Pass@2468**",
    };
}
public class IdentificationExample : IExamplesProvider<IdentificationDto>
{
    public IdentificationDto GetExamples() => new()
    {
        Value = "1021663015",
        IdentificationType = IdentificationType.CC,
    };
}
public class ResetPasswordExample : IExamplesProvider<ResetPasswordDto>
{
    public ResetPasswordDto GetExamples() => new()
    {
        Id = Guid.NewGuid().ToString(),
        NewPassword = "Pass@2468**",
        ResetPasswordId = Guid.NewGuid().ToString(),
        ResetPasswordCode = 562485,
    };
}

// ProfileController
public class UserToUpdateExample : IExamplesProvider<UserToUpdateDto>
{
    public UserToUpdateDto GetExamples() => new()
    {
        Name = "Miguel Angel",
        Surname = "Cuellar Arias",
        PhoneNumber = "3168580636",
        Address = "Cll 43a sur #11c-26 este",
    };
}
public class UserToUpdateEmailExample : IExamplesProvider<UserToUpdateEmailDto>
{
    public UserToUpdateEmailDto GetExamples() => new()
    {
        Email = "miguelangels2024@gmail.com",
    };
}
public class UpdateEmailExample : IExamplesProvider<UpdateEmailDto>
{
    public UpdateEmailDto GetExamples() => new()
    {
        Id = Guid.NewGuid().ToString(),
        NewEmail = "miguelangels2024@gmail.com",
        UpdateEmailId = Guid.NewGuid().ToString(),
        UpdateEmailCode = 864762,
        UpdateEmailDate = DateTime.Now,
    };
}
public class UserToUpdatePasswordExample : IExamplesProvider<UserToUpdatePasswordDto>
{
    public UserToUpdatePasswordDto GetExamples() => new()
    {
        CurrentPassword = "Pass@2468**",
        NewPassword = "Pass@2468**",
    };
}
