using HealthHub.Domain.Enums.UserManagement;

namespace HealthHub.Domain.Models.UserManagement;

public class Identification
{
    public string Value { get; set; }
    public IdentificationType IdentificationType { get; set; }
}
