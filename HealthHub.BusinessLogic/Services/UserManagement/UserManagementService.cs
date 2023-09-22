using Angels.Packages.Logger.Services;
using AutoMapper;
using HealthHub.Application.Dtos.UserManagement;
using HealthHub.Application.Interfaces.UserManagement;
using HealthHub.Domain.Enums.UserManagement;
using HealthHub.Domain.Interfaces.UserManagement;
using HealthHub.Domain.Models.UserManagement;
using Microsoft.Extensions.Logging;

namespace HealthHub.BusinessLogic.Services.UserManagement;

public class UserManagementService : IUserManagementService
{
    private readonly ILogger<UserManagementService> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserManagementService(
        ILogger<UserManagementService> logger,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _logger = logger;
        _userRepository = userRepository;
        _mapper = mapper;
    }


    public async Task<string> RegisterNewUser(UserInformationDto userInformation)
    {
        var userId = await _userRepository.InsertOneAsync(
                         entity: _mapper.Map<User>(source: userInformation));

        _logger.Information($"Se registra el usuario {userId} exitosamente.");

        return userId;
    }
    public async Task UserActivation(BasicUserActivationDto basicUserActivation)
    {
        await _userRepository.ActivateUserById(
            id: basicUserActivation.UserId,
            activationDate: basicUserActivation.ActivationDate,
            activationMethod: (ActivationMethod)basicUserActivation.ActivationMethod);
    }

    public async Task<UserInformationDto> GetUserInformationById(string userId)
    {
        var user = await _userRepository.FindOneByIdAndStatusAsync(entityId: userId);

        if (user is null)
            _logger.Warning(
                message: $"El usuario {userId} no existe.",
                exception: new InvalidDataException("Usuario inexistente."));

        return _mapper.Map<UserInformationDto>(source: user);
    }

    public async Task UpdateProfileData(UserToUpdateDto userToUpdate)
    {
        await _userRepository.UpdateUserById(
            user: _mapper.Map<User>(source: userToUpdate));
    }
    public async Task UpdateProfileEmail(string userId, string email)
    {
        await _userRepository.UpdateEmailByIdAndStatus(
            userId: userId,
            email: email);

        _logger.Information($"Se actualiza correctamente la informacion del usuario {userId}.");
    }

    public async Task<List<FlatUserInformationDto>> GetAllUsers()
    {
        var users = _mapper.Map<List<FlatUserInformationDto>>(
                        source: await _userRepository.FindManyByStatusAsync());

        _logger.Information($"Se obtiene el listado de usuarios exitosamente.");

        return users;
    }
}
