using HealthHub.Application.Dtos.UserManagement;

namespace HealthHub.Application.Interfaces.UserManagement;

public interface IUserManagementService
{
    Task<string> RegisterNewUser(UserInformationDto userInformation);
    Task UserActivation(BasicUserActivationDto basicUserActivation);

    Task<UserInformationDto> GetUserInformationById(string userId);

    Task UpdateProfileData(UserToUpdateDto userToUpdate);
    Task UpdateProfileEmail(string userId, string email);

    Task<List<FlatUserInformationDto>> GetAllUsers();
}
