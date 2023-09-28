using HealthHub.Application.Dtos.Commons;

namespace HealthHub.BusinessLogic.Utils;

public static class UserUtils
{
    public static string GetUserId(IdentificationDto identification) => $"{identification.IdentificationType.Id}_{identification.Value}";
}
