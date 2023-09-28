using Angels.Packages.JwtToken.Models;
using Angels.Packages.JwtToken.Services;
using Angels.Packages.Logger.Services;
using AutoMapper;
using HealthHub.Application.Dtos.AuthService;
using HealthHub.Application.Dtos.Commons;
using HealthHub.Application.Dtos.UserManagement;
using HealthHub.Application.Interfaces.AuthService;
using HealthHub.Application.Interfaces.TokenizationService;
using HealthHub.Application.Interfaces.UserManagement;
using HealthHub.BusinessLogic.Utils;
using HealthHub.Domain.Interfaces.AuthService;
using HealthHub.Domain.Models.AuthService;
using Microsoft.Extensions.Logging;
using MimeKit;
using System.Security.Claims;

namespace HealthHub.BusinessLogic.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly JwtTokenSettings _jwtTokenSettings;

        private readonly ILogger<AuthService> _logger;
        private readonly ITokenizationDataService _tokenizationDataService;
        private readonly IUserManagementService _userManagementService;
        private readonly IAuthUserRepository _authUserRepository;
        private readonly IMapper _mapper;

        private readonly Random _random;

        public AuthService(
            JwtTokenSettings jwtTokenSettings,
            ILogger<AuthService> logger,
            ITokenizationDataService tokenizationDataService,
            IUserManagementService userManagementService,
            IAuthUserRepository authUserRepository,
            IMapper mapper)
        {
            _jwtTokenSettings = jwtTokenSettings;

            _logger = logger;
            _tokenizationDataService = tokenizationDataService;
            _userManagementService = userManagementService;
            _authUserRepository = authUserRepository;
            _mapper = mapper;

            _random = new Random();
        }


        public async Task RegisterUser(UserToRegisterDto userToRegister)
        {
            // Se obtiene el UserId del usuario.

            var id = await _tokenizationDataService.TokenizeSystemValue(value: UserUtils.GetUserId(identification: userToRegister.Identification));

            // Valida con el UserId y el Email que el usuario no exista.

            var existingUser = await _authUserRepository.FindOneByIdOrEmail(
                id: id,
                email: userToRegister.Email);

            if (existingUser is not null)
                _logger.Warning(
                    message: $"El usuario {id} fue previamente registrado en el sistema.",
                    exception: new InvalidDataException("Usuario previamente registrado en el sistema."));

            var authUser = _mapper.Map<AuthUser>(userToRegister);

            // Se crea el usuario en el servicio UserManagement.

            var userId = await _userManagementService.RegisterNewUser(
                userInformation: _mapper.Map<UserInformationDto>(userToRegister));

            authUser.Id = id;
            authUser.UserId = userId;

            // Genera un token basado en la contraseña y se asignan los valores.

            var tokenizedPassword = await _tokenizationDataService.TokenizePassword(value: userToRegister.Password);

            authUser.Password = tokenizedPassword.TokenizedValue;
            authUser.SecretId = tokenizedPassword.SecretId;

            await _authUserRepository.InsertOneAsync(entity: authUser);

            // Se envia el correo de activacion.

            var userEmails = new List<string> { userToRegister.Email };
            var subject = "Activa tu cuenta!";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $"Para activar tu cuenta ingresa en el siguiente <a href=\"https://localhost:17568?aid={authUser.Activation.ActivationId}&uid={authUser.Id}\">enlace</a> e ingresa el codigo <b>{authUser.Activation.ActivationCode}</b>..."
            };

            await EmailUtils.SendEmailToUsers(
                userEmails: userEmails,
                subject: subject,
                bodyBuilder: bodyBuilder);

            _logger.Information($"Se termina el proceso de registro del usuario {id} y se envia su correo de activacion.");
        }
        public async Task<string> UserActivation(UserActivationDto userActivation)
        {
            // Se busca el usuario y se valida el ActivationId.

            var authUser = await _authUserRepository.FindOneByIdAndActivationIdAndActivationCodeAndStatus(
                id: userActivation.Id,
                activationId: userActivation.ActivationId,
                activationCode: userActivation.ActivationCode);

            if (authUser is null)
                _logger.Warning(
                    message: $"El codigo del usurio {userActivation.Id} no es correcto.",
                    exception: new InvalidDataException("El codigo no es correcto."));

            // Se activa el usuario en la DB y en el servicio UserManagement.

            await _authUserRepository.ActivateUserByIdAndStatus(id: authUser.Id);

            await _userManagementService.UserActivation(
                basicUserActivation: new()
                {
                    UserId = authUser.UserId,
                    ActivationDate = DateTime.Now,
                    ActivationMethod = userActivation.ActivationMethod,
                });

            _logger.Information($"Se realiza la activacion del usuario {userActivation.Id} en el sistema.");

            // Se obtiene la informacion del usuario.

            var userInformation = await _userManagementService.GetUserInformationById(userId: authUser.UserId);

            // Se retorna el bearerToken del usuario.

            return await ConfigureBearerToken(
                userInformation: userInformation,
                role: authUser.Role.Id);
        }

        public async Task<string> SignInUser(UserToAuthDto userToAuth)
        {
            // Valida que el Email ingresado sea correcto.

            var existingUser = await _authUserRepository.FindOneByEmailAndStatus(email: userToAuth.Email);

            if (existingUser is null)
                _logger.Warning(
                    message: $"Las credenciales del usuario {userToAuth.Email} son incorrectas.",
                    exception: new InvalidDataException("Las credenciales son incorrectas."));

            // Valida que la Contraseña sea correcta.

            var tokenizedPassword = await _tokenizationDataService.TokenizeExistingPassword(
                                        tokenizedValue: new()
                                        {
                                            TokenizedValue = userToAuth.Password,
                                            SecretId = existingUser.SecretId
                                        });

            if (!tokenizedPassword.Equals(existingUser.Password))
                _logger.Warning(
                    message: $"Las credenciales del usuario {userToAuth.Email} son incorrectas.",
                    exception: new InvalidDataException("Las credenciales son incorrectas."));

            // Se obtiene la informacion del usuario.

            var userInformation = await _userManagementService.GetUserInformationById(userId: existingUser.UserId);

            // Se retorna el bearerToken del usuario.

            return await ConfigureBearerToken(
                userInformation: userInformation,
                role: existingUser.Role.Id);
        }

        public async Task ResetPassword(IdentificationDto identification)
        {
            // Valida que el usuario ingresado exista.

            var id = await _tokenizationDataService.TokenizeSystemValue(value: UserUtils.GetUserId(identification: identification));
            var existingUser = await _authUserRepository.FindOneByIdAndStatusAsync(entityId: id);

            if (existingUser is null)
                _logger.Warning(
                    message: $"Las credenciales del usuario {identification.Value} son incorrectas.",
                    exception: new InvalidDataException("Las credenciales son incorrectas."));

            // Se genera el codigo para restablecer la contraseña.

            var resetPasswordRecord = new ResetPassword
            {
                ResetPasswordId = Guid.NewGuid().ToString(),
                ResetPasswordCode = _random.Next(100000, 999999),
                ExpirationDate = DateTime.Now.AddDays(1)
            };

            await _authUserRepository.UpdateResetPasswordByIdAndStatus(
                id: existingUser.Id,
                resetPasswordRecord: resetPasswordRecord);

            // Se envia el correo para restablecer la contraseña.

            var userEmails = new List<string> { existingUser.Email };
            var subject = "Restablece tu contraseña!";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $"Para restablecer tu contraseña ingresa en el siguiente <a href=\"https://localhost:17568?rpid={resetPasswordRecord.ResetPasswordId}&uid={existingUser.Id}\">enlace</a> e ingresa el codigo <b>{resetPasswordRecord.ResetPasswordCode}</b>..."
            };

            await EmailUtils.SendEmailToUsers(
                userEmails: userEmails,
                subject: subject,
                bodyBuilder: bodyBuilder);

            _logger.Information($"Se empieza el proceso para restablecer la contraseña del usuario {id}.");
        }
        public async Task ConfirmResetPassword(ResetPasswordDto resetPassword)
        {
            // Valida que el usuario ingresado exista.

            var existingUser = await _authUserRepository.FindOneByIdAndResetPasswordIdAndCodeAndStatus(
                id: resetPassword.Id,
                resetPasswordId: resetPassword.ResetPasswordId,
                resetPasswordCode: resetPassword.ResetPasswordCode);

            if (existingUser is null)
                _logger.Warning(
                    message: $"El codigo del usuario {resetPassword.Id} no es correcto.",
                    exception: new InvalidDataException("El codigo no es correcto."));

            if (existingUser.ResetPassword.ExpirationDate < resetPassword.ResetPasswordDate)
                _logger.Warning(
                    message: $"El codigo del usuario {resetPassword.Id} ya ha expirado.",
                    exception: new InvalidDataException("El codigo ingresado ya ha expirado, por favor genere uno nuevo."));

            // Genera un token basado en la contraseña.

            var tokenizedPassword = await _tokenizationDataService.TokenizePassword(value: resetPassword.NewPassword);

            // Se actualiza la contraseña en la BD.

            await _authUserRepository.UpdatePasswordAndSecretIdByIdAndStatus(
                id: resetPassword.Id,
                newPassword: tokenizedPassword.TokenizedValue,
                secretId: tokenizedPassword.SecretId);

            _logger.Information($"Se restablece correctamente la contraseña del usuario {existingUser.Id}.");
        }

        public async Task UpdateEmail(UserToUpdateEmailDto userToUpdateEmail)
        {
            // Valida que el Id ingresado sea correcto.

            var existingUser = await _authUserRepository.FindOneByUserIdAndStatus(userId: userToUpdateEmail.Id);

            if (existingUser is null)
                _logger.Warning(
                    message: $"El usuario con id {userToUpdateEmail.Id} no se encuentra activo en el sistema.",
                    exception: new InvalidDataException("El usuario no se encuentra activo en el sistema."));

            // Se genera el codigo para actualizar el email.

            var updateEmail = new UpdateEmail
            {
                NewEmail = userToUpdateEmail.Email,
                UpdateEmailId = Guid.NewGuid().ToString(),
                UpdateEmailCode = _random.Next(100000, 999999),
                ExpirationDate = DateTime.Now.AddDays(1)
            };

            await _authUserRepository.UpdateEmailIdAndCodeAndEmailByIdAndStatus(
                id: existingUser.Id,
                updateEmail: updateEmail);

            // Se envia el correo para restablecer el email.

            var userEmails = new List<string> { existingUser.Email };
            var subject = "Actualiza tu email!";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $"Para actualizar el email de tu cuenta a la nueva direccion <b>{userToUpdateEmail.Email}</b> ingresa en el siguiente <a href=\"https://localhost:17568?rpid={updateEmail.UpdateEmailId}&uid={existingUser.Id}&ne={updateEmail.NewEmail}\">enlace</a> e ingresa el codigo <b>{updateEmail.UpdateEmailCode}</b>..."
            };

            await EmailUtils.SendEmailToUsers(
                userEmails: userEmails,
                subject: subject,
                bodyBuilder: bodyBuilder);

            _logger.Information($"Se empieza el proceso para actualizar la email del usuario {existingUser.Id}.");
        }
        public async Task ConfirmUpdateEmail(UpdateEmailDto resetPassword)
        {
            // Valida que el usuario ingresado exista.

            var existingUser = await _authUserRepository.FindOneByIdAndUpdateEmailIdAndCodeAndEmailAndDateAndStatus(
                id: resetPassword.Id,
                newEmail: resetPassword.NewEmail,
                updateEmailId: resetPassword.UpdateEmailId,
                updateEmailCode: resetPassword.UpdateEmailCode);

            if (existingUser is null)
                _logger.Warning(
                    message: $"El codigo del usuario {resetPassword.Id} no es correcto.",
                    exception: new InvalidDataException("El codigo no es correcto."));

            if (existingUser.ResetPassword.ExpirationDate < resetPassword.UpdateEmailDate)
                _logger.Warning(
                    message: $"El codigo del usuario {resetPassword.Id} ya ha expirado.",
                    exception: new InvalidDataException("El codigo ingresado ya ha expirado, por favor genere uno nuevo."));

            // Se actualiza el email en el servicio UserManagement.

            await _userManagementService.UpdateProfileEmail(
                userId: existingUser.UserId,
                email: resetPassword.NewEmail);

            // Se actualiza el email en la BD.

            await _authUserRepository.UpdateEmailByIdAndStatus(
                id: resetPassword.Id,
                email: resetPassword.NewEmail);

            // Se envia el correo para confirmar la actualizacion.

            var userEmails = new List<string> { existingUser.Email, resetPassword.NewEmail };
            var subject = "Email actualizado exitosamente!";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $"Buen día. Por medio de este mensaje se desea informar que la direccion de correo <b>{resetPassword.NewEmail}</b> ha sido actualizada exitosamente."
            };

            await EmailUtils.SendEmailToUsers(
                userEmails: userEmails,
                subject: subject,
                bodyBuilder: bodyBuilder);

            _logger.Information($"Se actualiza correctamente el email del usuario {existingUser.Id}.");
        }
        public async Task UpdatePassword(UserToUpdatePasswordDto userToUpdatePassword)
        {
            // Valida que el Id ingresado sea correcto.

            var existingUser = await _authUserRepository.FindOneByUserIdAndStatus(userId: userToUpdatePassword.Id);

            if (existingUser is null)
                _logger.Warning(
                    message: $"El usuario con id {userToUpdatePassword.Id} no se encuentra activo en el sistema.",
                    exception: new InvalidDataException("El usuario no se encuentra activo en el sistema."));

            // Valida que la contraseña sea correcta.

            var tokenizedPassword = await _tokenizationDataService.TokenizeExistingPassword(
                                        tokenizedValue: new()
                                        {
                                            TokenizedValue = userToUpdatePassword.CurrentPassword,
                                            SecretId = existingUser.SecretId
                                        });

            if (!tokenizedPassword.Equals(existingUser.Password))
                _logger.Warning(
                    message: $"La contraseña es incorrecta.",
                    exception: new InvalidDataException("La contraseña es incorrecta."));

            // Se tokeniza la nueva contraseña.

            var newTokenizedPassword = await _tokenizationDataService.TokenizePassword(value: userToUpdatePassword.NewPassword);

            // Se actualiza la contraseña en la base de datos.

            await _authUserRepository.UpdatePasswordAndSecretIdByIdAndStatus(
                id: existingUser.Id,
                newPassword: newTokenizedPassword.TokenizedValue,
                secretId: newTokenizedPassword.SecretId);

            _logger.Information($"Se actualiza correctamente la contraseña del usuario {existingUser.Id}.");
        }

        private Task<string> ConfigureBearerToken(
            UserInformationDto userInformation,
            string role)
        {
            ClaimsIdentity claims = new();

            claims.AddClaim(new("sub", userInformation.Id));
            claims.AddClaim(new("name", userInformation.Name));
            claims.AddClaim(new("surname", userInformation.Surname));
            claims.AddClaim(new("email", userInformation.Email));
            claims.AddClaim(new(ClaimTypes.Role, role));

            var bearerToken = _jwtTokenSettings.GenerateBearerToken(claims: claims);

            _logger.Information($"Se genera el BearerToken del usuario {userInformation.Id} exitosamente.");

            return Task.FromResult(result: bearerToken);
        }
    }
}
