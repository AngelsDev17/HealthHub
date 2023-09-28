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
        Age = 19,
        Identification = new()
        {
            Value = "1021663015",
            IdentificationType = new() { Id = "CC", Value = "Cédula de ciudadanía" },
        },
        JuridicalIdentification = new()
        {
            Value = "123456789-1",
            JuridicalIdentificationType = new() { Id = "NIT", Value = "Número de identificación tributaria" },
        },
        Gender = new() { Id = "Male", Value = "Masculino" },
        PhoneNumber = "3168580636",
        City = new() { Id = "001", Value = "Bogotá D.C." },
        Locality = new() { Id = "004", ParentId = "001", Value = "San Cristobal" },
        Address = "Cll 43a sur #11c-26 este",
        Email = "miguelangels2024@gmail.com",
        Password = "Pass@2468**",
        Role = new() { Id = "Client", Value = "Cliente" },
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
        IdentificationType = new() { Id = "CC", Value = "Cédula de ciudadanía" },
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
        Age = 22,
        Gender = new() { Id = "Female", Value = "Femenino" },
        PhoneNumber = "3168580636",
        City = new() { Id = "001", Value = "Bogotá D.C." },
        Locality = new() { Id = "010", ParentId = "001", Value = "Engativá" },
        Address = "Cra 11 #79-54",
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
